namespace PlaceMint.Access
{
    partial class SampleSlotGUI
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
            this.components = new System.ComponentModel.Container();
            this.widthUpDown = new System.Windows.Forms.NumericUpDown();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.heightUpDown = new System.Windows.Forms.NumericUpDown();
            this.xUpDown = new System.Windows.Forms.NumericUpDown();
            this.yUpDown = new System.Windows.Forms.NumericUpDown();
            this.locationLabel = new System.Windows.Forms.Label();
            this.xLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.slotTemplatesSelect = new System.Windows.Forms.ComboBox();
            this.templateLabel = new System.Windows.Forms.Label();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.moveRightButton = new System.Windows.Forms.Button();
            this.moveLeftButton = new System.Windows.Forms.Button();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.detailsGrabberButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // widthUpDown
            // 
            this.widthUpDown.Location = new System.Drawing.Point(53, 29);
            this.widthUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.widthUpDown.Name = "widthUpDown";
            this.widthUpDown.Size = new System.Drawing.Size(45, 20);
            this.widthUpDown.TabIndex = 0;
            this.widthUpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            this.widthUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.upDown_Validating);
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Location = new System.Drawing.Point(12, 9);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(27, 13);
            this.sizeLabel.TabIndex = 1;
            this.sizeLabel.Text = "Size";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(12, 31);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(35, 13);
            this.widthLabel.TabIndex = 2;
            this.widthLabel.Text = "Width";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(104, 31);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(38, 13);
            this.heightLabel.TabIndex = 2;
            this.heightLabel.Text = "Height";
            // 
            // heightUpDown
            // 
            this.heightUpDown.Location = new System.Drawing.Point(148, 29);
            this.heightUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.heightUpDown.Name = "heightUpDown";
            this.heightUpDown.Size = new System.Drawing.Size(45, 20);
            this.heightUpDown.TabIndex = 1;
            this.heightUpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            this.heightUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.upDown_Validating);
            // 
            // xUpDown
            // 
            this.xUpDown.Location = new System.Drawing.Point(53, 74);
            this.xUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.xUpDown.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.xUpDown.Name = "xUpDown";
            this.xUpDown.Size = new System.Drawing.Size(45, 20);
            this.xUpDown.TabIndex = 2;
            this.xUpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            this.xUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.upDown_Validating);
            // 
            // yUpDown
            // 
            this.yUpDown.Location = new System.Drawing.Point(148, 74);
            this.yUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.yUpDown.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.yUpDown.Name = "yUpDown";
            this.yUpDown.Size = new System.Drawing.Size(45, 20);
            this.yUpDown.TabIndex = 3;
            this.yUpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            this.yUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.upDown_Validating);
            // 
            // locationLabel
            // 
            this.locationLabel.AutoSize = true;
            this.locationLabel.Location = new System.Drawing.Point(12, 54);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(48, 13);
            this.locationLabel.TabIndex = 1;
            this.locationLabel.Text = "Location";
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(12, 76);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(14, 13);
            this.xLabel.TabIndex = 2;
            this.xLabel.Text = "X";
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(104, 76);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(14, 13);
            this.yLabel.TabIndex = 2;
            this.yLabel.Text = "Y";
            // 
            // slotTemplatesSelect
            // 
            this.slotTemplatesSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.slotTemplatesSelect.FormattingEnabled = true;
            this.slotTemplatesSelect.Location = new System.Drawing.Point(12, 115);
            this.slotTemplatesSelect.Name = "slotTemplatesSelect";
            this.slotTemplatesSelect.Size = new System.Drawing.Size(181, 21);
            this.slotTemplatesSelect.TabIndex = 4;
            this.slotTemplatesSelect.SelectedIndexChanged += new System.EventHandler(this.slotTemplatesSelect_SelectedIndexChanged);
            // 
            // templateLabel
            // 
            this.templateLabel.AutoSize = true;
            this.templateLabel.Location = new System.Drawing.Point(12, 99);
            this.templateLabel.Name = "templateLabel";
            this.templateLabel.Size = new System.Drawing.Size(110, 13);
            this.templateLabel.TabIndex = 1;
            this.templateLabel.Text = "Predefined Templates";
            // 
            // moveUpButton
            // 
            this.moveUpButton.Location = new System.Drawing.Point(230, 31);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(34, 34);
            this.moveUpButton.TabIndex = 5;
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // moveRightButton
            // 
            this.moveRightButton.Location = new System.Drawing.Point(261, 65);
            this.moveRightButton.Name = "moveRightButton";
            this.moveRightButton.Size = new System.Drawing.Size(34, 34);
            this.moveRightButton.TabIndex = 5;
            this.moveRightButton.UseVisualStyleBackColor = true;
            this.moveRightButton.Click += new System.EventHandler(this.moveRightButton_Click);
            // 
            // moveLeftButton
            // 
            this.moveLeftButton.Location = new System.Drawing.Point(199, 65);
            this.moveLeftButton.Name = "moveLeftButton";
            this.moveLeftButton.Size = new System.Drawing.Size(34, 34);
            this.moveLeftButton.TabIndex = 5;
            this.moveLeftButton.UseVisualStyleBackColor = true;
            this.moveLeftButton.Click += new System.EventHandler(this.moveLeftButton_Click);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Location = new System.Drawing.Point(230, 100);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(34, 34);
            this.moveDownButton.TabIndex = 5;
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // detailsGrabberButton
            // 
            this.detailsGrabberButton.Location = new System.Drawing.Point(12, 142);
            this.detailsGrabberButton.Name = "detailsGrabberButton";
            this.detailsGrabberButton.Size = new System.Drawing.Size(181, 23);
            this.detailsGrabberButton.TabIndex = 6;
            this.detailsGrabberButton.Text = "Show Details Grabber";
            this.detailsGrabberButton.UseVisualStyleBackColor = true;
            this.detailsGrabberButton.Click += new System.EventHandler(this.detailsGrabberButton_Click);
            // 
            // SampleSlotGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 291);
            this.Controls.Add(this.detailsGrabberButton);
            this.Controls.Add(this.moveDownButton);
            this.Controls.Add(this.moveRightButton);
            this.Controls.Add(this.moveLeftButton);
            this.Controls.Add(this.moveUpButton);
            this.Controls.Add(this.slotTemplatesSelect);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.xLabel);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.templateLabel);
            this.Controls.Add(this.locationLabel);
            this.Controls.Add(this.yUpDown);
            this.Controls.Add(this.sizeLabel);
            this.Controls.Add(this.xUpDown);
            this.Controls.Add(this.heightUpDown);
            this.Controls.Add(this.widthUpDown);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SampleSlotGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Sample Slot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SampleSlotGUI_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown widthUpDown;
        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.NumericUpDown heightUpDown;
        private System.Windows.Forms.NumericUpDown xUpDown;
        private System.Windows.Forms.NumericUpDown yUpDown;
        private System.Windows.Forms.Label locationLabel;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox slotTemplatesSelect;
        private System.Windows.Forms.Label templateLabel;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.Button moveRightButton;
        private System.Windows.Forms.Button moveLeftButton;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button detailsGrabberButton;
    }
}