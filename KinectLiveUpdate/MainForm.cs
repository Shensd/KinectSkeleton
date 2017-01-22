

namespace KinectLiveUpdate
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Microsoft.Kinect;

    public partial class MainForm : Form
    {


        private KinectSensor sensor;
        private System.Drawing.Graphics graphics;
        private Bitmap body;
        private Bitmap lastColor;

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
                this.sensor.ColorStream.Enable();
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;
                this.sensor.ColorFrameReady += this.SensorColorFrameReady;

                try
                {
                    this.sensor.Start();
                } catch (IOException)
                {
                    this.sensor = null;
                }
            }
        }

        private void SensorColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            if(!check_video.Checked)
            {
                return;
            }
            ColorImageFrame img = e.OpenColorImageFrame();
            byte[] imagedata = new byte[img.PixelDataLength];
            img.CopyPixelDataTo(imagedata);
            Bitmap bmap = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppRgb);
            BitmapData bmapdata = bmap.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.WriteOnly, bmap.PixelFormat);
            IntPtr ptr = bmapdata.Scan0;
            Marshal.Copy(imagedata, 0, ptr, img.PixelDataLength);
            bmap.UnlockBits(bmapdata);
            lastColor = bmap;
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

                Rectangle fill = new Rectangle(0, 0, body.Width, body.Height);
                
                this.graphics.FillRectangle(Brushes.Black, fill);
                if(this.lastColor != null && check_video.Checked)
                {
                    this.graphics.DrawImage(lastColor, 0, 0);
                }

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
            bool drawJoints = check_joints.Checked;
            bool drawBones = check_bones.Checked;

            //Torso
            this.drawBone(skeleton, JointType.Head, JointType.ShoulderCenter, drawBones, drawJoints);
            this.drawBone(skeleton, JointType.ShoulderCenter, JointType.ShoulderLeft, drawBones, drawJoints);
            this.drawBone(skeleton, JointType.ShoulderCenter, JointType.ShoulderRight, drawBones, drawJoints);
            this.drawBone(skeleton, JointType.ShoulderCenter, JointType.Spine, drawBones, drawJoints);
            this.drawBone(skeleton, JointType.Spine, JointType.HipCenter, drawBones, drawJoints);
            this.drawBone(skeleton, JointType.HipCenter, JointType.HipLeft, drawBones, drawJoints);
            this.drawBone(skeleton, JointType.HipCenter, JointType.HipRight, drawBones, drawJoints);

            //Left arm
            this.drawBone(skeleton, JointType.ShoulderLeft, JointType.ElbowLeft, drawBones, drawJoints);
            this.drawBone(skeleton, JointType.ElbowLeft, JointType.WristLeft, drawBones, drawJoints);
            this.drawBone(skeleton, JointType.WristLeft, JointType.HandLeft, drawBones, drawJoints);

            //Right arm
            this.drawBone(skeleton, JointType.ShoulderRight, JointType.ElbowRight, drawBones, drawJoints);
            this.drawBone(skeleton, JointType.ElbowRight, JointType.WristRight, drawBones, drawJoints);
            this.drawBone(skeleton, JointType.WristRight, JointType.HandRight, drawBones, drawJoints);

            //Left leg
            this.drawBone(skeleton, JointType.HipLeft, JointType.KneeLeft, drawBones, drawJoints);
            this.drawBone(skeleton, JointType.KneeLeft, JointType.AnkleLeft, drawBones, drawJoints);
            this.drawBone(skeleton, JointType.AnkleLeft, JointType.FootLeft, drawBones, drawJoints);

            //Right leg
            this.drawBone(skeleton, JointType.HipRight, JointType.KneeRight, drawBones, drawJoints);
            this.drawBone(skeleton, JointType.KneeRight, JointType.AnkleRight, drawBones, drawJoints);
            this.drawBone(skeleton, JointType.AnkleRight, JointType.FootRight, drawBones, drawJoints);
        }

        private Pen bonePen = new Pen(Brushes.Green, 5);

        private void drawBone(Skeleton skele, JointType jointA, JointType jointB, bool drawBones, bool drawJoints)
        {
            int x_offset = (pictureBox.Width / 2);
            int y_offset = (pictureBox.Height / 2);

            int scale = 200;

            if(check_video.Checked)
            {
                int spine_scale = (int)skele.Joints[JointType.Spine].Position.Z;
                int hip_scale = (int)skele.Joints[JointType.HipCenter].Position.Z;
                int head_scale = (int)skele.Joints[JointType.Head].Position.Z;
                scale = (int)((spine_scale + head_scale + head_scale) / 3) * 120;
            }

            int x1_pos = (int)(skele.Joints[jointA].Position.X * scale) + x_offset;
            int y1_pos = (int)(-skele.Joints[jointA].Position.Y * scale) + y_offset;
            int x2_pos = (int)(skele.Joints[jointB].Position.X * scale) + x_offset;
            int y2_pos = (int)(-skele.Joints[jointB].Position.Y * scale) + y_offset;

            if (drawBones)
            {
                Point pointA = new Point(x1_pos, y1_pos);
                Point pointB = new Point(x2_pos, y2_pos);

                graphics.DrawLine(this.bonePen, pointA, pointB);
            }

            if(drawJoints)
            {
                Rectangle rect1 = new Rectangle(x1_pos - 5, y1_pos - 5, 10, 10);
                Rectangle rect2 = new Rectangle(x2_pos - 5, y2_pos - 5, 10, 10);
                graphics.FillEllipse(Brushes.Green, rect1);
                graphics.FillEllipse(Brushes.Green, rect2);
            }
        }
    }
}
