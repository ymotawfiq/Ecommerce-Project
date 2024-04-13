

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Repository.Repositories.ShoppingCartRepository;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Service.Services.ShoppingCartService
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCart _shoppingCartRepository;
        private readonly UserManager<SiteUser> _userManager;
        public ShoppingCartService(IShoppingCart _shoppingCartRepository, UserManager<SiteUser> _userManager)
        {
            this._shoppingCartRepository = _shoppingCartRepository;
            this._userManager = _userManager;
        }
        public async Task<ApiResponse<ShoppingCart>> AddShoppingCartAsync(ShoppingCartDto shoppingCartDto)
        {
            if (shoppingCartDto == null)
            {
                return new ApiResponse<ShoppingCart>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            try
            {
                var userId = new Guid(shoppingCartDto.UserIdOrEmail);
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return new ApiResponse<ShoppingCart>
                    {
                        IsSuccess = false,
                        Message = "User not found",
                        StatusCode = 400
                    };
                }
                ShoppingCart shoppingCart = new ShoppingCart
                {
                    UserId = user.Id
                };
                var newShoppingCart = await _shoppingCartRepository.AddShoppingCartAsync(shoppingCart);
                return new ApiResponse<ShoppingCart>
                {
                    IsSuccess = true,
                    Message = "Shopping cart saved successfully",
                    StatusCode = 201,
                    ResponseObject = newShoppingCart
                };
            }
            catch (Exception)
            {
                var user = await _userManager.FindByEmailAsync(shoppingCartDto.UserIdOrEmail);
                if (user == null)
                {
                    return new ApiResponse<ShoppingCart>
                    {
                        IsSuccess = false,
                        Message = "User not found",
                        StatusCode = 400
                    };
                }
                ShoppingCart shoppingCart = new ShoppingCart
                {
                    UserId = user.Id
                };
                var newShoppingCart = await _shoppingCartRepository.AddShoppingCartAsync(shoppingCart);
                return new ApiResponse<ShoppingCart>
                {
                    IsSuccess = true,
                    Message = "Shopping cart saved successfully",
                    StatusCode = 201,
                    ResponseObject = newShoppingCart
                };
            }
        }

        public async Task<ApiResponse<ShoppingCart>> DeleteShoppingCartByIdAsync(Guid shoppingCartId)
        {
            ShoppingCart shoppingCart = await _shoppingCartRepository.GetShoppingCartByIdAsync(shoppingCartId);
            if (shoppingCart == null)
            {
                return new ApiResponse<ShoppingCart>
                {
                    IsSuccess = false,
                    Message = "Shopping cart vot found",
                    StatusCode = 400,
                };
            }
            var deletedShoppingCart = await _shoppingCartRepository.DeleteShoppingCartByIdAsync(shoppingCartId);
            return new ApiResponse<ShoppingCart>
            {
                IsSuccess = true,
                Message = "Shopping cart deleted successfully",
                StatusCode = 200,
                ResponseObject = deletedShoppingCart
            };
        }

        public async Task<ApiResponse<IEnumerable<ShoppingCart>>> GeAllShoppingCartsAsync()
        {
            var shoppingCarts = await _shoppingCartRepository.GetAllShoppingCartsAsync();
            if (shoppingCarts.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ShoppingCart>>
                {
                    IsSuccess = true,
                    Message = "No shopping cart found",
                    StatusCode = 200,
                    ResponseObject = shoppingCarts
                };
            }
            return new ApiResponse<IEnumerable<ShoppingCart>>
            {
                IsSuccess = true,
                Message = "Shopping cart found successfully",
                StatusCode = 200,
                ResponseObject = shoppingCarts
            };
        }

        public async Task<ApiResponse<IEnumerable<ShoppingCart>>> GeAllShoppingCartsByUserIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ApiResponse<IEnumerable<ShoppingCart>>
                {
                    IsSuccess = true,
                    Message = "User not found",
                    StatusCode = 400,
                };
            }
            var shoppingCarts = await _shoppingCartRepository.GetAllShoppingCartsByUserIdAsync(userId);
            if (shoppingCarts.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ShoppingCart>>
                {
                    IsSuccess = true,
                    Message = "No shopping cart found",
                    StatusCode = 200,
                    ResponseObject = shoppingCarts
                };
            }
            return new ApiResponse<IEnumerable<ShoppingCart>>
            {
                IsSuccess = true,
                Message = "Shopping cart found successfully",
                StatusCode = 200,
                ResponseObject = shoppingCarts
            };
        }

        public async Task<ApiResponse<IEnumerable<ShoppingCart>>> GeAllShoppingCartsByUsernameOrEmailAsync(string usernameOrEmail)
        {
            var user = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user == null)
            {
                return new ApiResponse<IEnumerable<ShoppingCart>>
                {
                    IsSuccess = true,
                    Message = "User not found",
                    StatusCode = 400,
                };
            }
            var shoppingCarts = await _shoppingCartRepository.GetAllShoppingCartsByUserEmailAsync(usernameOrEmail);
            if (shoppingCarts.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ShoppingCart>>
                {
                    IsSuccess = true,
                    Message = "No shopping cart found",
                    StatusCode = 200,
                    ResponseObject = shoppingCarts
                };
            }
            return new ApiResponse<IEnumerable<ShoppingCart>>
            {
                IsSuccess = true,
                Message = "Shopping cart found successfully",
                StatusCode = 200,
                ResponseObject = shoppingCarts
            };
        }

        public async Task<ApiResponse<ShoppingCart>> GetShoppingCartByIdAsync(Guid shoppingCartId)
        {
            ShoppingCart shoppingCart = await _shoppingCartRepository.GetShoppingCartByIdAsync(shoppingCartId);
            if (shoppingCart == null)
            {
                return new ApiResponse<ShoppingCart>
                {
                    IsSuccess = false,
                    Message = "Shopping cart vot found",
                    StatusCode = 400,
                };
            }
            return new ApiResponse<ShoppingCart>
            {
                IsSuccess = true,
                Message = "Shopping cart found successfully",
                StatusCode = 200,
                ResponseObject = shoppingCart
            };
        }

        public async Task<ApiResponse<ShoppingCart>> UpdateShoppingCartAsync(ShoppingCartDto shoppingCartDto)
        {
            if (shoppingCartDto == null)
            {
                return new ApiResponse<ShoppingCart>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            try
            {
                var userId = new Guid(shoppingCartDto.UserIdOrEmail);
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return new ApiResponse<ShoppingCart>
                    {
                        IsSuccess = false,
                        Message = "User not found",
                        StatusCode = 400
                    };
                }
                if (shoppingCartDto.Id == null)
                {
                    return new ApiResponse<ShoppingCart>
                    {
                        IsSuccess = false,
                        Message = "Shopping cart id must not be null",
                        StatusCode = 400
                    };
                }
                ShoppingCart shoppingCart = new ShoppingCart
                {
                    Id = new Guid(shoppingCartDto.Id),
                    UserId = user.Id
                };
                var updatedShoppingCart = await _shoppingCartRepository.UpdateShoppingCartAsync(shoppingCart);
                return new ApiResponse<ShoppingCart>
                {
                    IsSuccess = true,
                    Message = "Shopping cart updated successfully",
                    StatusCode = 200,
                    ResponseObject = updatedShoppingCart
                };
            }
            catch (Exception)
            {
                var user = await _userManager.FindByEmailAsync(shoppingCartDto.UserIdOrEmail);
                if (user == null)
                {
                    return new ApiResponse<ShoppingCart>
                    {
                        IsSuccess = false,
                        Message = "User not found",
                        StatusCode = 400
                    };
                }
                if (shoppingCartDto.Id == null)
                {
                    return new ApiResponse<ShoppingCart>
                    {
                        IsSuccess = false,
                        Message = "Shopping cart id must not be null",
                        StatusCode = 400
                    };
                }
                ShoppingCart shoppingCart = new ShoppingCart
                {
                    Id = new Guid(shoppingCartDto.Id),
                    UserId = user.Id
                };
                var updatedShoppingCart = await _shoppingCartRepository.UpdateShoppingCartAsync(shoppingCart);
                return new ApiResponse<ShoppingCart>
                {
                    IsSuccess = true,
                    Message = "Shopping cart updated successfully",
                    StatusCode = 200,
                    ResponseObject = updatedShoppingCart
                };
            }
        }
    }
}
