﻿using System.Drawing;

namespace ObjectDetection.TinyYolo2
{
    public class BoundingBoxDimensions: DimensionsBase { }

    public class BoundingBox
    {
        public BoundingBoxDimensions Dimensions { get; set; }

        public string Label { get; set; }

        public float Confidence { get; set; }

        public RectangleF Rect
        {
            get { return new RectangleF(Dimensions.X, Dimensions.Y, Dimensions.Width, Dimensions.Height); }
        }

        public Color BoxColor { get; set; }
    }
    
}