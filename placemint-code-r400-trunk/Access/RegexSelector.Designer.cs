namespace PlaceMint.Access
{
    partial class RegexSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.expressionsLabel = new System.Windows.Forms.Label();
            this.regexTextBox = new System.Windows.Forms.TextBox();
            this.regexCheckedList = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // expressionsLabel
            // 
            this.expressionsLabel.AutoSize = true;
            this.expressionsLabel.Location = new System.Drawing.Point(4, 4);
            this.expressionsLabel.Name = "expressionsLabel";
            this.expressionsLabel.Size = new System.Drawing.Size(63, 13);
            this.expressionsLabel.TabIndex = 1;
            this.expressionsLabel.Text = "Expressions";
            // 
            // regexTextBox
            // 
            this.regexTextBox.Location = new System.Drawing.Point(4, 210);
            this.regexTextBox.Multiline = true;
            this.regexTextBox.Name = "regexTextBox";
            this.regexTextBox.Size = new System.Drawing.Size(280, 151);
            this.regexTextBox.TabIndex = 2;
            // 
            // regexCheckedList
            // 
            this.regexCheckedList.FormattingEnabled = true;
            this.regexCheckedList.Location = new System.Drawing.Point(7, 21);
            this.regexCheckedList.Name = "regexCheckedList";
            this.regexCheckedList.Size = new System.Drawing.Size(277, 184);
            this.regexCheckedList.TabIndex = 3;
            this.regexCheckedList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.regexCheckedList_ItemCheck);
            this.regexCheckedList.SelectedIndexChanged += new System.EventHandler(this.regexCheckedList_SelectedIndexChanged);
            // 
            // RegexSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.regexCheckedList);
            this.Controls.Add(this.regexTextBox);
            this.Controls.Add(this.expressionsLabel);
            this.Name = "RegexSelector";
            this.Size = new System.Drawing.Size(290, 370);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label expressionsLabel;
        private System.Windows.Forms.TextBox regexTextBox;
        private System.Windows.Forms.CheckedListBox regexCheckedList;
    }
}
