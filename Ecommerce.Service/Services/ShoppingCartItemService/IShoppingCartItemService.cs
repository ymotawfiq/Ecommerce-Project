
using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.ShoppingCartItemService
{
    public interface IShoppingCartItemService
    {
        public Task<ApiResponse<ShoppingCartItem>> AddShoppingCartItemAsync(ShoppingCartItemDto shoppingCartItemDto);
        public Task<ApiResponse<ShoppingCartItem>> UpdateShoppingCartItemAsync(ShoppingCartItemDto shoppingCartItemDto);
        public Task<ApiResponse<ShoppingCartItem>> DeleteShoppingCartItemByIdAsync(Guid shoppingCartItemId);
        public Task<ApiResponse<ShoppingCartItem>> GetShoppingCartItemAsync(Guid shoppingCartItemId);
        public Task<ApiResponse<IEnumerable<ShoppingCartItem>>> GetAllShoppingCartItemsAsync();
        public Task<ApiResponse<IEnumerable<ShoppingCartItem>>> GetAllShoppingCartItemsByCartIdAsync(Guid cartId);

    }
}
