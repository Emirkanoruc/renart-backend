using System.Text.Json;
using RenartApi.Models;

namespace RenartApi.Services
{
    public class ProductService
    {
        private readonly string _jsonPath = Path.Combine("Data", "product.json");
        private readonly GoldPriceService _goldPriceService;

        public ProductService()
        {
            _goldPriceService = new GoldPriceService();
        }

        public async Task<List<ProductResponse>> GetProductsAsync()
        {
            double goldPricePerGram = await _goldPriceService.GetGoldPricePerGramAsync();

            var json = await File.ReadAllTextAsync(_jsonPath);
            var products = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var result = new List<ProductResponse>();

            foreach (var p in products)
            {
                var price = (p.PopularityScore + 1) * p.Weight * goldPricePerGram;

                result.Add(new ProductResponse
                {
                    Name = p.Name,
                    Price = Math.Round(price, 2),
                    PopularityOutOf5 = Math.Round(p.PopularityScore * 5, 1),
                    Images = p.Images
                });
            }

            return result;
        }
    }
}
