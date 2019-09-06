using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using ObjectDetection.DataStructures;

namespace ObjectDetection.Yolo3
{
    class Scorer
    {
        private readonly string imagesFolder;
        private readonly string modelLocation;
        private readonly MLContext mlContext;

        private IList<BoundingBox> _boundingBoxes = new List<BoundingBox>();

        public Scorer(string imagesFolder, string modelLocation, MLContext mlContext)
        {
            this.imagesFolder = imagesFolder;
            this.modelLocation = modelLocation;
            this.mlContext = mlContext;
        }

        public struct ImageNetSettings
        {
            public const int imageHeight = 416;
            public const int imageWidth = 416;
        }

        public struct Settings
        {
            // for checking Tiny yolo2 Model input and  output  parameter names,
            //you can use tools like Netron, 
            // which is installed by Visual Studio AI Tools

            // input tensor name
            public const string ImageInput = "input_1";
            public const string ImageSizeInput = "image_shape";

            // output tensor name
            public const string Output1 = "yolonms_layer_1/ExpandDims_1:0";
            public const string Output2 = "yolonms_layer_1/ExpandDims_3:0";
            public const string Output3 = "yolonms_layer_1/concat_2:0";
        }

        private ITransformer LoadModel(string modelLocation)
        {
            Console.WriteLine("Read model");
            Console.WriteLine($"Model location: {modelLocation}");
            Console.WriteLine($"Default parameters: image size=({ImageNetSettings.imageWidth},{ImageNetSettings.imageHeight})");

            // Create IDataView from empty list to obtain input data schema
            var data = mlContext.Data.LoadFromEnumerable(new List<ImageNetData>());

            // Define scoring pipeline
            var pipeline = mlContext.Transforms
                .LoadImages(outputColumnName: $"{Settings.ImageInput}Original", imageFolder: "", inputColumnName: nameof(ImageNetData.ImagePath))
                .Append(mlContext.Transforms.ResizeImages(outputColumnName: $"{Settings.ImageInput}Scaled", imageWidth: ImageNetSettings.imageWidth, imageHeight: ImageNetSettings.imageHeight, inputColumnName: $"{Settings.ImageInput}Original"))
                .Append(mlContext.Transforms.ExtractPixels(outputColumnName: Settings.ImageInput, $"{Settings.ImageInput}Scaled"))
                .Append(mlContext.Transforms.ApplyOnnxModel(modelFile: modelLocation, outputColumnNames: new[] { Settings.Output1, Settings.Output2, Settings.Output3 }, inputColumnNames: new[] { Settings.ImageInput }))
            ;

            // Fit scoring pipeline
            var model = pipeline.Fit(data);

            return model;
        }

        private IEnumerable<float[]> PredictDataUsingModel(IDataView testData, ITransformer model)
        {
            Console.WriteLine($"Images location: {imagesFolder}");
            Console.WriteLine("");
            Console.WriteLine("=====Identify the objects in the images=====");
            Console.WriteLine("");

            IDataView scoredData = model.Transform(testData);

            IEnumerable<float[]> probabilities = scoredData.GetColumn<float[]>(Settings.Output1);

            return probabilities;
        }

        public IEnumerable<float[]> Score(IDataView data)
        {
            var model = LoadModel(modelLocation);

            return PredictDataUsingModel(data, model);
        }
    }
}

