using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.ProductService.ProductService
{
    public interface IProductService
    {
        Task<ApiResponse<Product>> AddProductAsync(ProductDto productDto);
        Task<ApiResponse<Product>> UpdateProductAsync(ProductDto productDto);
        Task<ApiResponse<Product>> DeleteProductAsync(Guid productId);
        Task<ApiResponse<Product>> GetProductByProductIdAsync(Guid productId);
        Task<ApiResponse<IEnumerable<Product>>> GetAllProductsAsync();
        Task<ApiResponse<IEnumerable<Product>>> GetAllProductsByCategoryIdAsync(Guid categoryId);
    }
}
