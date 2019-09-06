using Microsoft.ML.Data;
using System;

namespace AnomalyDetectionWinForms.MLSessions
{
    public class Features
    {
        [LoadColumn(0)]
        public DateTime T { get; set; }
        [LoadColumn(1)]
        public float C { get; set; }
        [LoadColumn(2)]
        public float CE { get; set; }
        [LoadColumn(3)]
        public float F { get; set; }
        [LoadColumn(4)]
        public float FE { get; set; }
        [LoadColumn(5)]
        public float Tot { get; set; }
    }
}
