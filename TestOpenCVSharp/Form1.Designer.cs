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
            this.txtBoxThreshHoldVal = new System.Windows.Forms.TextBox();
            this.txtBoxThreshMaxVal = new System.Windows.Forms.TextBox();
            this.txtBoxCannyThresh1 = new System.Windows.Forms.TextBox();
            this.txtBoxCannyThresh2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnGoRight = new System.Windows.Forms.Button();
            this.btnGoLeft = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainFeedPicBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainFeedPicBox
            // 
            this.mainFeedPicBox.Location = new System.Drawing.Point(142, 74);
            this.mainFeedPicBox.Name = "mainFeedPicBox";
            this.mainFeedPicBox.Size = new System.Drawing.Size(900, 431);
            this.mainFeedPicBox.TabIndex = 0;
            this.mainFeedPicBox.TabStop = false;
            this.mainFeedPicBox.Click += new System.EventHandler(this.mainFeedPicBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(501, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 48);
            this.label1.TabIndex = 1;
            this.label1.Text = "Live Feed";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnStart.Location = new System.Drawing.Point(266, 524);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(196, 68);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnStop.Location = new System.Drawing.Point(691, 524);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(199, 68);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click_1);
            // 
            // btnFindGap
            // 
            this.btnFindGap.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnFindGap.Location = new System.Drawing.Point(480, 524);
            this.btnFindGap.Name = "btnFindGap";
            this.btnFindGap.Size = new System.Drawing.Size(196, 68);
            this.btnFindGap.TabIndex = 4;
            this.btnFindGap.Text = "Find Gap";
            this.btnFindGap.UseVisualStyleBackColor = true;
            this.btnFindGap.Click += new System.EventHandler(this.btnFindGap_Click);
            // 
            // radioBtnBlackWhite
            // 
            this.radioBtnBlackWhite.AutoSize = true;
            this.radioBtnBlackWhite.Location = new System.Drawing.Point(1128, 102);
            this.radioBtnBlackWhite.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioBtnBlackWhite.Name = "radioBtnBlackWhite";
            this.radioBtnBlackWhite.Size = new System.Drawing.Size(163, 29);
            this.radioBtnBlackWhite.TabIndex = 5;
            this.radioBtnBlackWhite.Text = "Black and White";
            this.radioBtnBlackWhite.UseVisualStyleBackColor = true;
            this.radioBtnBlackWhite.CheckedChanged += new System.EventHandler(this.radioBtnBlackWhite_CheckedChanged);
            // 
            // radioBtnColor
            // 
            this.radioBtnColor.AutoSize = true;
            this.radioBtnColor.Checked = true;
            this.radioBtnColor.Location = new System.Drawing.Point(1128, 158);
            this.radioBtnColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioBtnColor.Name = "radioBtnColor";
            this.radioBtnColor.Size = new System.Drawing.Size(80, 29);
            this.radioBtnColor.TabIndex = 6;
            this.radioBtnColor.TabStop = true;
            this.radioBtnColor.Text = "Color";
            this.radioBtnColor.UseVisualStyleBackColor = true;
            // 
            // txtBoxThreshHoldVal
            // 
            this.txtBoxThreshHoldVal.Location = new System.Drawing.Point(105, 43);
            this.txtBoxThreshHoldVal.Name = "txtBoxThreshHoldVal";
            this.txtBoxThreshHoldVal.Size = new System.Drawing.Size(150, 31);
            this.txtBoxThreshHoldVal.TabIndex = 7;
            this.txtBoxThreshHoldVal.Text = "230";
            this.txtBoxThreshHoldVal.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtBoxThreshMaxVal
            // 
            this.txtBoxThreshMaxVal.Location = new System.Drawing.Point(105, 90);
            this.txtBoxThreshMaxVal.Name = "txtBoxThreshMaxVal";
            this.txtBoxThreshMaxVal.Size = new System.Drawing.Size(150, 31);
            this.txtBoxThreshMaxVal.TabIndex = 8;
            this.txtBoxThreshMaxVal.Text = "100";
            // 
            // txtBoxCannyThresh1
            // 
            this.txtBoxCannyThresh1.Location = new System.Drawing.Point(1201, 450);
            this.txtBoxCannyThresh1.Name = "txtBoxCannyThresh1";
            this.txtBoxCannyThresh1.Size = new System.Drawing.Size(150, 31);
            this.txtBoxCannyThresh1.TabIndex = 9;
            this.txtBoxCannyThresh1.Text = "100";
            // 
            // txtBoxCannyThresh2
            // 
            this.txtBoxCannyThresh2.Location = new System.Drawing.Point(1201, 497);
            this.txtBoxCannyThresh2.Name = "txtBoxCannyThresh2";
            this.txtBoxCannyThresh2.Size = new System.Drawing.Size(150, 31);
            this.txtBoxCannyThresh2.TabIndex = 10;
            this.txtBoxCannyThresh2.Text = "200";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Threshold";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "MaxVal";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(1083, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 150);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtBoxThreshMaxVal);
            this.groupBox2.Controls.Add(this.txtBoxThreshHoldVal);
            this.groupBox2.Location = new System.Drawing.Point(1083, 233);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 158);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Threshold Params";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(1083, 409);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(300, 150);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Canny Params";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "Thresh2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "Thresh1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnGoRight);
            this.groupBox4.Controls.Add(this.btnGoLeft);
            this.groupBox4.Location = new System.Drawing.Point(142, 664);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1241, 184);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Motor Control";
            // 
            // btnGoRight
            // 
            this.btnGoRight.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGoRight.Location = new System.Drawing.Point(622, 40);
            this.btnGoRight.Name = "btnGoRight";
            this.btnGoRight.Size = new System.Drawing.Size(196, 42);
            this.btnGoRight.TabIndex = 18;
            this.btnGoRight.Text = ">>>";
            this.btnGoRight.UseVisualStyleBackColor = true;
            this.btnGoRight.Click += new System.EventHandler(this.btnGoRight_Click);
            // 
            // btnGoLeft
            // 
            this.btnGoLeft.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGoLeft.Location = new System.Drawing.Point(253, 40);
            this.btnGoLeft.Name = "btnGoLeft";
            this.btnGoLeft.Size = new System.Drawing.Size(196, 42);
            this.btnGoLeft.TabIndex = 17;
            this.btnGoLeft.Text = "<<<";
            this.btnGoLeft.UseVisualStyleBackColor = true;
            this.btnGoLeft.Click += new System.EventHandler(this.btnGoLeft_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1488, 889);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtBoxCannyThresh2);
            this.Controls.Add(this.txtBoxCannyThresh1);
            this.Controls.Add(this.radioBtnColor);
            this.Controls.Add(this.radioBtnBlackWhite);
            this.Controls.Add(this.btnFindGap);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainFeedPicBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.mainFeedPicBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
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
        private TextBox txtBoxThreshHoldVal;
        private TextBox txtBoxThreshMaxVal;
        private TextBox txtBoxCannyThresh1;
        private TextBox txtBoxCannyThresh2;
        private Label label2;
        private Label label3;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label5;
        private Label label4;
        private GroupBox groupBox4;
        private Button btnGoRight;
        private Button btnGoLeft;
    }
}