using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDetectionWinForms.MLSessions
{
    public class Prediction
    {
        public DateTime T { get; set; }
        public float C { get; set; }
        public float CE{ get; set; }
        public float F { get; set; }
        public float FE { get; set; }
        public double[] CScore { get; set; }
        public double[] CEScore { get; set; }
        public double[] FScore { get; set; }
        public double[] FEScore { get; set; }
    }
}
