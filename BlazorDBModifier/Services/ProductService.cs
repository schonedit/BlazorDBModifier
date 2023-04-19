using BlazorDBModifier.Data;
using BlazorDBModifier.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorDBModifier.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<Product>> CreateProductAsync(Product newProduct)
        {
            var response = new ServiceResponse<Product>();

            try
            {
                if (newProduct != null)
                {
                    await _context.Products.AddAsync(newProduct);
                    await _context.SaveChangesAsync();

                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Product is null";
                }
               
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Kitörli a Product-ot a paraméterbe kapott id szerint.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<Product>> DeleteProductAsync(int id)
        {
            var response = new ServiceResponse<Product>();

            try
            {
                var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();

                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Product not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Módosítja a paraméterben megadott Productot
        /// </summary>
        /// <param name="updatedProduct"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<Product>> UpdateProductAsync(Product updatedProduct)
        {
            var response = new ServiceResponse<Product>();

            try
            {
                var product = await _context.Products.Where(x => x.Id == updatedProduct.Id).FirstOrDefaultAsync();
                if (product != null)
                {
                    product.Price = updatedProduct.Price;
                    product.Title = updatedProduct.Title;
                    product.Description = updatedProduct.Description;

                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();

                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Product not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Vissza adja a Product-ot id alapján.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<Product>> GetProductByIdAsync(int id)
        {
            var response = new ServiceResponse<Product>();

            try
            {
                var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (product != null)
                {
                    response.Data = product;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Product not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Listába vissza adja az összes Product-ot.
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products.ToListAsync()
            };
            return response;
        }

    }
}
