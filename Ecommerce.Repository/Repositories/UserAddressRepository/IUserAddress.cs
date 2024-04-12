

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.UserAddressRepository
{
    public interface IUserAddress
    {
        public Task<UserAddress> AddUserAddressAsync(UserAddress userAddress);
        public Task<UserAddress> UpdateUserAddressAsync(UserAddress userAddress);
        public Task<UserAddress> DeleteUserAddressByIdAsync(Guid id);
        public Task<UserAddress> GetUserAddressByIdAsync(Guid id);
        public Task<IEnumerable<UserAddress>> GetAllUsersAddressesAsync();
        public Task<IEnumerable<UserAddress>> GetAllUsersAddressesByUserIdAsync(Guid userId);
        public Task<IEnumerable<UserAddress>> GetAllUsersAddressesByUserNameOrEmailAsync
            (string userNameOrEmail);
        public Task SaveChangesAsync();
        public Task<UserAddress> UpsertAsync(UserAddress userAddress);
    }
}
