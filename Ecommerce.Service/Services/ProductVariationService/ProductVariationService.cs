

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Ecommerce.Repository.Repositories.ProductVariationRepository;
using Ecommerce.Repository.Repositories.VariationOptionsRepository;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.Services.ProductVariationService
{
    public class ProductVariationService : IProductVariationService
    {
        private readonly IProductVariation _producVariationRepository;
        private readonly IVariationOptions _variationOptionsRepository;
        private readonly IProductItem _productItemRepository;
        public ProductVariationService(IProductVariation _producVariationRepository,
            IVariationOptions _variationOptionsRepository, IProductItem _productItemRepository)
        {
            this._productItemRepository = _productItemRepository;
            this._producVariationRepository = _producVariationRepository;
            this._variationOptionsRepository = _variationOptionsRepository;
        }
        public async Task<ApiResponse<ProductVariation>> AddProductVariationAsync(ProductVariationDto productVariationDto)
        {
            if (productVariationDto == null)
            {
                return new ApiResponse<ProductVariation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null",
                    ResponseObject = new ProductVariation()
                };
            }
            ProductItem productItem = await _productItemRepository.GetProductItemByIdAsync
                (productVariationDto.ProductItemId);
            if (productItem == null)
            {
                return new ApiResponse<ProductVariation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No product items founded with product id ({productVariationDto.ProductItemId})",
                    ResponseObject = new ProductVariation()
                };
            }
            VariationOptions variationOptions = await _variationOptionsRepository.GetVariationOptionsByIdAsync
                (productVariationDto.VariationOptionId);
            if (variationOptions == null)
            {
                return new ApiResponse<ProductVariation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No variation options founded with id ({productVariationDto.VariationOptionId})",
                    ResponseObject = new ProductVariation()
                };
            }

            ProductVariation productVariation = await _producVariationRepository.AddProductVariationAsync(
                ConvertFromDto.ConvertFromProductVariationsDto_Add(productVariationDto));
            return new ApiResponse<ProductVariation>
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = "Product variation created successfully",
                    ResponseObject = productVariation
                };
        }

        public async Task<ApiResponse<ProductVariation>> DeleteProductVariationByIdAsync(Guid productVariationId)
        {
            ProductVariation productVariation = await _producVariationRepository
                                .GetProductVariationByIdAsync(productVariationId);
            if (productVariation == null)
            {
                return new ApiResponse<ProductVariation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No product variation founded with id ({productVariationId})",
                    ResponseObject = new ProductVariation()
                };
            }
            var deletedProductVariation = await _producVariationRepository
                .DeleteProductVariationByIdAsync(productVariationId);
            return new ApiResponse<ProductVariation>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Product variation founded successfully",
                    ResponseObject = deletedProductVariation
                };
        }

        public async Task<ApiResponse<IEnumerable<ProductVariation>>> GetAllProductVariationsAsync()
        {
            var productVariations = await _producVariationRepository.GetAllProductVariationsAsync();
            if (productVariations.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ProductVariation>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "No product variations founded",
                    ResponseObject = new List<ProductVariation>()
                };
            }
            return new ApiResponse<IEnumerable<ProductVariation>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Product variations founded successfully",
                    ResponseObject = new List<ProductVariation>()
                };
        }

        public async Task<ApiResponse<IEnumerable<ProductVariation>>> 
            GetAllProductVariationsByProductItemIdAsync(Guid productItemId)
        {
            var productVariations = await _producVariationRepository
                                .GetAllVariationsByProductItemIdAsync(productItemId);
            if (productVariations.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ProductVariation>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "No variations founded for this product",
                    ResponseObject = new List<ProductVariation>()
                };
            }
            return new ApiResponse<IEnumerable<ProductVariation>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Product variations founded successfully",
                    ResponseObject = new List<ProductVariation>()
                };
        }

        public async Task<ApiResponse<IEnumerable<ProductVariation>>> 
            GetAllProductVariationsByVariationOptionIdAsync(Guid variationOptionId)
        {
            var productVariations = await _producVariationRepository
                                .GetAllVariationsByVariationOptionIdAsync(variationOptionId);
            if (productVariations.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ProductVariation>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "No products founded for this variation",
                    ResponseObject = new List<ProductVariation>()
                };
            }
            return new ApiResponse<IEnumerable<ProductVariation>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Product variations founded successfully",
                    ResponseObject = new List<ProductVariation>()
                };
        }

        public async Task<ApiResponse<ProductVariation>> GetProductVariationByIdAsync(Guid productVariationId)
        {
            ProductVariation productVariation = await _producVariationRepository
                                .GetProductVariationByIdAsync(productVariationId);
            if (productVariation == null)
            {
                return new ApiResponse<ProductVariation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No product variation founded with id ({productVariationId})",
                    ResponseObject = new ProductVariation()
                };
            }
            return new ApiResponse<ProductVariation>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Product variation founded successfully",
                    ResponseObject = productVariation
                };
        }

        public async Task<ApiResponse<ProductVariation>> 
            UpdateProductVariationAsync(ProductVariationDto productVariationDto)
        {
            if (productVariationDto == null)
            {
                return new ApiResponse<ProductVariation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null",
                    ResponseObject = new ProductVariation()
                };
            }
            if (productVariationDto.Id == null)
            {
                return new ApiResponse<ProductVariation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Product variation id must not be null",
                    ResponseObject = new ProductVariation()
                };
            }
            ProductItem productItem = await _productItemRepository.GetProductItemByIdAsync
                (productVariationDto.ProductItemId);
            if (productItem == null)
            {
                return new ApiResponse<ProductVariation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No product items founded with product id ({productVariationDto.ProductItemId})",
                    ResponseObject = new ProductVariation()
                };
            }
            VariationOptions variationOptions = await _variationOptionsRepository.GetVariationOptionsByIdAsync
                (productVariationDto.VariationOptionId);
            if (variationOptions == null)
            {
                return new ApiResponse<ProductVariation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No variation options founded with id ({productVariationDto.VariationOptionId})",
                    ResponseObject = new ProductVariation()
                };
            }
            ProductVariation productVariation = await _producVariationRepository.UpdateProductVariationAsync(
                ConvertFromDto.ConvertFromProductVariationsDto_Update(productVariationDto));
            return new ApiResponse<ProductVariation>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Product variation updated successfully",
                    ResponseObject = productVariation
                };
        }
    }
}
