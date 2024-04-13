

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.ShippingMethodRepository
{
    public interface IShippingMethod
    {
        Task<ShippingMethod> AddShippingMethodAsync(ShippingMethod shippingMethod);
        Task<ShippingMethod> UpdateShippingMethodAsync(ShippingMethod shippingMethod);
        Task<ShippingMethod> DeleteShippingMethodByIdAsync(Guid shippingMethodId);
        Task<ShippingMethod> GetShippingMethodByIdAsync(Guid shippingMethodId);
        Task<IEnumerable<ShippingMethod>> GetAllShippingMethodAsync();
        Task<ShippingMethod> UpsertAsync(ShippingMethod shippingMethod);
        Task SaveChangesAsync();
    }
}
