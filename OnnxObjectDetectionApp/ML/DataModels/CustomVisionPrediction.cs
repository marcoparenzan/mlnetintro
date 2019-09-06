using Microsoft.ML.Data;

namespace OnnxObjectDetectionApp
{
    public class CustomVisionPrediction : IOnnxObjectPrediction
    {
        [ColumnName("model_outputs0")]
        public float[] PredictedLabels { get; set; }
    }
}
