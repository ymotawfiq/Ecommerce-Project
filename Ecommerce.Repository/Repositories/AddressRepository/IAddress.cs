

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.AddressRepository
{
    public interface IAddress
    {
        public Address AddAddress(Address address);
        public Address UpdateAddress(Address address);
        public Address GetAddressById(Guid addressId);
        public Address DeleteAddressById(Guid addressId);
        IEnumerable<Address> GetAllAddresses();
        IEnumerable<Address> GetAllAddressesByCountaryId(Guid countaryId);
        public void SaveChanges();
        public Address Upsert(Address address);
    }
}
