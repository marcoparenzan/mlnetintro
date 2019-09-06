using System.Collections.Generic;

namespace AnomalyDetectionWinForms.MLSessions
{
    public interface IMLSession
    {
        string Title { get; }
        IEnumerable<MLSessionSerie> Series { get; }
        IEnumerable<MLSessionParameter> Parameters { get; }
        MLSessionParameter Parameter(string name);
        IEnumerable<Prediction> Predict();
    }
}