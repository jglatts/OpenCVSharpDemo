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
            src_roi = frame.SubMat(new OpenCvSharp.Range(50, frame.Rows),
                                   new OpenCvSharp.Range(0, frame.Cols));
            updateLiveFeedImage(src_roi);
            Task.Delay(2000);

            Cv2.CvtColor(src_roi, src_gray, ColorConversionCodes.BGR2GRAY);
            updateLiveFeedImage(src_gray);
            Task.Delay(2000);

            Cv2.Threshold(src_gray, src_thresh, 130, 450, ThresholdTypes.TozeroInv);
            updateLiveFeedImage(src_thresh);
            Task.Delay(2000);

            Cv2.Canny(src_thresh, src_canny, 50, 300, 3, true);
            updateLiveFeedImage(src_canny);
            Task.Delay(2000);

            Cv2.FindContours(src_canny, out contours, out hierarchyIndexes,
                             mode: RetrievalModes.External,
                             method: ContourApproximationModes.ApproxNone);

            anaylzeFrame(src_roi, src_canny);
            MessageBox.Show("Contours Length: " + contours.Length);
        }

        private void anaylzeFrame(Mat src_roi, Mat src_canny)
        {
            // splits the image into 2 seperate ROI's
            // find contours of both ROI's and stores in 2 sets
            // run convex hull on both sets of contours
            // find distance between the convex hulls
            // draw both sets on OG img to examine
            // thats gap!
            /*
                CPP Source
                Mat src_gray, src_thresh;
                Mat roi_left, roi_right;
                vector<vector<Point>> contours_set_one, contours_set_two;
                vector<Point> convex_hull_points_one, convex_hull_points_two;
                roi_left = src_canny(Range(0, src_canny.size[0]), Range(0, 47));
                roi_right = src_canny(Range(0, src_canny.size[0]), Range(47, src_canny.size[1]));
                findContours(roi_left, contours_set_one, RETR_EXTERNAL, CHAIN_APPROX_SIMPLE);
                findContours(roi_right, contours_set_two, RETR_EXTERNAL, CHAIN_APPROX_SIMPLE);
                convex_hull_points_one = contoursConvexHull(contours_set_one);
                convex_hull_points_two = contoursConvexHull(contours_set_two);
                translateContours(convex_hull_points_two, 47);
                Rect rect_left = boundingRect(convex_hull_points_one);
                Rect rect_right = boundingRect(convex_hull_points_two);
                rectangle(src_roi, rect_left, Scalar(0, 0, 255), 2);
                rectangle(src_roi, rect_right, Scalar(0, 0, 255), 2);
                int rect_left_pos = rect_left.x + rect_left.width;
                int rect_right_pos = rect_right.x;
                int center_pos = (rect_left_pos + rect_right_pos) / 2;
                cout << "The center pos is " << center_pos << endl;
                line(src_roi, Point(rect_right_pos, 40), Point(rect_left_pos, 40), Scalar(0, 0, 255), 1);
                line(src_roi, Point(center_pos, 0), Point(center_pos, roi_left.size[0]), Scalar(255, 0, 0), 1);
                cout << "Distance to left-edge " << center_pos - rect_left.x << endl;
                cout << "Distance to right-edge " << (rect_right.x + rect_right.width) - center_pos << endl;
            */
            Mat src_gray = new Mat();
            Mat src_thresh = new Mat();
            Mat roi_left = new Mat();
            Mat roi_right = new Mat();
            Mat out_left = new Mat();
            Mat out_right = new Mat();
            OpenCvSharp.Point[][] contoursSetOne;
            HierarchyIndex[] hierarchyIndexesOne;
            OpenCvSharp.Point[][] contoursSetTwo;
            HierarchyIndex[] hierarchyIndexesTwo;
            int middle_split = src_canny.Cols / 2;

            roi_left = src_canny.SubMat(new OpenCvSharp.Range(0, src_canny.Rows),
                                        new OpenCvSharp.Range(0, middle_split));

            roi_right = src_canny.SubMat(new OpenCvSharp.Range(0, src_canny.Rows),
                                         new OpenCvSharp.Range(middle_split, src_canny.Cols));

            Cv2.ImShow("left", roi_left);
            Cv2.ImShow("right", roi_right);

            Cv2.FindContours(roi_left, out contoursSetOne, out hierarchyIndexesOne,
                             mode: RetrievalModes.External,
                             method: ContourApproximationModes.ApproxSimple);

            Cv2.FindContours(roi_right, out contoursSetTwo, out hierarchyIndexesTwo,
                             mode: RetrievalModes.External,
                             method: ContourApproximationModes.ApproxSimple);
            try
            {
                /*
                vector<vector<Point>> hull(contours.size() );
                for (size_t i = 0; i < contours.size(); i++)
                {
                    convexHull(contours[i], hull[i]);
                }
                */
                for (int i = 0; i < contoursSetOne.Length; i++) 
                {
                    //InputArray left_in = InputArray.Create<InputArray>(contoursSetOne[i]);
                    Mat out_hull_left = new Mat();
                    //Cv2.ConvexHull(contoursSetOne[i], out_hull_left);
                }

                MessageBox.Show("num rows " + frame.Rows.ToString() + "\n" + 
                                "num cols " + frame.Cols.ToString());
                /*
                Cv2.ConvexHull(left_in, out_left);
                Cv2.ConvexHull(right_in, out_right);

                Cv2.ImShow("leftConvex", out_left);
                Cv2.ImShow("rightConvex", out_right);
                */
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
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
    }
}
