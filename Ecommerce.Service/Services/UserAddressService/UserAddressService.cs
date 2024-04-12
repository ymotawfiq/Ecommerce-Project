using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Repository.Repositories.AddressRepository;
using Ecommerce.Repository.Repositories.UserAddressRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace Ecommerce.Service.Services.UserAddressService
{
    public class UserAddressService : IUserAddressService
    {
        private readonly IUserAddress _userAddressRepository;
        private readonly UserManager<SiteUser> _userManager;
        private readonly IAddress _addressRepository;
        public UserAddressService(IUserAddress _userAddressRepository, UserManager<SiteUser> _userManager,
            IAddress _addressRepository)
        {
            this._userAddressRepository = _userAddressRepository;
            this._userManager = _userManager;
            this._addressRepository = _addressRepository;
        }

        public async Task<ApiResponse<UserAddress>> AddUserAddressAsync(UserAddressDto userAddressDto)
        {
            if (userAddressDto == null)
            {
                return new ApiResponse<UserAddress>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null"
                };
            }
            Address address = await _addressRepository.GetAddressByIdAsync(new Guid(userAddressDto.AddressId));
            if (address == null)
            {
                return new ApiResponse<UserAddress>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Address not found"
                };
            }
            try
            {
                var userId = new Guid(userAddressDto.UserIdOrEmail);
                
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return new ApiResponse<UserAddress>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "User not found"
                    };
                }
                var newUserAddress = await _userAddressRepository.AddUserAddressAsync(
                    ConvertFromDto.ConvertFromUserAddressDto_Add(userAddressDto));
                return new ApiResponse<UserAddress>
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = "User address saved successfully",
                    ResponseObject = newUserAddress
                };
            }
            catch (Exception)
            {
                var user = await _userManager.FindByEmailAsync(userAddressDto.UserIdOrEmail);
                if (user == null)
                {
                    return new ApiResponse<UserAddress>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "User not found"
                    };
                }
                var userAddress = new UserAddress
                {
                    AddressId = new Guid(userAddressDto.AddressId),
                    IsDefault = userAddressDto.IsDefault,
                    UserId = user.Id,
                };
                var newUserAddress = await _userAddressRepository.AddUserAddressAsync(userAddress);
                return new ApiResponse<UserAddress>
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = "User address saved successfully",
                    ResponseObject = newUserAddress
                };
            }


        }

        public async Task<ApiResponse<UserAddress>> DeleteUserAddressByIdAsync(Guid userAddressId)
        {
            var userAddress = await _userAddressRepository.GetUserAddressByIdAsync(userAddressId);
            if (userAddress == null)
            {
                return new ApiResponse<UserAddress>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "No user address founded"
                };
            }
            var deletedUser = await _userAddressRepository.DeleteUserAddressByIdAsync(userAddressId);
            return new ApiResponse<UserAddress>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "User address deleted successfully",
                ResponseObject = deletedUser
            };
        }

        public async Task<ApiResponse<IEnumerable<UserAddress>>> GetAllUserAddressAsync()
        {
            var userAddresses = await _userAddressRepository.GetAllUsersAddressesAsync();
            if (userAddresses.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<UserAddress>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No user addresses founded",
                        ResponseObject = userAddresses
                    };
            }
            return new ApiResponse<IEnumerable<UserAddress>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "User addresses founded successfully",
                        ResponseObject = userAddresses
                    };
        }

        public async Task<ApiResponse<UserAddress>> GetUserAddressByIdAsync(Guid userAddressId)
        {

            var userAddress = await _userAddressRepository.GetUserAddressByIdAsync(userAddressId);
            if (userAddress == null)
            {
                return new ApiResponse<UserAddress>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "No user address founded"
                };
            }
            return new ApiResponse<UserAddress>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "User address founded successfully",
                ResponseObject = userAddress
            };
        }

        public async Task<ApiResponse<IEnumerable<UserAddress>>> GetUserAddressesByUserIdAsync(Guid userId)
        {
            var userAddressesById = await _userAddressRepository.GetAllUsersAddressesByUserIdAsync(userId);
            if (userAddressesById.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<UserAddress>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No user addresses founded for this user",
                        ResponseObject = userAddressesById
                    };
            }
            return new ApiResponse<IEnumerable<UserAddress>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "User addresses founded successfully",
                        ResponseObject = userAddressesById
                    };
        }

        public async Task<ApiResponse<IEnumerable<UserAddress>>> GetUserAddressesByUsernameOrEmailAsync
            (string userNameOrEmail)
        {
            var userAddressesById = await _userAddressRepository
                                .GetAllUsersAddressesByUserNameOrEmailAsync(userNameOrEmail);
            if (userAddressesById.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<UserAddress>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No user addresses founded for this user",
                        ResponseObject = userAddressesById
                    };
            }
            return new ApiResponse<IEnumerable<UserAddress>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "User addresses founded successfully",
                        ResponseObject = userAddressesById
                    };
        }

        public async Task<ApiResponse<UserAddress>> UpdateUserAddressAsync(UserAddressDto userAddressDto)
        {
            if (userAddressDto == null)
            {
                return new ApiResponse<UserAddress>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null"
                };
            }
            if (userAddressDto.Id == null)
            {
                return new ApiResponse<UserAddress>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "User address id must not be null"
                };
            }
            Address address = await _addressRepository.GetAddressByIdAsync(new Guid(userAddressDto.AddressId));
            if (address == null)
            {
                return new ApiResponse<UserAddress>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Address not found"
                };
            }

            try
            {
                var userId = new Guid(userAddressDto.UserIdOrEmail);

                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return new ApiResponse<UserAddress>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "User not found"
                    };
                }
                var updatedUserAddress = await _userAddressRepository.AddUserAddressAsync(
                    ConvertFromDto.ConvertFromUserAddressDto_Update(userAddressDto));
                return new ApiResponse<UserAddress>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "User address updated successfully",
                    ResponseObject = updatedUserAddress
                };
            }
            catch (Exception)
            {
                var user = await _userManager.FindByEmailAsync(userAddressDto.UserIdOrEmail);
                if (user == null)
                {
                    return new ApiResponse<UserAddress>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "User not found"
                    };
                }
                var userAddress = new UserAddress
                {
                    Id = new Guid(userAddressDto.Id),
                    AddressId = new Guid(userAddressDto.AddressId),
                    IsDefault = userAddressDto.IsDefault,
                    UserId = user.Id,
                };
                var updatedUserAddress = await _userAddressRepository.UpdateUserAddressAsync(userAddress);
                return new ApiResponse<UserAddress>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "User address updated successfully",
                    ResponseObject = updatedUserAddress
                };
            }
        }
    }
}
