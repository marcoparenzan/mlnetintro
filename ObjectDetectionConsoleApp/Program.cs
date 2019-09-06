﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Microsoft.ML;
using ObjectDetection.DataStructures;
using ObjectDetection.TinyYolo2;
//using ObjectDetection.Yolo3;
//using ObjectDetection.Custom;

namespace ObjectDetection
{
    class Program
    {
        public static void Main()
        {
            var assetsRelativePath = @"../../../assets";
            var assetsPath = GetAbsolutePath(assetsRelativePath);
            var modelFilePath = Path.Combine(assetsPath, "Model", "TinyYolo2_model.onnx");
            //var modelFilePath = Path.Combine(assetsPath, "Model", "Yolov3.onnx");
            //var modelFilePath = Path.Combine(assetsPath, "Model", "model.onnx");
            var imagesFolder = Path.Combine(assetsPath, "people");
            var outputFolder = Path.Combine(imagesFolder, "output");

            // Initialize MLContext
            var mlContext = new MLContext();

            try
            {
                // Load Data
                var images = ImageNetData.ReadFromFile(imagesFolder);
                var imageDataView = mlContext.Data.LoadFromEnumerable(images);

                // Create instance of model scorer
                var modelScorer = new Scorer(imagesFolder, modelFilePath, mlContext);

                // Use model to score data
                var probabilities = modelScorer.Score(imageDataView);

                // Post-process model output
                var parser = new OutputParser();

                var boundingBoxes =
                    probabilities
                    .Select(probability => parser.ParseOutputs(probability))
                    .Select(boxes => parser.FilterBoundingBoxes(boxes, 5, .5F))
                    .ToList()
                ;

                // Draw bounding boxes for detected objects in each of the images
                for (var i = 0; i < images.Count(); i++)
                {
                    var imageFileName = images.ElementAt(i).Label;
                    var detectedObjects = boundingBoxes.ElementAt(i);

                    DrawBoundingBox(imagesFolder, outputFolder, imageFileName, detectedObjects);

                    LogDetectedObjects(imageFileName, detectedObjects);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("========= End of Process..Hit any Key ========");
            Console.ReadLine();
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        private static void DrawBoundingBox(string inputImageLocation, string outputImageLocation, string imageName, IList<BoundingBox> filteredBoundingBoxes)
        {
            Image image = Image.FromFile(Path.Combine(inputImageLocation, imageName));

            var originalImageHeight = image.Height;
            var originalImageWidth = image.Width;

            foreach (var box in filteredBoundingBoxes)
            {
                // Get Bounding Box Dimensions
                var x = (uint)Math.Max(box.Dimensions.X, 0);
                var y = (uint)Math.Max(box.Dimensions.Y, 0);
                var width = (uint)Math.Min(originalImageWidth - x, box.Dimensions.Width);
                var height = (uint)Math.Min(originalImageHeight - y, box.Dimensions.Height);

                // Resize To Image
                x = (uint)originalImageWidth * x / Scorer.ImageNetSettings.imageWidth;
                y = (uint)originalImageHeight * y / Scorer.ImageNetSettings.imageHeight;
                width = (uint)originalImageWidth * width / Scorer.ImageNetSettings.imageWidth;
                height = (uint)originalImageHeight * height / Scorer.ImageNetSettings.imageHeight;

                // Bounding Box Text
                string text = $"{box.Label} ({(box.Confidence * 100).ToString("0")}%)";

                using (Graphics thumbnailGraphic = Graphics.FromImage(image))
                {
                    thumbnailGraphic.CompositingQuality = CompositingQuality.HighQuality;
                    thumbnailGraphic.SmoothingMode = SmoothingMode.HighQuality;
                    thumbnailGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    // Define Text Options
                    Font drawFont = new Font("Arial", 12, FontStyle.Bold);
                    SizeF size = thumbnailGraphic.MeasureString(text, drawFont);
                    SolidBrush fontBrush = new SolidBrush(Color.Black);
                    Point atPoint = new Point((int)x, (int)y - (int)size.Height - 1);

                    // Define BoundingBox options
                    Pen pen = new Pen(box.BoxColor, 3.2f);
                    SolidBrush colorBrush = new SolidBrush(box.BoxColor);

                    // Draw text on image 
                    thumbnailGraphic.FillRectangle(colorBrush, (int)x, (int)(y - size.Height - 1), (int)size.Width, (int)size.Height);
                    thumbnailGraphic.DrawString(text, drawFont, fontBrush, atPoint);

                    // Draw bounding box on image
                    thumbnailGraphic.DrawRectangle(pen, x, y, width, height);
                }
            }

            if (!Directory.Exists(outputImageLocation))
            {
                Directory.CreateDirectory(outputImageLocation);
            }

            image.Save(Path.Combine(outputImageLocation, imageName));
        }

        private static void LogDetectedObjects(string imageName, IList<BoundingBox> boundingBoxes)
        {
            Console.WriteLine($".....The objects in the image {imageName} are detected as below....");

            foreach (var box in boundingBoxes)
            {
                Console.WriteLine($"{box.Label} and its Confidence score: {box.Confidence}");
            }

            Console.WriteLine("");
        }
    }
}