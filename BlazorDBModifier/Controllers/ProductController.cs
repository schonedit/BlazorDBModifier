using BlazorDBModifier.Models;
using BlazorDBModifier.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDBModifier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var result = await _productService.GetProductsAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductAsync(Product product)
        {
            var response = await _productService.CreateProductAsync(product);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
