using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using PlaceMint.Manager;
using PlaceMint.Manager.PMException;

namespace PlaceMint.Access
{
    using Properties;

    /// <summary>
    /// Show samples of a slot
    /// </summary>
    public partial class SampleSlotGUI : Form
    {
        private ConfigureGUI _parent;
        private int _group;
        private int _slot;
        private const int TOOL_TIP_TIME = 1500; //ms
        private const string NONE = "None";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="groupIndex"></param>
        /// <param name="slotIndex"></param>
        /// <param name="parent"></param>
        public SampleSlotGUI(RectangleWrap rect, int groupIndex, int slotIndex, ConfigureGUI parent)
        {
            this.Visible = false;
            _parent = parent;
            this.Owner = _parent;
            _group = groupIndex;
            _slot = slotIndex;

            InitializeComponent();
            this.Text = string.Format("{0} {1}:{2}", this.Text, _group + 1, _slot + 1);
            Bitmap rightArrow = (Bitmap)PlaceMint.Access.Properties.Resources.upArrow.Clone();
            Bitmap downArrow = (Bitmap)rightArrow.Clone();
            Bitmap leftArrow = (Bitmap)rightArrow.Clone();
            rightArrow.RotateFlip(RotateFlipType.Rotate90FlipNone);
            downArrow.RotateFlip(RotateFlipType.Rotate180FlipNone);
            leftArrow.RotateFlip(RotateFlipType.Rotate270FlipNone);
            this.moveUpButton.Image = PlaceMint.Access.Properties.Resources.upArrow;
            this.moveLeftButton.Image = leftArrow;
            this.moveDownButton.Image = downArrow;
            this.moveRightButton.Image = rightArrow;
            this.Icon = Properties.Resources.icon;
            setShape(rect);
            disableUpDownChange();
            widthUpDown.Maximum = (decimal)Int32.MaxValue;
            widthUpDown.Minimum = 0;
            heightUpDown.Maximum = (decimal)Int32.MaxValue;
            heightUpDown.Minimum = 0;
            xUpDown.Maximum = (decimal)Int32.MaxValue;
            xUpDown.Minimum = (decimal)Int32.MinValue;
            yUpDown.Maximum = (decimal)Int32.MaxValue;
            yUpDown.Minimum = (decimal)Int32.MinValue;
            setSlotTemplateSelect();
            widthUpDown.Value = rect.Width;
            heightUpDown.Value = rect.Height;
            xUpDown.Value = rect.X;
            yUpDown.Value = rect.Y;
            enableUpDownChange();
        }

        private void SampleSlotGUI_Change(object sender, EventArgs e)
        {
            updateUpDowns();
            _parent.updateSlotRect(this);
        }

        private void UpDown_ValueChanged(object sender, EventArgs e)
        {
            setShape(new RectangleWrap(
                Convert.ToInt32(xUpDown.Value),
                Convert.ToInt32(yUpDown.Value),
                Convert.ToInt32(widthUpDown.Value),
                Convert.ToInt32(heightUpDown.Value)));
            _parent.updateSlotRect(this);
        }

        private void setShape(RectangleWrap shape)
        {
            disableFormChange();
            this.Location = new Point(shape.X, shape.Y);
            this.Size = new Size(shape.Width, shape.Height);
            enableFormChange();
        }

        /// <summary>
        /// Update the windows location and size
        /// </summary>
        /// <param name="shape"></param>
        public void updateRect(RectangleWrap shape)
        {
            setShape(shape);
            updateUpDowns();
        }

        private void updateUpDowns()
        {
            disableUpDownChange();
            this.widthUpDown.Value = this.Size.Width;
            this.heightUpDown.Value = this.Size.Height;
            this.xUpDown.Value = this.Location.X;
            this.yUpDown.Value = this.Location.Y;
            enableUpDownChange();
        }

        /// <summary>
        /// Group index
        /// </summary>
        public int Group
        {
            get { return _group; }
        }

        /// <summary>
        /// Slot index
        /// </summary>
        public int Slot
        {
            get { return _slot; }
        }

        private void SampleSlotGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                _parent.uncheck(this);
            }
            this.Hide();
            _parent.Focus();
        }

        private void disableFormChange()
        {
            this.Resize -= SampleSlotGUI_Change;
            this.Move -= SampleSlotGUI_Change;
        }

        private void enableFormChange()
        {
            this.Resize += SampleSlotGUI_Change;
            this.Move += SampleSlotGUI_Change;
        }

        private void disableUpDownChange()
        {
            this.xUpDown.ValueChanged -= UpDown_ValueChanged;
            this.yUpDown.ValueChanged -= UpDown_ValueChanged;
            this.widthUpDown.ValueChanged -= UpDown_ValueChanged;
            this.heightUpDown.ValueChanged -= UpDown_ValueChanged;
        }

        private void enableUpDownChange()
        {
            this.xUpDown.ValueChanged += UpDown_ValueChanged;
            this.yUpDown.ValueChanged += UpDown_ValueChanged;
            this.widthUpDown.ValueChanged += UpDown_ValueChanged;
            this.heightUpDown.ValueChanged += UpDown_ValueChanged;
        }

        private void upDown_Validating(object sender, CancelEventArgs e)
        {
            NumericUpDown upDown = sender as NumericUpDown;
            if (upDown == null)
            {
                throw new IllegalSenderException(String.Format(Resources.illegalSenderFormat,
                    "upDown_Validating", "a NumericUpDown"));
            } try
            {
                int value = Convert.ToInt32(upDown.Value);
            }
            catch (OverflowException)
            {
                int boundary = 0;
                string direction = "";
                if (upDown.Value < 0)
                {
                    upDown.Value = (decimal)Int32.MinValue;
                    boundary = Int32.MinValue;
                    direction = "smaller";
                }
                else
                {
                    upDown.Value = (decimal)Int32.MaxValue;
                    boundary = Int32.MaxValue;
                    direction = "larger";
                }
                toolTip.Show(string.Format(Resources.intSizeFailFormat,
                    direction, boundary), upDown, TOOL_TIP_TIME);
                e.Cancel = true;
            }
        }

        private void slotTemplatesSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)slotTemplatesSelect.SelectedItem != NONE)
            {
                widthUpDown.Value = _parent.SlotTemplateList[slotTemplatesSelect.SelectedIndex - 1].Width;
                heightUpDown.Value = _parent.SlotTemplateList[slotTemplatesSelect.SelectedIndex - 1].Height;
            }
        }

        /// <summary>
        /// Set the objects to appear in the template dropdown
        /// </summary>
        public void setSlotTemplateSelect()
        {
            slotTemplatesSelect.Items.Clear();
            slotTemplatesSelect.Items.Add(NONE);
            foreach (SlotTemplate sTemp in _parent.SlotTemplateList)
            {
                slotTemplatesSelect.Items.Add(sTemp.Title);
            }
            slotTemplatesSelect.SelectedIndex = 0;
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X, this.Location.Y - this.Size.Height);
        }

        private void moveRightButton_Click(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
        }

        private void moveLeftButton_Click(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X - this.Size.Width, this.Location.Y);
        }

        /// <summary>
        /// Allow ConfigureGui to close a sample slot without triggering SampleSlotGUI_FormClosing 
        /// </summary>
        public void closeNoValidate()
        {
            this.FormClosing -= SampleSlotGUI_FormClosing;
            this.Close();
        }

        private void detailsGrabberButton_Click(object sender, EventArgs e)
        {
            _parent.showDetailsGrabber();
        }
    }
}