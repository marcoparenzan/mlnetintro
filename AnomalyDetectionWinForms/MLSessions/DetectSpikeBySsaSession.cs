using Microsoft.ML;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnomalyDetectionWinForms.MLSessions
{
    public class DetectSpikeBySsaSession : IEnumerable<MLSessionParameter>, IMLSession
    {
        private Dictionary<string, MLSessionParameter> parameters;

        private Dictionary<string, MLSessionSerie> series;

        public IEnumerable<MLSessionParameter> Parameters => parameters.Values;

        public IEnumerable<MLSessionSerie> Series => series.Values;

        public DetectSpikeBySsaSession()
        {
            series = new Dictionary<string, MLSessionSerie>
            {
                [nameof(Features.C)] = new MLSessionSerie
                {
                    Label = nameof(Features.C)
                },
                [nameof(Features.CE)] = new MLSessionSerie
                {
                    Label = nameof(Features.CE)
                },
                [nameof(Features.F)] = new MLSessionSerie
                {
                    Label = nameof(Features.F)
                },
                [nameof(Features.FE)] = new MLSessionSerie
                {
                    Label = nameof(Features.FE)
                }
            };

            parameters = new Dictionary<string, MLSessionParameter>
            {
                [nameof(Confidence)] = new MLSessionParameter
                {
                    Label = "Confidence",
                    Range = RangeType.Zero100,
                    Value = 95
                },
                [nameof(PValueHistoryLength)] = new MLSessionParameter
                {
                    Label = "PValueHistoryLength",
                    Range = RangeType.Zero100,
                    Value = 10
                },
                [nameof(TrainingWindowSize)] = new MLSessionParameter
                {
                    Label = "TrainingWindowSize",
                    Range = RangeType.Zero100,
                    Value = 30
                },
                [nameof(SeasonalityWindowSize)] = new MLSessionParameter
                {
                    Label = "SeasonalityWindowSize",
                    Range = RangeType.Zero100,
                    Value = 10
                },
                [nameof(Skip)] = new MLSessionParameter
                {
                    Label = "Skip",
                    Range = RangeType.Zero100,
                    Value = 0
                },
                [nameof(Take)] = new MLSessionParameter
                {
                    Label = "Take",
                    Range = RangeType.Zero1000,
                    Value = 100
                }
            };
        }

        public MLSessionParameter Confidence => Parameter();
        public MLSessionParameter PValueHistoryLength => Parameter();
        public MLSessionParameter TrainingWindowSize => Parameter();
        public MLSessionParameter SeasonalityWindowSize => Parameter();
        public MLSessionParameter Skip => Parameter();
        public MLSessionParameter Take => Parameter();

        public MLSessionParameter Parameter([CallerMemberName] string name = null) => parameters[name];

        IEnumerator<MLSessionParameter> IEnumerable<MLSessionParameter>.GetEnumerator() => parameters.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => parameters.Values.GetEnumerator();


        private MLContext ctx;
        private IDataView data;
        private IDataView trainingData;
        private IDataView testData;

        public string Title { get; private set; }

        public static DetectSpikeBySsaSession New(string filename)
        {
            var session = new DetectSpikeBySsaSession();
            session.Init(filename);
            return session;
        }

        public void Init(string filename)
        {
            Title = $"{filename}";


            ctx = new MLContext();

            data = ctx.Data.LoadFromTextFile<Features>(filename, ';', true, true);

            var split = ctx.Data.TrainTestSplit(data, 0.5);
            var trainingTake = 700; // (long)(data.GetRowCount() * 0.9);
            trainingData = ctx.Data.TakeRows(data, trainingTake); // split.TrainingData
            testData = ctx.Data.SkipRows(data, trainingTake); // split.TestData
        }

        public IEnumerable<Prediction> Predict()
        {
            IEstimator<ITransformer> estimator = ctx.Transforms
                .DropColumns(nameof(Features.Tot));

            estimator = estimator.Append(
                ctx.Transforms
                .DetectSpikeBySsa("CScore", nameof(Features.C), Confidence.Value, PValueHistoryLength.Value, TrainingWindowSize.Value, SeasonalityWindowSize.Value)
            );
            estimator = estimator.Append(
                ctx.Transforms
                .DetectSpikeBySsa("CEScore", nameof(Features.CE), Confidence.Value, PValueHistoryLength.Value, TrainingWindowSize.Value, SeasonalityWindowSize.Value)
            );
            estimator = estimator.Append(
                ctx.Transforms
                .DetectSpikeBySsa("FScore", nameof(Features.F), Confidence.Value, PValueHistoryLength.Value, TrainingWindowSize.Value, SeasonalityWindowSize.Value)
            );
            estimator = estimator.Append(
                ctx.Transforms
                .DetectSpikeBySsa("FEScore", nameof(Features.FE), Confidence.Value, PValueHistoryLength.Value, TrainingWindowSize.Value, SeasonalityWindowSize.Value)
            );

            var model = estimator
                .Fit(trainingData)
            ;

            var output =
                model
                    .Transform(testData)
            ;

            var predictions = ctx
              .Data
              .CreateEnumerable<Prediction>(output, false)
            ;

            return predictions;
        }
    }
}
