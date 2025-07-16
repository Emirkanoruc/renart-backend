using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RenartApi.Services
{
    public class GoldPriceService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://api.gold-api.com/price/XAU";  // Api Url 

        public GoldPriceService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<double> GetGoldPricePerGramAsync()
        {
            var response = await _httpClient.GetAsync(ApiUrl);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Altın fiyatı API'sinden veri alınamadı.");

            var content = await response.Content.ReadAsStringAsync();

            using var jsonDoc = JsonDocument.Parse(content);

            var pricePerOunce = jsonDoc.RootElement.GetProperty("price").GetDouble();  // Ons fiyatı

            const double gramsPerOunce = 31.1035;    // 1 ons= 31.1035  gram ediyor.

            double pricePerGram = pricePerOunce / gramsPerOunce;     // 1 gram fiyatı

            return Math.Round(pricePerGram, 2);

        }
    }
}
