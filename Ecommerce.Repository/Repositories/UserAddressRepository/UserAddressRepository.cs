

using Ecommerce.Data.Models.Entities;
using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Ecommerce.Data.Models.Entities.Authentication;

namespace Ecommerce.Repository.Repositories.UserAddressRepository
{
    public class UserAddressRepository : IUserAddress
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<SiteUser> _userManager;
        public UserAddressRepository(ApplicationDbContext _dbContext, UserManager<SiteUser> _userManager)
        {
            this._dbContext = _dbContext;
            this._userManager = _userManager;
        }

        public async Task<UserAddress> AddUserAddressAsync(UserAddress userAddress)
        {
            try
            {
                await _dbContext.UserAddresses.AddAsync(userAddress);
                await SaveChangesAsync();
                return userAddress;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserAddress> DeleteUserAddressByIdAsync(Guid id)
        {
            try
            {
                UserAddress userAddress = await GetUserAddressByIdAsync(id);
                _dbContext.UserAddresses.Remove(userAddress);
                await SaveChangesAsync();
                return userAddress;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserAddress>> GetAllUsersAddressesAsync()
        {
            try
            {
                return await _dbContext.UserAddresses.Include(e => e.Address).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserAddress>> GetAllUsersAddressesByUserIdAsync(Guid userId)
        {
            try
            {
                return
                    from u in await GetAllUsersAddressesAsync()
                    where u.UserId == userId.ToString()
                    select u;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserAddress>> GetAllUsersAddressesByUserNameOrEmailAsync(string userNameOrEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userNameOrEmail);
                return
                    await GetAllUsersAddressesByUserIdAsync(new Guid(user.Id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserAddress> GetUserAddressByIdAsync(Guid id)
        {
            try
            {
                UserAddress? userAddress = await
                    _dbContext.UserAddresses.Where(e => e.Id == id).FirstOrDefaultAsync();
                return userAddress;
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

        public async Task<UserAddress> UpdateUserAddressAsync(UserAddress userAddress)
        {
            try
            {
                UserAddress oldUserAddress = await GetUserAddressByIdAsync(userAddress.Id);
                oldUserAddress.IsDefault = userAddress.IsDefault;
                oldUserAddress.UserId = userAddress.UserId;
                oldUserAddress.AddressId = userAddress.AddressId;
                await SaveChangesAsync();
                return oldUserAddress;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserAddress> UpsertAsync(UserAddress userAddress)
        {
            try
            {
                UserAddress existUserAddress = await GetUserAddressByIdAsync(userAddress.Id);
                if (existUserAddress == null)
                {
                    return await AddUserAddressAsync(userAddress);
                }
                return await UpdateUserAddressAsync(userAddress);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
