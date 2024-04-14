using Ecommerce.Data.Models.Entities;


namespace Ecommerce.Repository.Repositories.ShopOrderRepository
{
    public interface IShopOrder
    {
        Task<ShopOrder> AddShopOrderAsync(ShopOrder shopOrder);
        Task<ShopOrder> UpdateShopOrderAsync(ShopOrder shopOrder);
        Task<ShopOrder> DeleteShopOrderByIdAsync(Guid shopOrderId);
        Task<ShopOrder> GetShopOrderByIdAsync(Guid shopOrderId);
        Task<IEnumerable<ShopOrder>> GetAllShopOrdersAsync();
        Task<IEnumerable<ShopOrder>> GetAllShopOrdersByDateAsync(DateTime orderDate);
        Task<IEnumerable<ShopOrder>> GetAllShopOrdersByUserUsernameOrEmailAsync(string usernameOrEmail);
        Task<IEnumerable<ShopOrder>> GetAllShopOrdersByAddressIdAsync(Guid shippingAddressId);
        Task<IEnumerable<ShopOrder>> GetAllShopOrdersByPaymentMethodIdAsync(Guid paymentMethodId);
        Task<IEnumerable<ShopOrder>> GetAllShopOrdersByShippingMethodIdAsync(Guid shippingMethodId);
        Task<IEnumerable<ShopOrder>> GetAllShopOrdersByOrderPriceAsync(decimal orderTotlaPrice);
        Task SaveChangesAsync();
        Task<ShopOrder> UpsertAsync(ShopOrder shopOrder);
    }
}
