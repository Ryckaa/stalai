//cs0108: Hides inherited member
#pragma warning disable 0108

namespace PlaceMint.Access
{
    partial class EditSlotTemplateList
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
            this.widthLabel = new System.Windows.Forms.Label();
            this.widthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.heightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.heightLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // mainListBox
            // 
            this.mainListBox.Size = new System.Drawing.Size(106, 225);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(190, 187);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(112, 187);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(208, 129);
            // 
            // detailsGrabberButton
            // 
            this.detailsGrabberButton.Location = new System.Drawing.Point(112, 158);
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(120, 48);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(35, 13);
            this.widthLabel.TabIndex = 6;
            this.widthLabel.Text = "Width";
            // 
            // widthNumericUpDown
            // 
            this.widthNumericUpDown.Location = new System.Drawing.Point(112, 64);
            this.widthNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.widthNumericUpDown.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.widthNumericUpDown.Name = "widthNumericUpDown";
            this.widthNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.widthNumericUpDown.TabIndex = 2;
            this.widthNumericUpDown.ValueChanged += new System.EventHandler(this.widthNumericUpDown_ValueChanged);
            this.widthNumericUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.upDown_Validating);
            // 
            // heightNumericUpDown
            // 
            this.heightNumericUpDown.Location = new System.Drawing.Point(112, 103);
            this.heightNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.heightNumericUpDown.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.heightNumericUpDown.Name = "heightNumericUpDown";
            this.heightNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.heightNumericUpDown.TabIndex = 3;
            this.heightNumericUpDown.ValueChanged += new System.EventHandler(this.heightNumericUpDown_ValueChanged);
            this.heightNumericUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.upDown_Validating);
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(120, 87);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(38, 13);
            this.heightLabel.TabIndex = 6;
            this.heightLabel.Text = "Height";
            // 
            // EditSlotTemplateList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(292, 225);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.widthNumericUpDown);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.heightNumericUpDown);
            this.Name = "EditSlotTemplateList";
            this.Text = "Edit Slot Template List";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditSlotTemplateList_FormClosed);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.saveButton, 0);
            this.Controls.SetChildIndex(this.detailsGrabberButton, 0);
            this.Controls.SetChildIndex(this.deleteButton, 0);
            this.Controls.SetChildIndex(this.heightNumericUpDown, 0);
            this.Controls.SetChildIndex(this.heightLabel, 0);
            this.Controls.SetChildIndex(this.widthNumericUpDown, 0);
            this.Controls.SetChildIndex(this.widthLabel, 0);
            this.Controls.SetChildIndex(this.mainListBox, 0);
            this.Controls.SetChildIndex(this.titleTextBox, 0);
            this.Controls.SetChildIndex(this.titleLabel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.NumericUpDown widthNumericUpDown;
        private System.Windows.Forms.NumericUpDown heightNumericUpDown;
        private System.Windows.Forms.Label heightLabel;
    }
}
