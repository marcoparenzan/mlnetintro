using System;
using System.Collections.Generic;
using Microsoft.ML;

namespace HeartDiseaseTrain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var mlContext = new MLContext();

            // Fit==>Create the model==>loop/onetime

            var trainingDataView = mlContext.Data.LoadFromTextFile<HeartData>("HeartTraining.csv", hasHeader: false, separatorChar: ';');
            var pipeline = mlContext.Transforms.Concatenate("Features", "Age", "Sex", "Cp", "TrestBps", "Chol", "Fbs", "RestEcg", "Thalac", "Exang", "OldPeak", "Slope", "Ca", "Thal")
                .Append(mlContext.BinaryClassification.Trainers.FastTree(labelColumnName: "Label", featureColumnName: "Features"));
            ITransformer trainedModel = pipeline.Fit(trainingDataView);
            mlContext.Model.Save(trainedModel, trainingDataView.Schema, "HeartDeseaseModel.zip");

            // Evaluate==>Check the metrics

            var testDataView = mlContext.Data.LoadFromTextFile<HeartData>("HeartTest.csv", hasHeader: true, separatorChar: ';');
            var predictions = trainedModel.Transform(testDataView);
            var metrics = mlContext.BinaryClassification.Evaluate(data: predictions, labelColumnName: "Label", scoreColumnName: "Score");
        }
    }
}
