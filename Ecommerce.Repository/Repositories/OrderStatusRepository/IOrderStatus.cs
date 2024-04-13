

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.OrderStatusRepository
{
    public interface IOrderStatus
    {
        Task<OrderStatus> AddOrderStatusAsync(OrderStatus orderStatus);
        Task<OrderStatus> UpdteOrderStatusAsync(OrderStatus orderStatus);
        Task<OrderStatus> DeleteOrderStatusByIdAsync(Guid orderStatusId);
        Task<OrderStatus> GetOrderStatusByIdAsync(Guid orderStatusId);
        Task<OrderStatus> UpsertAsync(OrderStatus orderStatus);
        Task<IEnumerable<OrderStatus>> GetAllOrdersStatusAsync();
        Task SaveChangesAsync();
    }
}
