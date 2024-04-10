

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
        public async Task<Address> AddAddressAsync(Address address)
        {
            try
            {
                await _dbContext.Address.AddAsync(address);
                await SaveChangesAsync();
                return address;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Address> DeleteAddressByIdAsync(Guid addressId)
        {
            try
            {
                Address address = await GetAddressByIdAsync(addressId);
                _dbContext.Address.Attach(address);
                _dbContext.Address.Remove(address);
                await SaveChangesAsync();
                return address;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Address> GetAddressByIdAsync(Guid addressId)
        {
            try
            {
                Address? address = await _dbContext.Address.Where(e => e.Id == addressId)
                    .FirstOrDefaultAsync();
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

        public async Task<IEnumerable<Address>> GetAllAddressesAsync()
        {
            try
            {
                return await _dbContext.Address.Include(e=>e.Countary).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Address>> GetAllAddressesByCountaryIdAsync(Guid countaryId)
        {
            try
            {
                return
                    from a in await GetAllAddressesAsync()
                    where a.CountaryId == countaryId
                    select a;
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

        public async Task<Address> UpdateAddressAsync(Address address)
        {
            try
            {
                Address address1 = await GetAddressByIdAsync(address.Id);
                address1.AddressLine1 = address.AddressLine1;
                address1.AddressLine2 = address.AddressLine2;
                address1.City = address.City;
                address1.CountaryId = address.CountaryId;
                address1.PostalCode = address.PostalCode;
                address1.Region = address.Region;
                address1.StreetNumber = address.StreetNumber;
                address1.UnitNumber = address.UnitNumber;
                SaveChangesAsync();
                return address1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Address> UpsertAsync(Address address)
        {
            try
            {
                Address address1 = await GetAddressByIdAsync(address.Id);
                if (address1 == null)
                {
                    return await AddAddressAsync(address);
                }
                return await UpdateAddressAsync(address);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
