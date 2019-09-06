using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ML;

[assembly: FunctionsStartup(typeof(HeartDiseaseApp.Startup))]

namespace HeartDiseaseApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            var mlContext = new MLContext();
            var trainedModel = mlContext.Model.Load("HeartDeseaseModel.zip",out DataViewSchema schema);
            var predictionEngine = mlContext.Model.CreatePredictionEngine<HeartData, HeartPrediction>(trainedModel);
            builder.Services.AddSingleton(mlContext);
            builder.Services.AddSingleton(predictionEngine);
        }
    }
}
