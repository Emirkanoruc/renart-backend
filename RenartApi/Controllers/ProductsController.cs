using Microsoft.AspNetCore.Mvc;
using RenartApi.Services;
using System.Linq;
using System.Threading.Tasks;

namespace RenartApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController()
        {
            _productService = new ProductService();
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] double? minPrice,
            [FromQuery] double? maxPrice,
            [FromQuery] double? minPopularity)
        {
            var products = await _productService.GetProductsAsync();

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value).ToList();
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value).ToList();
            }

            if (minPopularity.HasValue)
            {
                products = products.Where(p => p.PopularityOutOf5 >= minPopularity.Value).ToList();
            }

            return Ok(products);
        }
    }
}
