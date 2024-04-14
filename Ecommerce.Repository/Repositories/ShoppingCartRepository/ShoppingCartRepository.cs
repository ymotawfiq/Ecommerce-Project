

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.ShoppingCartRepository
{
    public class ShoppingCartRepository : IShoppingCart
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<SiteUser> _userManager;
        public ShoppingCartRepository(ApplicationDbContext _dbContext, UserManager<SiteUser> _userManager)
        {
            this._userManager = _userManager;
            this._dbContext = _dbContext;
        }

        public async Task<ShoppingCart> AddShoppingCartAsync(ShoppingCart shoppingCart)
        {
            try
            {
                await _dbContext.ShoppingCart.AddAsync(shoppingCart);
                await SaveChangesAsync();
                return shoppingCart;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ShoppingCart> DeleteShoppingCartByIdAsync(Guid shoppingCartId)
        {
            try
            {
                ShoppingCart shoppingCart = await GetShoppingCartByIdAsync(shoppingCartId);
                _dbContext.ShoppingCart.Remove(shoppingCart);
                await SaveChangesAsync();
                return shoppingCart;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsAsync()
        {
            try
            {
                return await _dbContext.ShoppingCart.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsByUserEmailAsync(string usernameOrEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(usernameOrEmail);
                return
                    await GetAllShoppingCartsByUserIdAsync(user.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsByUserIdAsync(string userId)
        {
            try
            {
                return
                    from u in await GetAllShoppingCartsAsync()
                    where u.UserId.ToString() == userId
                    select u;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ShoppingCart> GetShoppingCartByIdAsync(Guid shoppingCartId)
        {
            try
            {
                ShoppingCart? shoppingCart = await _dbContext.ShoppingCart
                    .Where(e => e.Id == shoppingCartId).FirstOrDefaultAsync();
                return shoppingCart;
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

        public async Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart shoppingCart)
        {
            try
            {
                ShoppingCart shoppingCart1 = await GetShoppingCartByIdAsync(shoppingCart.Id);
                shoppingCart1.UserId = shoppingCart.UserId;
                await SaveChangesAsync();
                return shoppingCart1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ShoppingCart> UpsertAsync(ShoppingCart shoppingCart)
        {
            try
            {
                ShoppingCart shoppingCart1 = await GetShoppingCartByIdAsync(shoppingCart.Id);
                if (shoppingCart1 == null)
                {
                    return await AddShoppingCartAsync(shoppingCart);
                }
                return await UpdateShoppingCartAsync(shoppingCart);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
