namespace KinectLiveUpdate {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label_info = new System.Windows.Forms.Label();
            this.check_video = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.check_joints = new System.Windows.Forms.CheckBox();
            this.check_bones = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBox.Location = new System.Drawing.Point(12, 34);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(640, 480);
            this.pictureBox.TabIndex = 5;
            this.pictureBox.TabStop = false;
            // 
            // label_info
            // 
            this.label_info.AutoSize = true;
            this.label_info.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_info.Location = new System.Drawing.Point(12, 9);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(189, 19);
            this.label_info.TabIndex = 6;
            this.label_info.Text = "Kinect Live Body Model";
            // 
            // check_video
            // 
            this.check_video.AutoSize = true;
            this.check_video.Location = new System.Drawing.Point(232, 9);
            this.check_video.Name = "check_video";
            this.check_video.Size = new System.Drawing.Size(89, 17);
            this.check_video.TabIndex = 7;
            this.check_video.Text = "Enable Video";
            this.toolTip.SetToolTip(this.check_video, "Shows video capture and scales skeleton to body");
            this.check_video.UseVisualStyleBackColor = true;
            // 
            // check_joints
            // 
            this.check_joints.AutoSize = true;
            this.check_joints.Checked = true;
            this.check_joints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_joints.Location = new System.Drawing.Point(425, 9);
            this.check_joints.Name = "check_joints";
            this.check_joints.Size = new System.Drawing.Size(89, 17);
            this.check_joints.TabIndex = 8;
            this.check_joints.Text = "Enable Joints";
            this.toolTip.SetToolTip(this.check_joints, " Enable dot representations of joints");
            this.check_joints.UseVisualStyleBackColor = true;
            // 
            // check_bones
            // 
            this.check_bones.AutoSize = true;
            this.check_bones.Checked = true;
            this.check_bones.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_bones.Location = new System.Drawing.Point(327, 9);
            this.check_bones.Name = "check_bones";
            this.check_bones.Size = new System.Drawing.Size(92, 17);
            this.check_bones.TabIndex = 9;
            this.check_bones.Text = "Enable Bones";
            this.toolTip.SetToolTip(this.check_bones, "Enable line representations of bones");
            this.check_bones.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 526);
            this.Controls.Add(this.check_bones);
            this.Controls.Add(this.check_joints);
            this.Controls.Add(this.check_video);
            this.Controls.Add(this.label_info);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Kinect BodyModel";
            this.Load += new System.EventHandler(this.MainForm_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label_info;
        private System.Windows.Forms.CheckBox check_video;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox check_joints;
        private System.Windows.Forms.CheckBox check_bones;
    }
}

