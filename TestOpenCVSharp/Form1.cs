/*
 *  Simple proof-of-concept for OpenCvSharp Inspection System
 *  
 *  ToDo:
 *      - add seperate window for real time camera view
 *  
 *  Working with Amscope!!
 *  Needs to use 32bit DLL and 32bit compiler
 *  
 *  Will need to convert from bitmap to OpenCV mat
 *  https://learn.microsoft.com/en-us/answers/questions/365983/convert-int-byte()-or-bitmap-to-the-mat-type-of-op
 *  
 *  Date:   8/8/24
 *  Author: John Glatts
 */
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using DirectShowLib;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.Intrinsics;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Drawing;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Toupcam;
using System.Drawing.Imaging;

namespace TestOpenCVSharp
{
    public partial class Form1 : Form
    {
        private Toupcam cam_ = null;
        private Bitmap bmp_ = null;
        private CancellationTokenSource cancelTokenSource;
        private CancellationToken token;
        private VideoCapture capture;
        private Mat frame;
        private Bitmap image;
        private MotorHelper motorHelper;
        private bool useBlackWhite;
        private int camIndex;
        private int threshold_value;
        private int threshold_max_value;
        private int canny_thresh1;
        private int canny_thresh2;

        public Form1()
        {
            InitializeComponent();
            camIndex = 1;
            useBlackWhite = false;
            cancelTokenSource = new CancellationTokenSource();
            openAmScopeCam();
            motorHelper = new MotorHelper();
            if (!motorHelper.setDevice())
                MessageBox.Show("error with uc100!");
            else
                MessageBox.Show("UC100 connected!");
        }

        private void listCamDevices()
        {
            String s = "\n";
            DsDevice[] devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            if (devices.Length == 0)
            {
                MessageBox.Show("no video devices!");
                return;
            }
            foreach (DsDevice dsDevice in devices)
            {
                s += dsDevice.Name + "\n";
            }
            MessageBox.Show("num devices: " + devices.Length + s);
        }

        private void startLiveAmScopeThread()
        {
            if (cam_ == null)
                return;

            uint resnum = cam_.ResolutionNumber;
            uint eSize = 0;
            if (cam_.get_eSize(out eSize))
            {
                for (uint i = 0; i < resnum; ++i)
                {
                    int w = 0, h = 0;
                    if (cam_.get_Resolution(i, out w, out h)) { 
                        // print the resoultions number
                        // will be useful later on
                    }
                }

                int width = 0, height = 0;
                if (cam_.get_Size(out width, out height))
                {
                    /* The backend of Winform is GDI, which is different from WPF/UWP/WinUI's backend Direct3D/Direct2D.
                     * We use their respective native formats, Bgr24 in Winform, and Bgr32 in WPF/UWP/WinUI
                     */
                    bmp_ = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                    if (!cam_.StartPullModeWithCallback(new Toupcam.DelegateEventCallback(DelegateOnEventCallback)))
                        MessageBox.Show("Failed to start camera.");
                    else
                    {
                        bool autoexpo = true;
                        cam_.get_AutoExpoEnable(out autoexpo);
                    }
                }
            }
        }

        private void DelegateOnEventCallback(Toupcam.eEVENT evt)
        {
            /* this is call by internal thread of toupcam.dll which is NOT the same of UI thread.
             * Why we use BeginInvoke, Please see:
             * http://msdn.microsoft.com/en-us/magazine/cc300429.aspx
             * http://msdn.microsoft.com/en-us/magazine/cc188732.aspx
             * http://stackoverflow.com/questions/1364116/avoiding-the-woes-of-invoke-begininvoke-in-cross-thread-winform-event-handling
             */
            BeginInvoke((Action)(() =>
            {
                /* this run in the UI thread */
                if (cam_ != null)
                {
                    switch (evt)
                    {
                        case Toupcam.eEVENT.EVENT_ERROR:
                            //MessageBox.Show("error!");     
                            break;
                        case Toupcam.eEVENT.EVENT_DISCONNECTED:
                            //MessageBox.Show("disconnected!");
                            break;
                        case Toupcam.eEVENT.EVENT_EXPOSURE:
                            //MessageBox.Show("exposure!");
                            break;
                        case Toupcam.eEVENT.EVENT_IMAGE:
                            onEventImage();
                            break;
                        case Toupcam.eEVENT.EVENT_STILLIMAGE:
                            //MessageBox.Show("error!");
                            break;
                        case Toupcam.eEVENT.EVENT_TEMPTINT:
                            //MessageBox.Show("error!");
                            break;
                        default:
                            break;
                    }
                }
            }));
        }

        private void onEventImage()
        {
            if (bmp_ != null)
            {
                Toupcam.FrameInfoV3 info = new Toupcam.FrameInfoV3();
                bool bOK = false;
                try
                {
                    BitmapData bmpdata = bmp_.LockBits(new Rectangle(0, 0, bmp_.Width, bmp_.Height), ImageLockMode.WriteOnly, bmp_.PixelFormat);
                    try
                    {
                        bOK = cam_.PullImageV3(bmpdata.Scan0, 0, 24, bmpdata.Stride, out info); // check the return value
                    }
                    finally
                    {
                        bmp_.UnlockBits(bmpdata);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (bOK)
                {
                    frame = BitmapConverter.ToMat(bmp_);
                    drawCrossHair(frame, 200);
                    updateLiveFeedImage(frame);
                }
                GC.Collect();
            }
        }

        private void tryOpenCam()
        {
            frame = new Mat();
            openCam();

            if (capture.IsOpened())
            {
                capture.Read(frame);
                image = BitmapConverter.ToBitmap(frame);
                if (mainFeedPicBox.Image != null)
                {
                    mainFeedPicBox.Image.Dispose();
                }
                mainFeedPicBox.Image = image;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            /*
            stopLiveFeedThread();
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
            Task task = new Task(startLiveFeed, token);
            task.Start();
            */
            startLiveAmScopeThread();
        }

        private void openCam()
        {
            try
            {
                capture = new VideoCapture(camIndex);
                capture.Open(camIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace + "\n" + ex.InnerException);
                return;
            }
        }

        private void openAmScopeCam()
        {
            Toupcam.DeviceV2[] arr = Toupcam.EnumV2();
            if (arr.Length <= 0)
            {
                MessageBox.Show("No camera found.");
                return;
            }
            cam_ = Toupcam.Open(arr[0].id);
            if (cam_ != null)
            { 
                MessageBox.Show("cam opened!");
            }
        }

        private void startLiveFeed()
        {
            frame = new Mat();

            if (!capture.IsOpened())
            {
                MessageBox.Show("not open!");
                return;
            }

            // forever loop to run the webcam
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                getAndDisplayFrame(5);
            }
        }

        private void getAndDisplayFrame(int delay)
        {
            capture.Read(frame);
            if (useBlackWhite)
            {
                Mat new_frame = new Mat();
                Cv2.CvtColor(frame, new_frame, ColorConversionCodes.BGR2GRAY);
                frame = new_frame;
            }
            drawCrossHair(frame, 200);
            updateLiveFeedImage(frame);
            //Task.Delay(delay);
        }

        private void drawCrossHair(Mat frame, int line_size) 
        {
            int rows = 431;
            int cols = 900;

            int vert_y1 = (rows - line_size) / 2;
            int vert_y2 = vert_y1 + line_size;
            int horz_x1 = (cols - line_size) / 2;
            int horz_x2 = horz_x1 + line_size;

            Cv2.Line(frame, cols/2, vert_y1, cols/2, vert_y2, new Scalar(0, 255), thickness:4);
            Cv2.Line(frame, horz_x1, rows/2, horz_x2, rows/2, new Scalar(0, 255), thickness:4);
        }

        private void detectGap()
        {
            Mat src_gray = new Mat();
            Mat src_canny = new Mat();
            Mat src_roi = new Mat();
            Mat src_thresh = new Mat();
            OpenCvSharp.Point[][] contours;
            List<OpenCvSharp.Point[]> used_contours = new List<OpenCvSharp.Point[]>();
            HierarchyIndex[] hierarchyIndexes;
            int center_x = 0;
            int center_y = 0;
            int max_line_val = 50;
            int left_x = 0;
            int right_x = 0;
            int left_dist_from_center = 0;
            int right_dist_from_center = 0;

            if (frame == null)
                return;

            capture.Read(frame);
            src_roi = frame;
            if (src_roi == null)
                return;

            if (!updateThreshValues())
                return;

            Cv2.CvtColor(src_roi, src_gray, ColorConversionCodes.BGR2GRAY);
            Cv2.Threshold(src_gray, src_thresh, threshold_value, threshold_max_value, ThresholdTypes.Binary);
            Cv2.Canny(src_thresh, src_canny, canny_thresh1, canny_thresh2, 3, false);
            Cv2.FindContours(src_canny, out contours, out hierarchyIndexes,
                             mode: RetrievalModes.External,
                             method: ContourApproximationModes.ApproxNone);

            Mat contured_mat = new Mat(src_gray.Rows, src_gray.Cols, MatType.CV_8UC1);
            Cv2.DrawContours(contured_mat, contours, -1, new Scalar(255, 255), thickness: 3, hierarchy: hierarchyIndexes);
            Cv2.ImShow("tt", contured_mat);

            Mat contured_mat_2 = new Mat(src_gray.Rows, src_gray.Cols, MatType.CV_8UC1);
            for (int j = 0; j < contours.Length; j++)
            {
                if (contours[j].Length > 20)
                {
                    Cv2.DrawContours(contured_mat_2, contours, j, new Scalar(255, 255),
                                    thickness: -1, hierarchy: hierarchyIndexes);
                    used_contours.Add(contours[j]);
                }
            }

            center_x = contured_mat_2.Cols / 2;
            center_y = contured_mat_2.Rows / 2;
            max_line_val = 50;
            left_x = 0;
            right_x = 0;

            // find left and right x
            for (int i = 0; i < used_contours.Count; i++) 
            {
                OpenCvSharp.Point[] cnts = used_contours.ElementAt(i);
                for (int j = 0; j < cnts.Length; j++)
                {
                    int check_x = cnts[j].X;
                    if (check_x >= (center_x - max_line_val) &&
                        check_x < center_x)
                    { 
                        left_x = check_x;
                    }
                    if (check_x <= (center_x + max_line_val) &&
                        check_x > center_x)
                    {
                        right_x = check_x;
                    }
                }
            }

            Cv2.Circle(contured_mat_2, contured_mat_2.Cols / 2, contured_mat_2.Rows / 2, 5, new Scalar(255, 255),
                        thickness: 2);

            Cv2.Line(contured_mat_2, left_x, 0, left_x, contured_mat_2.Cols, new Scalar(255, 255), thickness: 4);
            Cv2.Line(contured_mat_2, right_x, 0, right_x, contured_mat_2.Cols, new Scalar(255, 255), thickness: 4);
            Cv2.ImShow("t2t", contured_mat_2);

            left_dist_from_center = center_x - left_x;
            right_dist_from_center = right_x - center_x;

            // fine tune this
            bool check = Math.Abs((right_dist_from_center - left_dist_from_center)) <= 15;

            String s = "left_x " + left_x.ToString() + " - right_x " + right_x.ToString();
            s += "\nleft-dist " + left_dist_from_center + "\n" +
                            "right-dist " + right_dist_from_center + "\n" +
                            (check == true ? "gap good" : "gap not good");

            MessageBox.Show(s);
        }

        private bool updateThreshValues()
        {

            if (!Int32.TryParse(txtBoxThreshHoldVal.Text, out threshold_value))
                 return false;
            if (!Int32.TryParse(txtBoxThreshMaxVal.Text, out threshold_max_value))
                return false;
            if (!Int32.TryParse(txtBoxCannyThresh1.Text, out canny_thresh1))
                return false;
            if (!Int32.TryParse(txtBoxCannyThresh2.Text, out canny_thresh2))
                return false;
            return true;
        }

        private void updateLiveFeedImage(Mat frame) 
        {
            image = BitmapConverter.ToBitmap(frame);
            if (mainFeedPicBox.Image != null)
            {
                mainFeedPicBox.Image.Dispose();
            }
            mainFeedPicBox.Image = image;
        }

        private void btnStop_Click_1(object sender, EventArgs e)
        {
            stopLiveFeedThread();
        }

        private void stopLiveFeedThread()
        {
            try
            {
                cancelTokenSource.Cancel();
                cancelTokenSource.Dispose();
            }
            catch (Exception ex) { }
        }

        private void btnFindGap_Click(object sender, EventArgs e)
        {
            stopLiveFeedThread();
            Task.Delay(10);
            detectGap();
            btnStart_Click(null, null);
        }

        private void radioBtnBlackWhite_CheckedChanged(object sender, EventArgs e)
        {
            useBlackWhite = radioBtnBlackWhite.Checked == true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
