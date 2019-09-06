using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.ML;

namespace HeartDiseaseApp
{
    public class DeseasePrediction
    {
        private readonly HttpClient httpClient;
        private readonly MLContext mlContext;
        private readonly PredictionEngine<HeartData, HeartPrediction> predictionEngine;

        public DeseasePrediction(IHttpClientFactory httpClientFactory, MLContext mlContext, PredictionEngine<HeartData, HeartPrediction> predictionEngine)
        {
            this.httpClient = httpClientFactory.CreateClient();
            this.mlContext = mlContext;
            this.predictionEngine = predictionEngine;
        }

        [FunctionName("DeseasePrediction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var json = await new StreamReader(req.Body).ReadToEndAsync();
            var heartData = JsonConvert.DeserializeObject<HeartData>(json);
            log.LogInformation(json);

            var prediction = predictionEngine.Predict(heartData);

            return new JsonResult(prediction);
        }
    }
}
