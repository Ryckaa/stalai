using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using PlaceMint.Manager;
using System.Drawing;

namespace PlaceMint.Access
{
    /// <summary>
    /// Display a border around the shape
    /// </summary>
    public class Overlay : Form
    {
        private delegate void HideForm();
        private RectangleWrap _shape;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shape">Shape to size and position the boarder</param>
        public Overlay(RectangleWrap shape)
        {
            _shape = shape;
            TransparencyKey = Color.Cyan;
            BackColor = TransparencyKey;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;
            TopMost = true;

            Size = new Size(shape.Width, shape.Height);
            Location = new Point(shape.X, shape.Y);
            this.Paint += new PaintEventHandler(Overlay_Paint);
            Thread hide = new Thread(new ThreadStart(Hide_Tick));
            hide.Start();
        }

        private void Overlay_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(TransparencyKey);
            Pen pen = new Pen(Color.Red, 5);
            g.DrawRectangle(pen, 0, 0, ClientSize.Width, ClientSize.Height);
            g.Dispose();
        }

        private void Hide_Tick()
        {
            Thread.Sleep(1000);
            this.Invoke(new HideForm(Hide));
        }

        /// <summary>
        /// Checks if the overlay is the same as the given shape
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public bool shapeEquals(RectangleWrap shape)
        {
            return _shape.Equals(shape);
        }
    }
}
