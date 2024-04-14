

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.OrderLineRepository
{
    public interface IOrderLine
    {
        Task<OrderLine> AddOrderLineAsync(OrderLine orderLine);
        Task<OrderLine> UpdateOrderLineAsync(OrderLine orderLine);
        Task<OrderLine> DeleteOrderLineByIdAsync(Guid orderLineId);
        Task<OrderLine> GetOrderLineByIdAsync(Guid orderLineId);
        Task<OrderLine> UpsertAsync(OrderLine orderLine);
        Task SaveChangesAsync();
        Task<IEnumerable<OrderLine>> GetAllOrderLinesAsync();
        Task<IEnumerable<OrderLine>> GetAllOrderLinesByProductItemIdAsync(Guid productItemId);
        Task<IEnumerable<OrderLine>> GetAllOrderLinesByShopOrderIdAsync(Guid shopOrderId);
    }
}
