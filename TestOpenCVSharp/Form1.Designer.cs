namespace TestOpenCVSharp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainFeedPicBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnFindGap = new System.Windows.Forms.Button();
            this.radioBtnBlackWhite = new System.Windows.Forms.RadioButton();
            this.radioBtnColor = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.mainFeedPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainFeedPicBox
            // 
            this.mainFeedPicBox.Location = new System.Drawing.Point(85, 38);
            this.mainFeedPicBox.Margin = new System.Windows.Forms.Padding(2);
            this.mainFeedPicBox.Name = "mainFeedPicBox";
            this.mainFeedPicBox.Size = new System.Drawing.Size(447, 251);
            this.mainFeedPicBox.TabIndex = 0;
            this.mainFeedPicBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(256, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Live Feed";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnStart.Location = new System.Drawing.Point(85, 305);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(137, 41);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnStop.Location = new System.Drawing.Point(393, 305);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(139, 41);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click_1);
            // 
            // btnFindGap
            // 
            this.btnFindGap.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnFindGap.Location = new System.Drawing.Point(236, 305);
            this.btnFindGap.Margin = new System.Windows.Forms.Padding(2);
            this.btnFindGap.Name = "btnFindGap";
            this.btnFindGap.Size = new System.Drawing.Size(137, 41);
            this.btnFindGap.TabIndex = 4;
            this.btnFindGap.Text = "Find Gap";
            this.btnFindGap.UseVisualStyleBackColor = true;
            this.btnFindGap.Click += new System.EventHandler(this.btnFindGap_Click);
            // 
            // radioBtnBlackWhite
            // 
            this.radioBtnBlackWhite.AutoSize = true;
            this.radioBtnBlackWhite.Location = new System.Drawing.Point(552, 88);
            this.radioBtnBlackWhite.Name = "radioBtnBlackWhite";
            this.radioBtnBlackWhite.Size = new System.Drawing.Size(110, 19);
            this.radioBtnBlackWhite.TabIndex = 5;
            this.radioBtnBlackWhite.Text = "Black and White";
            this.radioBtnBlackWhite.UseVisualStyleBackColor = true;
            this.radioBtnBlackWhite.CheckedChanged += new System.EventHandler(this.radioBtnBlackWhite_CheckedChanged);
            // 
            // radioBtnColor
            // 
            this.radioBtnColor.AutoSize = true;
            this.radioBtnColor.Checked = true;
            this.radioBtnColor.Location = new System.Drawing.Point(552, 122);
            this.radioBtnColor.Name = "radioBtnColor";
            this.radioBtnColor.Size = new System.Drawing.Size(54, 19);
            this.radioBtnColor.TabIndex = 6;
            this.radioBtnColor.TabStop = true;
            this.radioBtnColor.Text = "Color";
            this.radioBtnColor.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 369);
            this.Controls.Add(this.radioBtnColor);
            this.Controls.Add(this.radioBtnBlackWhite);
            this.Controls.Add(this.btnFindGap);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainFeedPicBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.mainFeedPicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox mainFeedPicBox;
        private Label label1;
        private Button btnStart;
        private Button btnStop;
        private Button btnFindGap;
        private RadioButton radioBtnBlackWhite;
        private RadioButton radioBtnColor;
    }
}