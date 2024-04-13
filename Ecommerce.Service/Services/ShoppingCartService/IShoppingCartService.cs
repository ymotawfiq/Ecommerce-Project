
using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.ShoppingCartService
{
    public interface IShoppingCartService
    {
        public Task<ApiResponse<ShoppingCart>> AddShoppingCartAsync(ShoppingCartDto shoppingCartDto);
        public Task<ApiResponse<ShoppingCart>> UpdateShoppingCartAsync(ShoppingCartDto shoppingCartDto);
        public Task<ApiResponse<ShoppingCart>> DeleteShoppingCartByIdAsync(Guid shoppingCartId);
        public Task<ApiResponse<ShoppingCart>> GetShoppingCartByIdAsync(Guid shoppingCartId);
        public Task<ApiResponse<IEnumerable<ShoppingCart>>> GeAllShoppingCartsAsync();
        public Task<ApiResponse<IEnumerable<ShoppingCart>>> GeAllShoppingCartsByUserIdAsync(string userId);
        public Task<ApiResponse<IEnumerable<ShoppingCart>>> GeAllShoppingCartsByUsernameOrEmailAsync(string usernameOrEmail);
    }
}
