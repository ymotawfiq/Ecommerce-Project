

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.AddressRepository
{
    public class AddressRepository : IAddress
    {
        private readonly ApplicationDbContext _dbContext;
        public AddressRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public Address AddAddress(Address address)
        {
            try
            {
                _dbContext.Address.Add(address);
                SaveChanges();
                return address;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Address DeleteAddressById(Guid addressId)
        {
            try
            {
                Address address = GetAddressById(addressId);
                _dbContext.Address.Remove(address);
                SaveChanges();
                return address;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Address GetAddressById(Guid addressId)
        {
            try
            {
                Address? address = _dbContext.Address.Where(e => e.Id == addressId).FirstOrDefault();
                if (address == null)
                {
                    return null;
                }
                return address;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            try
            {
                return _dbContext.Address.Include(e=>e.Countary).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Address> GetAllAddressesByCountaryId(Guid countaryId)
        {
            try
            {
                return
                    from a in GetAllAddresses()
                    where a.CountaryId == countaryId
                    select a;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public Address UpdateAddress(Address address)
        {
            try
            {
                Address address1 = GetAddressById(address.Id);
                address1.AddressLine1 = address.AddressLine1;
                address1.AddressLine2 = address.AddressLine2;
                address1.City = address.City;
                address1.CountaryId = address.CountaryId;
                address1.PostalCode = address.PostalCode;
                address1.Region = address.Region;
                address1.StreetNumber = address.StreetNumber;
                address1.UnitNumber = address.UnitNumber;
                SaveChanges();
                return address1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Address Upsert(Address address)
        {
            try
            {
                Address address1 = GetAddressById(address.Id);
                if (address1 == null)
                {
                    return AddAddress(address);
                }
                return UpdateAddress(address);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
