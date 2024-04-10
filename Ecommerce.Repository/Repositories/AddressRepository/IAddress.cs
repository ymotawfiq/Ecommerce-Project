

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.AddressRepository
{
    public interface IAddress
    {
        public Task<Address> AddAddressAsync(Address address);
        public Task<Address> UpdateAddressAsync(Address address);
        public Task<Address> GetAddressByIdAsync(Guid addressId);
        public Task<Address> DeleteAddressByIdAsync(Guid addressId);
        Task<IEnumerable<Address>> GetAllAddressesAsync();
        Task<IEnumerable<Address>> GetAllAddressesByCountaryIdAsync(Guid countaryId);
        public Task SaveChangesAsync();
        public Task<Address> UpsertAsync(Address address);
    }
}
