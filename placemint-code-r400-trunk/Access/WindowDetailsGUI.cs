using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PlaceMint.Access
{
    using PlaceMint.Access.Properties;
    using PlaceMint.Manager;

    /// <summary>
    /// GUI to display grabbed window details
    /// </summary>
    public partial class WindowDetailsGUI : Form
    {
        private WindowDetailsGrabber _grabber;
        private const string BUTTON_CHECKED = "Select Window or Click to Cancel";
        private const string BUTTON_UNCHECKED = "Click to Select Window";

        /// <summary>
        /// Default constructor
        /// </summary>
        public WindowDetailsGUI()
        {
            InitializeComponent();
            Icon = new Icon(Resources.icon, new Size(32, 32));
            _grabber = new WindowDetailsGrabber();
            selectWindowCheckBox.Text = BUTTON_UNCHECKED;
        }

        private void WindowDetailsGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true; // this cancels the close event.
        }

        private void selectWindowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (selectWindowCheckBox.Checked)
            {
                selectWindowCheckBox.Text = BUTTON_CHECKED;
                _grabber.RegisterEventHandler(detailResult);
            }
            else
            {
                selectWindowCheckBox.Text = BUTTON_UNCHECKED;
                _grabber.UnregisterEventHandler(detailResult);
            }
        }

        private void detailResult(object sender, WindowDetailsGrabber.WindowDetailEventArgs details)
        {
            titleTextBox.Text = details.Title;
            classTextBox.Text = details.Class;
            xTextBox.Text = details.Shape.X.ToString();
            yTextBox.Text = details.Shape.Y.ToString();
            heightTextBox.Text = details.Shape.Height.ToString();
            widthTextBox.Text = details.Shape.Width.ToString();
            selectWindowCheckBox.Checked = false;
        }
    }
}
