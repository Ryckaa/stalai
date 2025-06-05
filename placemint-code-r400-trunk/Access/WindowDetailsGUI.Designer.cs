namespace PlaceMint.Access
{
    partial class WindowDetailsGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.detailsGroupBox = new System.Windows.Forms.GroupBox();
            this.sizeGroupBox = new System.Windows.Forms.GroupBox();
            this.sizeByLable = new System.Windows.Forms.Label();
            this.heightTextBox = new System.Windows.Forms.TextBox();
            this.widthTextBox = new System.Windows.Forms.TextBox();
            this.locationGroupBox = new System.Windows.Forms.GroupBox();
            this.locationCommaLabel = new System.Windows.Forms.Label();
            this.yTextBox = new System.Windows.Forms.TextBox();
            this.xTextBox = new System.Windows.Forms.TextBox();
            this.classTextBox = new System.Windows.Forms.TextBox();
            this.classLabel = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.selectWindowCheckBox = new System.Windows.Forms.CheckBox();
            this.detailsGroupBox.SuspendLayout();
            this.sizeGroupBox.SuspendLayout();
            this.locationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // detailsGroupBox
            // 
            this.detailsGroupBox.Controls.Add(this.sizeGroupBox);
            this.detailsGroupBox.Controls.Add(this.locationGroupBox);
            this.detailsGroupBox.Controls.Add(this.classTextBox);
            this.detailsGroupBox.Controls.Add(this.classLabel);
            this.detailsGroupBox.Controls.Add(this.titleTextBox);
            this.detailsGroupBox.Controls.Add(this.titleLabel);
            this.detailsGroupBox.Location = new System.Drawing.Point(12, 41);
            this.detailsGroupBox.Name = "detailsGroupBox";
            this.detailsGroupBox.Size = new System.Drawing.Size(329, 158);
            this.detailsGroupBox.TabIndex = 1;
            this.detailsGroupBox.TabStop = false;
            this.detailsGroupBox.Text = "Details";
            // 
            // sizeGroupBox
            // 
            this.sizeGroupBox.Controls.Add(this.sizeByLable);
            this.sizeGroupBox.Controls.Add(this.heightTextBox);
            this.sizeGroupBox.Controls.Add(this.widthTextBox);
            this.sizeGroupBox.Location = new System.Drawing.Point(9, 97);
            this.sizeGroupBox.Name = "sizeGroupBox";
            this.sizeGroupBox.Size = new System.Drawing.Size(151, 46);
            this.sizeGroupBox.TabIndex = 4;
            this.sizeGroupBox.TabStop = false;
            this.sizeGroupBox.Text = "Size";
            // 
            // sizeByLable
            // 
            this.sizeByLable.AutoSize = true;
            this.sizeByLable.Location = new System.Drawing.Point(77, 22);
            this.sizeByLable.Name = "sizeByLable";
            this.sizeByLable.Size = new System.Drawing.Size(12, 13);
            this.sizeByLable.TabIndex = 2;
            this.sizeByLable.Text = "x";
            // 
            // heightTextBox
            // 
            this.heightTextBox.Location = new System.Drawing.Point(95, 20);
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.ReadOnly = true;
            this.heightTextBox.Size = new System.Drawing.Size(50, 20);
            this.heightTextBox.TabIndex = 1;
            // 
            // widthTextBox
            // 
            this.widthTextBox.Location = new System.Drawing.Point(16, 19);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.ReadOnly = true;
            this.widthTextBox.Size = new System.Drawing.Size(50, 20);
            this.widthTextBox.TabIndex = 0;
            // 
            // locationGroupBox
            // 
            this.locationGroupBox.Controls.Add(this.locationCommaLabel);
            this.locationGroupBox.Controls.Add(this.yTextBox);
            this.locationGroupBox.Controls.Add(this.xTextBox);
            this.locationGroupBox.Location = new System.Drawing.Point(166, 97);
            this.locationGroupBox.Name = "locationGroupBox";
            this.locationGroupBox.Size = new System.Drawing.Size(151, 46);
            this.locationGroupBox.TabIndex = 5;
            this.locationGroupBox.TabStop = false;
            this.locationGroupBox.Text = "Location";
            // 
            // locationCommaLabel
            // 
            this.locationCommaLabel.AutoSize = true;
            this.locationCommaLabel.Location = new System.Drawing.Point(77, 22);
            this.locationCommaLabel.Name = "locationCommaLabel";
            this.locationCommaLabel.Size = new System.Drawing.Size(10, 13);
            this.locationCommaLabel.TabIndex = 2;
            this.locationCommaLabel.Text = ",";
            // 
            // yTextBox
            // 
            this.yTextBox.Location = new System.Drawing.Point(95, 19);
            this.yTextBox.Name = "yTextBox";
            this.yTextBox.ReadOnly = true;
            this.yTextBox.Size = new System.Drawing.Size(50, 20);
            this.yTextBox.TabIndex = 1;
            // 
            // xTextBox
            // 
            this.xTextBox.Location = new System.Drawing.Point(16, 19);
            this.xTextBox.Name = "xTextBox";
            this.xTextBox.ReadOnly = true;
            this.xTextBox.Size = new System.Drawing.Size(50, 20);
            this.xTextBox.TabIndex = 0;
            // 
            // classTextBox
            // 
            this.classTextBox.Location = new System.Drawing.Point(9, 71);
            this.classTextBox.Name = "classTextBox";
            this.classTextBox.ReadOnly = true;
            this.classTextBox.Size = new System.Drawing.Size(308, 20);
            this.classTextBox.TabIndex = 3;
            // 
            // classLabel
            // 
            this.classLabel.AutoSize = true;
            this.classLabel.Location = new System.Drawing.Point(6, 55);
            this.classLabel.Name = "classLabel";
            this.classLabel.Size = new System.Drawing.Size(32, 13);
            this.classLabel.TabIndex = 2;
            this.classLabel.Text = "Class";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(9, 32);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.ReadOnly = true;
            this.titleTextBox.Size = new System.Drawing.Size(308, 20);
            this.titleTextBox.TabIndex = 1;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(6, 16);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(30, 13);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Ttitle";
            // 
            // selectWindowCheckBox
            // 
            this.selectWindowCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.selectWindowCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectWindowCheckBox.Location = new System.Drawing.Point(12, 12);
            this.selectWindowCheckBox.Name = "selectWindowCheckBox";
            this.selectWindowCheckBox.Size = new System.Drawing.Size(329, 23);
            this.selectWindowCheckBox.TabIndex = 2;
            this.selectWindowCheckBox.Text = "Click to Select Window";
            this.selectWindowCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.selectWindowCheckBox.UseVisualStyleBackColor = true;
            this.selectWindowCheckBox.CheckedChanged += new System.EventHandler(this.selectWindowCheckBox_CheckedChanged);
            // 
            // WindowDetailsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 207);
            this.Controls.Add(this.selectWindowCheckBox);
            this.Controls.Add(this.detailsGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WindowDetailsGUI";
            this.Text = "Window Details Grabber";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WindowDetailsGUI_FormClosing);
            this.detailsGroupBox.ResumeLayout(false);
            this.detailsGroupBox.PerformLayout();
            this.sizeGroupBox.ResumeLayout(false);
            this.sizeGroupBox.PerformLayout();
            this.locationGroupBox.ResumeLayout(false);
            this.locationGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox detailsGroupBox;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox classTextBox;
        private System.Windows.Forms.Label classLabel;
        private System.Windows.Forms.GroupBox sizeGroupBox;
        private System.Windows.Forms.Label sizeByLable;
        private System.Windows.Forms.TextBox heightTextBox;
        private System.Windows.Forms.TextBox widthTextBox;
        private System.Windows.Forms.GroupBox locationGroupBox;
        private System.Windows.Forms.Label locationCommaLabel;
        private System.Windows.Forms.TextBox yTextBox;
        private System.Windows.Forms.TextBox xTextBox;
        private System.Windows.Forms.CheckBox selectWindowCheckBox;
    }
}