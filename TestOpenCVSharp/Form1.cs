/*
 *  Simple proof-of-concept for OpenCvSharp Inspection System
 *  
 *  ToDo:
 *      - add seperate window for real time camera view
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

namespace TestOpenCVSharp
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource cancelTokenSource;
        private CancellationToken token;
        private VideoCapture capture;
        private Mat frame;
        private Bitmap image;
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
            openCam();
            btnStart_Click(null, null);
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
            try
            {
                cancelTokenSource.Cancel();
                cancelTokenSource.Dispose();
            }
            catch (Exception ex) { }
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
            Task task = new Task(startLiveFeed, token);
            task.Start();
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
            Task.Delay(delay);
        }

        private void drawCrossHair(Mat frame, int line_size) 
        {
            int vert_y1 = (frame.Rows - line_size) / 2;
            int vert_y2 = vert_y1 + line_size;
            int horz_x1 = (frame.Cols - line_size) / 2;
            int horz_x2 = horz_x1 + line_size;

            Cv2.Line(frame, frame.Cols/2, vert_y1, frame.Cols/2, vert_y2, new Scalar(255, 255), thickness:4);
            Cv2.Line(frame, horz_x1, frame.Rows/2, horz_x2, frame.Rows/2, new Scalar(255, 255), thickness:4);
        }

        private void detectGap()
        {
            Mat src_gray = new Mat();
            Mat src_canny = new Mat();
            Mat src_roi = new Mat();
            Mat src_thresh = new Mat();
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchyIndexes;

            capture.Read(frame);
            src_roi = frame;
            // double check if this split is needed
            /*
            src_roi = frame.SubMat(new OpenCvSharp.Range(50, frame.Rows),
                                   new OpenCvSharp.Range(0, frame.Cols));
            */
            if (src_roi == null)
                return;

            updateLiveFeedImage(src_roi);
            Task.Delay(2000);

            Cv2.CvtColor(src_roi, src_gray, ColorConversionCodes.BGR2GRAY);
            updateLiveFeedImage(src_gray);
            Task.Delay(2000);

            if (!updateThreshValues())
                return;

            Cv2.Threshold(src_gray, src_thresh, threshold_value, threshold_max_value, ThresholdTypes.Binary);
            updateLiveFeedImage(src_thresh);
            Task.Delay(2000);

            Cv2.Canny(src_thresh, src_canny, canny_thresh1, canny_thresh2, 3, false);
            updateLiveFeedImage(src_canny);
            Task.Delay(2000);

            Cv2.FindContours(src_canny, out contours, out hierarchyIndexes,
                             mode: RetrievalModes.External,
                             method: ContourApproximationModes.ApproxNone);
            //MessageBox.Show("Found # " + contours.Length.ToString() + " contours");

            Mat contured_mat = new Mat(src_gray.Rows, src_gray.Cols, MatType.CV_8UC1);
            Cv2.DrawContours(contured_mat, contours, -1, new Scalar(255, 255), thickness: 3, hierarchy: hierarchyIndexes);
            Cv2.ImShow("tt", contured_mat);

            // working better edge\wire detection
            List<OpenCvSharp.Point[]> used_contours = new List<OpenCvSharp.Point[]>();
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

            int center_x = contured_mat_2.Cols / 2;
            int center_y = contured_mat_2.Rows / 2;
            int max_line_val = 50;
            int left_x = 0;
            int right_x = 0;

            // find left and right x
            for (int i = 0; i < used_contours.Count; i++) 
            {
                OpenCvSharp.Point[] cnts = used_contours.ElementAt(i);
                for (int j = 0; j < cnts.Length; j++)
                {
                    int check_x = cnts[j].X;
                    if (check_x > (center_x - max_line_val) &&
                        check_x < center_x)
                    { 
                        left_x = check_x;
                    }
                    if (check_x < (center_x + max_line_val) &&
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

            int left_dist_from_center = center_x - left_x;
            int right_dist_from_center = right_x - center_x;

            MessageBox.Show("left-dist " + left_dist_from_center + "\n" + 
                            "right-dist " + right_dist_from_center);

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
            try
            {
                cancelTokenSource.Cancel();
                cancelTokenSource.Dispose();
            }
            catch (Exception ex) { }
        }

        private void btnFindGap_Click(object sender, EventArgs e)
        {
            try
            {
                cancelTokenSource.Cancel();
                cancelTokenSource.Dispose();
            }
            catch (Exception ex) { }
            Task.Delay(10);
            detectGap();
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
