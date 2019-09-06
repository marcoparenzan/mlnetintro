using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace AnomalyDetectionWinForms
{
    public class DrawingBitmap
    {
        private Color transparent;

        private Bitmap buffer;
        private Graphics graphics;

        private Point p1;
        public Bitmap Current => buffer;
        public Graphics Graphics => graphics;

        public bool IsTransparent(Color color) => color.ToArgb() == transparent.ToArgb();

        public DrawingBitmap()
        {
            this.transparent = Color.White;
        }

        public DrawingBitmap Buffer(int x, int y, Color? transparent = null)
        {
            // backed image
            buffer = new Bitmap(x, y, PixelFormat.Format32bppArgb);
            graphics = Graphics.FromImage(buffer);
            Clear(transparent);
            return this;
        }

        public void Clear(Color? transparent = null)
        {
            if (transparent != null) this.transparent = transparent.Value;
            graphics.Clear(this.transparent);
        }

        public DrawingBitmap MoveTo(Point p)
        {
            p1 = p;
            return this;
        }

        public DrawingBitmap MoveTo(int x, int y) => MoveTo(new Point(x, y));

        public DrawingBitmap DrawTo(Point p, Pen pen = null)
        {
            graphics.DrawLine(pen ?? Pens.Black, p1, p);
            p1 = p;
            return this;
        }

        public DrawingBitmap DrawTo(int x, int y, Pen pen = null) => DrawTo(new Point(x, y), pen);

        public Size Size => Current.Size;
    }
}
