﻿using Microsoft.ML.Transforms.Image;
using System.Drawing;

namespace OnnxObjectDetectionApp
{
    public struct ImageSettings
    {
        public const int imageHeight = 416;
        public const int imageWidth = 416;
    }

    public class ImageInputData
    {
        [ImageType(ImageSettings.imageHeight, ImageSettings.imageWidth)]
        public Bitmap Image { get; set; }
    }
}
