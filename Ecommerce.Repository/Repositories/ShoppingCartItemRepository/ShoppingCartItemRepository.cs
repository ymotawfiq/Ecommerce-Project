

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Ecommerce.Repository.Repositories.ShoppingCartRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.ShoppingCartItemRepository
{
    public class ShoppingCartItemRepository : IShoppingCartItem
    {
        private readonly ApplicationDbContext _dbContext;

        public ShoppingCartItemRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<ShoppingCartItem> AddShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
        {
            try
            {
                await _dbContext.ShoppingCartItem.AddAsync(shoppingCartItem);
                await SaveChangesAsync();
                return shoppingCartItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ShoppingCartItem> DeleteShoppingCartItemByIdAsync(Guid shoppingCartItemId)
        {
            try
            {
                ShoppingCartItem shoppingCartItem = await GetShoppingCartItemByIdAsync(shoppingCartItemId);
                _dbContext.ShoppingCartItem.Remove(shoppingCartItem);
                await SaveChangesAsync();
                return shoppingCartItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetAllShoppingCartItemsAsync()
        {
            try
            {
                return await _dbContext.ShoppingCartItem.Include(e => e.ProductItem)
                    .Include(e => e.ShoppingCart).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetAllShoppingCartItemsByCartIdAsync(Guid cartId)
        {
            try
            {
                return
                    from s in await GetAllShoppingCartItemsAsync()
                    where s.CartId == cartId
                    select s;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ShoppingCartItem> GetShoppingCartItemByIdAsync(Guid shoppingCartItemId)
        {
            try
            {
                ShoppingCartItem? shoppingCartItem = await _dbContext.ShoppingCartItem
                    .Where(e => e.Id == shoppingCartItemId).FirstOrDefaultAsync();
                return shoppingCartItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ShoppingCartItem> UpdateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
        {
            try
            {
                ShoppingCartItem shoppingCartItem1 = await GetShoppingCartItemByIdAsync(shoppingCartItem.Id);
                shoppingCartItem1.ProductItemId = shoppingCartItem.ProductItemId;
                shoppingCartItem1.Qty = shoppingCartItem.Qty;
                shoppingCartItem1.CartId = shoppingCartItem.CartId;
                await SaveChangesAsync();
                return shoppingCartItem1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ShoppingCartItem> UpsertAsync(ShoppingCartItem shoppingCartItem)
        {
            try
            {
                ShoppingCartItem shoppingCartItem1 = await GetShoppingCartItemByIdAsync(shoppingCartItem.Id);
                if (shoppingCartItem1 == null)
                {
                    return await AddShoppingCartItemAsync(shoppingCartItem);
                }
                return await UpdateShoppingCartItemAsync(shoppingCartItem);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
