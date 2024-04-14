
using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.OrderStatusRepository
{
    public class OrderStatusRepository : IOrderStatus
    {
        private readonly ApplicationDbContext _dbContext;
        public OrderStatusRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<OrderStatus> AddOrderStatusAsync(OrderStatus orderStatus)
        {
            try
            {
                await _dbContext.OrderStatus.AddAsync(orderStatus);
                await SaveChangesAsync();
                return orderStatus;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderStatus> DeleteOrderStatusByIdAsync(Guid orderStatusId)
        {
            try
            {
                OrderStatus orderStatus = await GetOrderStatusByIdAsync(orderStatusId);
                _dbContext.OrderStatus.Remove(orderStatus);
                await SaveChangesAsync();
                return orderStatus;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<OrderStatus>> GetAllOrdersStatusAsync()
        {
            try
            {
                return await _dbContext.OrderStatus.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderStatus> GetOrderStatusByIdAsync(Guid orderStatusId)
        {
            try
            {
                OrderStatus? orderStatus = await _dbContext.OrderStatus
                    .Where(e => e.Id == orderStatusId).FirstOrDefaultAsync();
                return orderStatus;
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

        public async Task<OrderStatus> UpdteOrderStatusAsync(OrderStatus orderStatus)
        {
            try
            {
                OrderStatus orderStatus1 = await GetOrderStatusByIdAsync(orderStatus.Id);
                orderStatus1.Status = orderStatus.Status;
                await SaveChangesAsync();
                return orderStatus1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderStatus> UpsertAsync(OrderStatus orderStatus)
        {
            try
            {
                OrderStatus orderStatus1 = await GetOrderStatusByIdAsync(orderStatus.Id);
                if (orderStatus1 == null)
                {
                    return await AddOrderStatusAsync(orderStatus);
                }
                return await UpdteOrderStatusAsync(orderStatus);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
