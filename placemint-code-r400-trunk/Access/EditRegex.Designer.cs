//cs0108: Hides inherited member
#pragma warning disable 0108

namespace PlaceMint.Access
{
    partial class EditRegex
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
            this.regexTextBox = new System.Windows.Forms.TextBox();
            this.regexLabel = new System.Windows.Forms.Label();
            this.caseSensitiveCheckBox = new System.Windows.Forms.CheckBox();
            this.titleRadio = new System.Windows.Forms.RadioButton();
            this.classRadio = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // mainListBox
            // 
            this.mainListBox.Size = new System.Drawing.Size(106, 303);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(192, 268);
            this.saveButton.TabIndex = 8;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(113, 268);
            this.cancelButton.TabIndex = 7;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(209, 213);
            this.deleteButton.TabIndex = 6;
            // 
            // detailsGrabberButton
            // 
            this.detailsGrabberButton.Location = new System.Drawing.Point(112, 239);
            // 
            // regexTextBox
            // 
            this.regexTextBox.Location = new System.Drawing.Point(112, 74);
            this.regexTextBox.Multiline = true;
            this.regexTextBox.Name = "regexTextBox";
            this.regexTextBox.Size = new System.Drawing.Size(167, 113);
            this.regexTextBox.TabIndex = 2;
            this.regexTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.regexTextBox_Validating);
            // 
            // regexLabel
            // 
            this.regexLabel.AutoSize = true;
            this.regexLabel.Location = new System.Drawing.Point(120, 58);
            this.regexLabel.Name = "regexLabel";
            this.regexLabel.Size = new System.Drawing.Size(98, 13);
            this.regexLabel.TabIndex = 3;
            this.regexLabel.Text = "Regular Expression";
            // 
            // caseSensitiveCheckBox
            // 
            this.caseSensitiveCheckBox.AutoSize = true;
            this.caseSensitiveCheckBox.Location = new System.Drawing.Point(113, 217);
            this.caseSensitiveCheckBox.Name = "caseSensitiveCheckBox";
            this.caseSensitiveCheckBox.Size = new System.Drawing.Size(96, 17);
            this.caseSensitiveCheckBox.TabIndex = 5;
            this.caseSensitiveCheckBox.Text = "Case Sensitive";
            this.caseSensitiveCheckBox.UseVisualStyleBackColor = true;
            this.caseSensitiveCheckBox.CheckedChanged += new System.EventHandler(this.caseSensitiveCheckBox_CheckedChanged);
            // 
            // titleRadio
            // 
            this.titleRadio.AutoSize = true;
            this.titleRadio.Location = new System.Drawing.Point(113, 194);
            this.titleRadio.Name = "titleRadio";
            this.titleRadio.Size = new System.Drawing.Size(45, 17);
            this.titleRadio.TabIndex = 3;
            this.titleRadio.TabStop = true;
            this.titleRadio.Text = "Title";
            this.titleRadio.UseVisualStyleBackColor = true;
            this.titleRadio.CheckedChanged += new System.EventHandler(this.titleRadio_CheckedChanged);
            // 
            // classRadio
            // 
            this.classRadio.AutoSize = true;
            this.classRadio.Location = new System.Drawing.Point(191, 194);
            this.classRadio.Name = "classRadio";
            this.classRadio.Size = new System.Drawing.Size(50, 17);
            this.classRadio.TabIndex = 4;
            this.classRadio.TabStop = true;
            this.classRadio.Text = "Class";
            this.classRadio.UseVisualStyleBackColor = true;
            this.classRadio.CheckedChanged += new System.EventHandler(this.classRadio_CheckedChanged);
            // 
            // EditRegex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 303);
            this.Controls.Add(this.classRadio);
            this.Controls.Add(this.titleRadio);
            this.Controls.Add(this.regexLabel);
            this.Controls.Add(this.regexTextBox);
            this.Controls.Add(this.caseSensitiveCheckBox);
            this.Name = "EditRegex";
            this.Text = "Edit Regex List";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditRegex_FormClosed);
            this.Controls.SetChildIndex(this.detailsGrabberButton, 0);
            this.Controls.SetChildIndex(this.caseSensitiveCheckBox, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.deleteButton, 0);
            this.Controls.SetChildIndex(this.saveButton, 0);
            this.Controls.SetChildIndex(this.regexTextBox, 0);
            this.Controls.SetChildIndex(this.regexLabel, 0);
            this.Controls.SetChildIndex(this.titleRadio, 0);
            this.Controls.SetChildIndex(this.mainListBox, 0);
            this.Controls.SetChildIndex(this.titleTextBox, 0);
            this.Controls.SetChildIndex(this.titleLabel, 0);
            this.Controls.SetChildIndex(this.classRadio, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox regexTextBox;
        private System.Windows.Forms.Label regexLabel;
        private System.Windows.Forms.CheckBox caseSensitiveCheckBox;
        private System.Windows.Forms.RadioButton titleRadio;
        private System.Windows.Forms.RadioButton classRadio;
    }
}