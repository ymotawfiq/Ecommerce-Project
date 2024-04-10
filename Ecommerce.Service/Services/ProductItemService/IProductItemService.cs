
using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.ProductItemService
{
    public interface IProductItemService
    {
        Task<ApiResponse<IEnumerable<ProductItem>>> GetAllItemsAsync();
        Task<ApiResponse<IEnumerable<ProductItem>>> GetAllItemsByProductIdAsync(Guid productId);
        Task<ApiResponse<ProductItem>> AddItemAsync(ProductItemDto productItemDto);
        Task<ApiResponse<ProductItem>> UpdateItemAsync(ProductItemDto productItemDto);
        Task<ApiResponse<ProductItem>> DeleteItemAsync(Guid itemId);
        Task<ApiResponse<ProductItem>> GetItemByIdAsync(Guid itemId);
    }
}
