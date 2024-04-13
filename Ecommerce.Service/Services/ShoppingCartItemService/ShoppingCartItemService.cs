

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Ecommerce.Repository.Repositories.ShoppingCartItemRepository;
using Ecommerce.Repository.Repositories.ShoppingCartRepository;

namespace Ecommerce.Service.Services.ShoppingCartItemService
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly IShoppingCartItem _shoppingCartItemRepository;
        private readonly IProductItem _productItemRepository;
        private readonly IShoppingCart _shoppingCartRepository;
        public ShoppingCartItemService(IShoppingCartItem _shoppingCartItemRepository
            , IProductItem _productItemRepository, IShoppingCart _shoppingCartRepository)
        {
            this._shoppingCartItemRepository = _shoppingCartItemRepository;
            this._productItemRepository = _productItemRepository;
            this._shoppingCartRepository = _shoppingCartRepository;
        }

        public async Task<ApiResponse<ShoppingCartItem>> AddShoppingCartItemAsync(ShoppingCartItemDto shoppingCartItemDto)
        {
            if (shoppingCartItemDto == null)
            {
                return new ApiResponse<ShoppingCartItem>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            ShoppingCart shoppingCart = await _shoppingCartRepository
                .GetShoppingCartByIdAsync(shoppingCartItemDto.CartId);
            if (shoppingCart == null)
            {
                return new ApiResponse<ShoppingCartItem>
                {
                    IsSuccess = false,
                    Message = "Shopping cart not found",
                    StatusCode = 400
                };
            }
            ProductItem productItem = await _productItemRepository
                .GetProductItemByIdAsync(shoppingCartItemDto.ProductItemId);
            if (productItem == null)
            {
                return new ApiResponse<ShoppingCartItem>
                {
                    IsSuccess = false,
                    Message = "Product item not found",
                    StatusCode = 400
                };
            }
            var newShoppingCartItem = await _shoppingCartItemRepository.AddShoppingCartItemAsync
                (ConvertFromDto.ConvertFromShoppingCartItemDto_Add(shoppingCartItemDto));
            return new ApiResponse<ShoppingCartItem>
            {
                IsSuccess = true,
                Message = "Shopping cart item added successfully",
                StatusCode = 201,
                ResponseObject = newShoppingCartItem
            };
        }

        public async Task<ApiResponse<ShoppingCartItem>> DeleteShoppingCartItemByIdAsync(Guid shoppingCartItemId)
        {
            ShoppingCartItem shoppingCartItem = await _shoppingCartItemRepository
                .GetShoppingCartItemByIdAsync(shoppingCartItemId);
            if (shoppingCartItem == null)
            {
                return new ApiResponse<ShoppingCartItem>
                {
                    IsSuccess = false,
                    Message = "Shopping cart item not found",
                    StatusCode = 400
                };
            }
            var deletedShoppingCartItem = await _shoppingCartItemRepository
                .DeleteShoppingCartItemByIdAsync(shoppingCartItemId);
            return new ApiResponse<ShoppingCartItem>
            {
                IsSuccess = true,
                Message = "Shopping cart item deleted successfully",
                StatusCode = 200,
                ResponseObject = deletedShoppingCartItem
            };
        }

        public async Task<ApiResponse<IEnumerable<ShoppingCartItem>>> GetAllShoppingCartItemsAsync()
        {
            var shoppingCartItems = await _shoppingCartItemRepository.GetAllShoppingCartItemsAsync();
            if (shoppingCartItems.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ShoppingCartItem>>
                {
                    IsSuccess = true,
                    Message = "No shopping cart item found",
                    StatusCode = 200,
                    ResponseObject = shoppingCartItems
                };
            }
            return new ApiResponse<IEnumerable<ShoppingCartItem>>
            {
                IsSuccess = true,
                Message = "Shopping cart item found successfully",
                StatusCode = 200,
                ResponseObject = shoppingCartItems
            };
        }

        public async Task<ApiResponse<IEnumerable<ShoppingCartItem>>> GetAllShoppingCartItemsByCartIdAsync(Guid cartId)
        {
            var shoppingCartItems = await _shoppingCartItemRepository.GetAllShoppingCartItemsByCartIdAsync(cartId);
            if (shoppingCartItems.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ShoppingCartItem>>
                {
                    IsSuccess = true,
                    Message = "No shopping cart item found",
                    StatusCode = 200,
                    ResponseObject = shoppingCartItems
                };
            }
            return new ApiResponse<IEnumerable<ShoppingCartItem>>
            {
                IsSuccess = true,
                Message = "Shopping cart item found successfully",
                StatusCode = 200,
                ResponseObject = shoppingCartItems
            };
        }

        public async Task<ApiResponse<ShoppingCartItem>> GetShoppingCartItemAsync(Guid shoppingCartItemId)
        {
            ShoppingCartItem shoppingCartItem = await _shoppingCartItemRepository
                .GetShoppingCartItemByIdAsync(shoppingCartItemId);
            if (shoppingCartItem == null)
            {
                return new ApiResponse<ShoppingCartItem>
                {
                    IsSuccess = false,
                    Message = "Shopping cart item not found",
                    StatusCode = 400
                };
            }
            return new ApiResponse<ShoppingCartItem>
            {
                IsSuccess = true,
                Message = "Shopping cart item found successfully",
                StatusCode = 200,
                ResponseObject = shoppingCartItem
            };
        }

        public async Task<ApiResponse<ShoppingCartItem>> UpdateShoppingCartItemAsync(ShoppingCartItemDto shoppingCartItemDto)
        {
            if (shoppingCartItemDto == null)
            {
                return new ApiResponse<ShoppingCartItem>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            if (shoppingCartItemDto.Id == null)
            {
                return new ApiResponse<ShoppingCartItem>
                {
                    IsSuccess = false,
                    Message = "Shopping cart item id must not be null",
                    StatusCode = 400
                };
            }
            ShoppingCartItem shoppingCartItem = await _shoppingCartItemRepository
                .GetShoppingCartItemByIdAsync(new Guid(shoppingCartItemDto.Id));
            if (shoppingCartItemDto.Id == null)
            {
                return new ApiResponse<ShoppingCartItem>
                {
                    IsSuccess = false,
                    Message = "No shopping cart item found",
                    StatusCode = 400
                };
            }
            ShoppingCart oldShoppingCart = await _shoppingCartRepository
                .GetShoppingCartByIdAsync(shoppingCartItemDto.CartId);
            if (oldShoppingCart == null)
            {
                return new ApiResponse<ShoppingCartItem>
                {
                    IsSuccess = false,
                    Message = "Shopping cart not found",
                    StatusCode = 400
                };
            }
            ProductItem productItem = await _productItemRepository
                .GetProductItemByIdAsync(shoppingCartItemDto.ProductItemId);
            if (productItem == null)
            {
                return new ApiResponse<ShoppingCartItem>
                {
                    IsSuccess = false,
                    Message = "Product item not found",
                    StatusCode = 400
                };
            }
            var newShoppingCartItem = await _shoppingCartItemRepository.UpdateShoppingCartItemAsync
                (ConvertFromDto.ConvertFromShoppingCartItemDto_Update(shoppingCartItemDto));
            return new ApiResponse<ShoppingCartItem>
            {
                IsSuccess = true,
                Message = "Shopping cart item updated successfully",
                StatusCode = 200,
                ResponseObject = newShoppingCartItem
            };
        }
    }
}
