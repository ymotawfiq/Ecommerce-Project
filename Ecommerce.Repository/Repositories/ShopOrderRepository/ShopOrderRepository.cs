

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.ShopOrderRepository
{
    public class ShopOrderRepository : IShopOrder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<SiteUser> _userManager;
        public ShopOrderRepository(ApplicationDbContext _dbContext, UserManager<SiteUser> _userManager)
        {
            this._dbContext = _dbContext;
            this._userManager = _userManager;
        }
        public async Task<ShopOrder> AddShopOrderAsync(ShopOrder shopOrder)
        {
            try
            {
                await _dbContext.ShopOrder.AddAsync(shopOrder);
                await SaveChangesAsync();
                return shopOrder;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ShopOrder> DeleteShopOrderByIdAsync(Guid shopOrderId)
        {
            try
            {
                ShopOrder shopOrder = await GetShopOrderByIdAsync(shopOrderId);
                _dbContext.ShopOrder.Remove(shopOrder);
                await SaveChangesAsync();
                return shopOrder;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShopOrder>> GetAllShopOrdersAsync()
        {
            try
            {
                return await _dbContext.ShopOrder.Include(e=>e.Address).Include(e=>e.OrderStatus)
                    .Include(e=>e.PaymentMethod).Include(e=>e.ShippingMethod).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShopOrder>> GetAllShopOrdersByAddressIdAsync(Guid shippingAddressId)
        {
            try
            {
                return
                    from u in await GetAllShopOrdersAsync()
                    where u.ShippingAddressId == shippingAddressId
                    select u;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShopOrder>> GetAllShopOrdersByDateAsync(DateTime orderDate)
        {
            try
            {
                return
                    from u in await GetAllShopOrdersAsync()
                    where u.OrderDate.Day == orderDate.Day
                            && u.OrderDate.Month == orderDate.Month
                            && u.OrderDate.Year == orderDate.Year
                    select u;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShopOrder>> GetAllShopOrdersByOrderPriceAsync(decimal orderTotlaPrice)
        {
            try
            {
                return
                    from u in await GetAllShopOrdersAsync()
                    where u.OrderTotal == orderTotlaPrice
                    select u;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShopOrder>> GetAllShopOrdersByPaymentMethodIdAsync(Guid paymentMethodId)
        {
            try
            {
                return
                    from u in await GetAllShopOrdersAsync()
                    where u.PaymentMethodId == paymentMethodId
                    select u;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShopOrder>> GetAllShopOrdersByShippingMethodIdAsync(Guid shippingMethodId)
        {
            try
            {
                return
                    from u in await GetAllShopOrdersAsync()
                    where u.ShippingAddressId == shippingMethodId
                    select u;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShopOrder>> GetAllShopOrdersByUserUsernameOrEmailAsync(string usernameOrEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(usernameOrEmail);
                return
                    from u in await GetAllShopOrdersAsync()
                    where u.UserId == user?.Id
                    select u;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ShopOrder> GetShopOrderByIdAsync(Guid shopOrderId)
        {
            try
            {
                ShopOrder? shopOrder = await _dbContext.ShopOrder
                    .Where(e => e.Id == shopOrderId).FirstOrDefaultAsync();
                return shopOrder;
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

        public async Task<ShopOrder> UpdateShopOrderAsync(ShopOrder shopOrder)
        {
            try
            {
                ShopOrder shopOrder1 = await GetShopOrderByIdAsync(shopOrder.Id);
                shopOrder1.OrderDate = shopOrder.OrderDate;
                shopOrder1.OrderStatusId = shopOrder.OrderStatusId;
                shopOrder1.OrderTotal = shopOrder.OrderTotal;
                shopOrder1.PaymentMethodId = shopOrder.PaymentMethodId;
                shopOrder1.ShippingAddressId = shopOrder.ShippingAddressId;
                shopOrder1.ShippingMethodId = shopOrder.ShippingMethodId;
                shopOrder1.UserId = shopOrder.UserId;
                await SaveChangesAsync();
                return shopOrder1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ShopOrder> UpsertAsync(ShopOrder shopOrder)
        {
            try
            {
                ShopOrder shopOrder1 = await GetShopOrderByIdAsync(shopOrder.Id);
                if (shopOrder1 == null)
                {
                    return await AddShopOrderAsync(shopOrder);
                }
                return await UpdateShopOrderAsync(shopOrder);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
