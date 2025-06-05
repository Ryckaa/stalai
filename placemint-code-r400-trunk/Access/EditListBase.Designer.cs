//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Access
{
    partial class EditListBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        protected System.ComponentModel.IContainer components = null;

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
        protected void InitializeComponent()
        {
            this.mainListBox = new System.Windows.Forms.ListBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.detailsGrabberButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainListBox
            // 
            this.mainListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.mainListBox.FormattingEnabled = true;
            this.mainListBox.Location = new System.Drawing.Point(0, 0);
            this.mainListBox.Name = "mainListBox";
            this.mainListBox.Size = new System.Drawing.Size(106, 266);
            this.mainListBox.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(191, 216);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(89, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save && Close";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(112, 25);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(167, 20);
            this.titleTextBox.TabIndex = 1;
            this.titleTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.titleTextBox_Validating);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(120, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(27, 13);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "Title";
            // 
            // cancelButton
            // 
            this.cancelButton.CausesValidation = false;
            this.cancelButton.Location = new System.Drawing.Point(112, 216);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(72, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.CausesValidation = false;
            this.deleteButton.Location = new System.Drawing.Point(112, 158);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(72, 23);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // detailsGrabberButton
            // 
            this.detailsGrabberButton.Location = new System.Drawing.Point(112, 187);
            this.detailsGrabberButton.Name = "detailsGrabberButton";
            this.detailsGrabberButton.Size = new System.Drawing.Size(167, 23);
            this.detailsGrabberButton.TabIndex = 6;
            this.detailsGrabberButton.Text = "Show Details Grabber";
            this.detailsGrabberButton.UseVisualStyleBackColor = true;
            this.detailsGrabberButton.Click += new System.EventHandler(this.detailsGrabberButton_Click);
            // 
            // EditListBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.detailsGrabberButton);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.mainListBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditListBase";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ListBox mainListBox;
        protected System.Windows.Forms.Button saveButton;
        protected System.Windows.Forms.TextBox titleTextBox;
        protected System.Windows.Forms.Label titleLabel;
        protected System.Windows.Forms.Button cancelButton;
        protected System.Windows.Forms.Button deleteButton;
        protected System.Windows.Forms.Button detailsGrabberButton;
    }
}