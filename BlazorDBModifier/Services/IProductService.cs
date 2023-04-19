using BlazorDBModifier.Models;

namespace BlazorDBModifier.Services
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();

        Task<ServiceResponse<Product>> GetProductByIdAsync(int id);

        Task<ServiceResponse<Product>> CreateProductAsync(Product newProduct);

        Task<ServiceResponse<Product>> DeleteProductAsync(int id);

        Task<ServiceResponse<Product>> UpdateProductAsync(Product updatedProduct);

    }
}
