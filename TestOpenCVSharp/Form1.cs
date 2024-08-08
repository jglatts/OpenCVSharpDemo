using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using DirectShowLib;
using System.Diagnostics;
using System.Numerics;

namespace TestOpenCVSharp
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource cancelTokenSource;
        private CancellationToken token;
        private VideoCapture capture;
        private Mat frame;
        private Bitmap image;
        private int camIndex;

        public Form1()
        {
            InitializeComponent();
            camIndex = 1;
            listCamDevices();
            openCam();
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
                capture.Read(frame);
                updateLiveFeedImage(frame);
                Task.Delay(10);
            }
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

            MessageBox.Show("Contours Length: " + contours.Length);

            //anaylzeFrame();
        }

        private void anaylzeFrame(Mat src_roi, Mat src_canny)
        {
            // splits the image into 2 seperate ROI's
            // find contours of both ROI's and stores in 2 sets
            // run convex hull on both sets of contours
            // find distance between the convex hulls
            // draw both sets on OG img to examine
            // thats gap!
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
            catch (Exception ex)
            {

            }
        }

        private void btnFindGap_Click(object sender, EventArgs e)
        {
            try
            {
                cancelTokenSource.Cancel();
                cancelTokenSource.Dispose();
            }
            catch (Exception ex)
            {
            }
            Task.Delay(10);
            detectGap();
        }
    }
}
