

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.OrderLineRepository
{
    public class OrderLineRepository : IOrderLine
    {
        private readonly ApplicationDbContext _dbContext;
        public OrderLineRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<OrderLine> AddOrderLineAsync(OrderLine orderLine)
        {
            try
            {
                await _dbContext.OrderLine.AddAsync(orderLine);
                await SaveChangesAsync();
                return orderLine;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderLine> DeleteOrderLineByIdAsync(Guid orderLineId)
        {
            try
            {
                OrderLine orderLine = await GetOrderLineByIdAsync(orderLineId);
                _dbContext.OrderLine.Remove(orderLine);
                await SaveChangesAsync();
                return orderLine;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<OrderLine>> GetAllOrderLinesAsync()
        {
            try
            {
                return await _dbContext.OrderLine.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<OrderLine>> GetAllOrderLinesByProductItemIdAsync(Guid productItemId)
        {
            try
            {
                return
                    from u in await GetAllOrderLinesAsync()
                    where u.ProductItemId == productItemId
                    select u;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<OrderLine>> GetAllOrderLinesByShopOrderIdAsync(Guid shopOrderId)
        {
            try
            {
                return
                    from u in await GetAllOrderLinesAsync()
                    where u.ShopOrderId == shopOrderId
                    select u;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderLine> GetOrderLineByIdAsync(Guid orderLineId)
        {
            try
            {
                OrderLine? orderLine = await _dbContext.OrderLine
                    .Where(e => e.Id == orderLineId).FirstOrDefaultAsync();
                return orderLine;
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

        public async Task<OrderLine> UpdateOrderLineAsync(OrderLine orderLine)
        {
            try
            {
                OrderLine orderLine1 = await GetOrderLineByIdAsync(orderLine.Id);
                orderLine1.Price = orderLine.Price;
                orderLine1.ProductItemId = orderLine.ProductItemId;
                orderLine1.Qty = orderLine.Qty;
                orderLine1.ShopOrderId = orderLine.ShopOrderId;
                await SaveChangesAsync();
                return orderLine1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderLine> UpsertAsync(OrderLine orderLine)
        {
            try
            {
                OrderLine orderLine1 = await GetOrderLineByIdAsync(orderLine.Id);
                if(orderLine1 == null)
                {
                    return await AddOrderLineAsync(orderLine);
                }
                return await UpdateOrderLineAsync(orderLine);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
