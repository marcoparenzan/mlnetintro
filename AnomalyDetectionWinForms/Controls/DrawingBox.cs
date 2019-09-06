using System;
using System.Drawing;
using System.Windows.Forms;

namespace AnomalyDetectionWinForms
{
    public class DrawingBox: Control
    {
        public DrawingBitmap Bitmap { get; private set; } = new DrawingBitmap();

        private float dx, dy;
        public Point Map(Point set) => new Point((int)(set.X / dx), (int)(set.Y / dy));

        public DrawingBox()
        {
            Bitmap.Buffer(24 * 60, 1000);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            dx = (float)this.DisplayRectangle.Width / (float)Bitmap.Size.Width;
            dy = (float)this.DisplayRectangle.Height / (float)Bitmap.Size.Height;
            Repaint();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Repaint();
        }

        private void Repaint()
        {
            using (var myBuffer = BufferedGraphicsManager.Current.Allocate(this.CreateGraphics(), this.DisplayRectangle))
            {
                myBuffer.Graphics.DrawImage(Bitmap.Current, this.DisplayRectangle);
                myBuffer.Render();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.IsLeftButtonDown())
            {
                Bitmap.DrawTo(Map(e.Location));
                Repaint();
            }
            else
            {
                Bitmap.MoveTo(Map(e.Location));
            }
        }
    }
}
