

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.ShoppingCartRepository
{
    public interface IShoppingCart
    {
        public Task<ShoppingCart> AddShoppingCartAsync(ShoppingCart shoppingCart);
        public Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart shoppingCart);
        public Task<ShoppingCart> DeleteShoppingCartByIdAsync(Guid shoppingCartId);
        public Task<ShoppingCart> GetShoppingCartByIdAsync(Guid shoppingCartId);
        public Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsAsync();
        public Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsByUserIdAsync(string userId);
        public Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsByUserEmailAsync(string usernameOrEmail);
        public Task SaveChangesAsync();
        public Task<ShoppingCart> UpsertAsync(ShoppingCart shoppingCart);


    }
}
