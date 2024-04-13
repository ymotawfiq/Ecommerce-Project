

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.ShoppingCartItemRepository
{
    public interface IShoppingCartItem
    {
        public Task<ShoppingCartItem> AddShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);
        public Task<ShoppingCartItem> UpdateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);
        public Task<ShoppingCartItem> DeleteShoppingCartItemByIdAsync(Guid shoppingCartItemId);
        public Task<ShoppingCartItem> GetShoppingCartItemByIdAsync(Guid shoppingCartItemId);
        public Task<IEnumerable<ShoppingCartItem>> GetAllShoppingCartItemsAsync();
        public Task<IEnumerable<ShoppingCartItem>> GetAllShoppingCartItemsByCartIdAsync(Guid cartId);
        public Task SaveChangesAsync();
        public Task<ShoppingCartItem> UpsertAsync(ShoppingCartItem shoppingCartItem);

    }
}
