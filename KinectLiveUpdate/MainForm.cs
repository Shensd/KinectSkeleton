

namespace KinectLiveUpdate
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Windows.Forms;
    using Microsoft.Kinect;

    public partial class MainForm : Form
    {


        private KinectSensor sensor;
        private System.Drawing.Graphics graphics;
        private Bitmap body;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {
            run();
        }

        private void run()
        {

            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }
            if (null != this.sensor)
            {
                this.sensor.SkeletonStream.Enable();
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                try
                {
                    this.sensor.Start();
                } catch (IOException)
                {
                    this.sensor = null;
                }
            }
        }

        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
            }
            if (skeletons.Length != 0)
            {
                //Initialize picture box for drawing
                this.body = new Bitmap(pictureBox.Width, pictureBox.Height);
                this.graphics = Graphics.FromImage(body);

                System.Drawing.Rectangle fill = new System.Drawing.Rectangle(0, 0, body.Width, body.Height);
                this.graphics.FillRectangle(Brushes.Black, fill);

                foreach (Skeleton skel in skeletons)
                {
                    if (skel.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        this.drawSkeletonLocationData(skel);
                    }
                }
                //Push changes
                pictureBox.Image = this.body;
                pictureBox.Update();
                this.body.Dispose();
            }
        }


        private void drawSkeletonLocationData(Skeleton skeleton)
        {
            bool drawJoints = false;

            //Torso
            this.drawBone(skeleton, JointType.Head, JointType.ShoulderCenter, drawJoints);
            this.drawBone(skeleton, JointType.ShoulderCenter, JointType.ShoulderLeft, drawJoints);
            this.drawBone(skeleton, JointType.ShoulderCenter, JointType.ShoulderRight, drawJoints);
            this.drawBone(skeleton, JointType.ShoulderCenter, JointType.Spine, drawJoints);
            this.drawBone(skeleton, JointType.Spine, JointType.HipCenter, drawJoints);
            this.drawBone(skeleton, JointType.HipCenter, JointType.HipLeft, drawJoints);
            this.drawBone(skeleton, JointType.HipCenter, JointType.HipRight, drawJoints);

            //Left arm
            this.drawBone(skeleton, JointType.ShoulderLeft, JointType.ElbowLeft, drawJoints);
            this.drawBone(skeleton, JointType.ElbowLeft, JointType.WristLeft, drawJoints);
            this.drawBone(skeleton, JointType.WristLeft, JointType.HandLeft, drawJoints);

            //Right arm
            this.drawBone(skeleton, JointType.ShoulderRight, JointType.ElbowRight, drawJoints);
            this.drawBone(skeleton, JointType.ElbowRight, JointType.WristRight, drawJoints);
            this.drawBone(skeleton, JointType.WristRight, JointType.HandRight, drawJoints);

            //Left leg
            this.drawBone(skeleton, JointType.HipLeft, JointType.KneeLeft, drawJoints);
            this.drawBone(skeleton, JointType.KneeLeft, JointType.AnkleLeft, drawJoints);
            this.drawBone(skeleton, JointType.AnkleLeft, JointType.FootLeft, drawJoints);

            //Right leg
            this.drawBone(skeleton, JointType.HipRight, JointType.KneeRight, drawJoints);
            this.drawBone(skeleton, JointType.KneeRight, JointType.AnkleRight, drawJoints);
            this.drawBone(skeleton, JointType.AnkleRight, JointType.FootRight, drawJoints);
        }

        private Pen bonePen = new Pen(Brushes.Green, 5);

        private void drawBone(Skeleton skele, JointType jointA, JointType jointB, bool drawJoints)
        {
            int x_offset = (pictureBox.Width / 2);
            int y_offset = (pictureBox.Height / 2);
            int scale = 150;

            int x1_pos = (int)(skele.Joints[jointA].Position.X * scale) + x_offset;
            int y1_pos = (int)(-skele.Joints[jointA].Position.Y * scale) + y_offset;
            int x2_pos = (int)(skele.Joints[jointB].Position.X * scale) + x_offset;
            int y2_pos = (int)(-skele.Joints[jointB].Position.Y * scale) + y_offset;

            Point pointA = new Point(x1_pos, y1_pos);
            Point pointB = new Point(x2_pos, y2_pos);

            graphics.DrawLine(this.bonePen, pointA, pointB);

            if(drawJoints)
            {
                System.Drawing.Rectangle rect1 = new System.Drawing.Rectangle(x1_pos - 5, y1_pos - 5, 10, 10);
                System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(x2_pos - 5, y2_pos - 5, 10, 10);
                graphics.FillEllipse(Brushes.Green, rect1);
                graphics.FillEllipse(Brushes.Green, rect2);
            }
        }
    }
}
