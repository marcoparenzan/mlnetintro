using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HeartDeseaseTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(HeartSampleData.heartDataList[0]);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("http://localhost:7071/api/DeseasePrediction", content);

            var jsonResponse = await response.Content.ReadAsStringAsync();
        }
    }
}
