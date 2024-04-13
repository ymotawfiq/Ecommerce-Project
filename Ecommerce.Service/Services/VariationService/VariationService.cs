using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.VariationRepository;
using Microsoft.AspNetCore.Http;


namespace Ecommerce.Service.Services.VariationService
{
    public class VariationService : IVariationService
    {
        private readonly IVariation _variationRepository;
        private readonly IProductCategory _categoryRepository;
        public VariationService(IVariation _variationRepository, IProductCategory _categoryRepository)
        {
            this._categoryRepository = _categoryRepository;
            this._variationRepository = _variationRepository;
        }

        public async Task<ApiResponse<Variation>> AddVariationAsync(VariationDto variationDto)
        {
            if (variationDto == null)
            {
                return new ApiResponse<Variation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Variation must not be null",
                    ResponseObject = new Variation()
                };
            }
            ProductCategory productCategory = await _categoryRepository.GetCategoryByIdAsync
                (new Guid(variationDto.CatrgoryId));
            if (productCategory == null)
            {
                return new ApiResponse<Variation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Category with id ({variationDto.CatrgoryId}) not exists",
                    ResponseObject = new Variation()
                };
            }
            Variation variation = await _variationRepository.AddVariationAsync(
                ConvertFromDto.ConvertFromVariationDto_Add(variationDto));
            return new ApiResponse<Variation>
            {
                StatusCode = 201,
                IsSuccess = true,
                Message = "Variation created successfully",
                ResponseObject = variation
            };
        }

        public async Task<ApiResponse<Variation>> DeleteVariationByIdAsync(Guid variationId)
        {
            Variation variation = await _variationRepository.GetVariationByIdAsync(variationId);
            if (variation == null)
            {
                return new ApiResponse<Variation>
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = $"No variations found with id ({variationId})",
                    ResponseObject = new Variation()
                };
            }
            var deletedVariation = await _variationRepository.DeleteVariationByIdAsync(variationId);
            return new ApiResponse<Variation>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Variation deleted successfully",
                ResponseObject = deletedVariation
            };
        }

        public async Task<ApiResponse<IEnumerable<Variation>>> GetAllVariationsAsync()
        {
            var variations = await _variationRepository.GetAllVariationsAsync();
            if (variations.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<Variation>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "No variations found",
                    ResponseObject = variations
                };
            }
            return new ApiResponse<IEnumerable<Variation>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Variations found successfully",
                    ResponseObject = variations
                };
        }

        public async Task<ApiResponse<IEnumerable<Variation>>> GetAllVariationsByCategoryIdAsync(Guid categoryId)
        {
            var variations = await _variationRepository.GetAllVariationsByCategoryIdAsync(categoryId);
            if (variations.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<Variation>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "No variations found",
                    ResponseObject = variations
                };
            }
            return new ApiResponse<IEnumerable<Variation>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Variations found successfully",
                    ResponseObject = variations
                };
        }

        public async Task<ApiResponse<Variation>> GetVariationByIdAsync(Guid variationId)
        {
            Variation variation = await _variationRepository.GetVariationByIdAsync(variationId);
            if (variation == null)
            {
                return new ApiResponse<Variation>
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = $"No variations found with id ({variationId})",
                    ResponseObject = new Variation()
                };
            }
            return new ApiResponse<Variation>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Variation found successfully",
                ResponseObject = variation
            };
        }

        public async Task<ApiResponse<Variation>> UpdateVariationAsync(VariationDto variationDto)
        {
            if (variationDto == null)
            {
                return new ApiResponse<Variation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Variation must not be null",
                    ResponseObject = new Variation()
                };
            }
            if (variationDto.Id == null)
            {
                return new ApiResponse<Variation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Variation id must not be null",
                    ResponseObject = new Variation()
                };
            }
            ProductCategory productCategory = await _categoryRepository.GetCategoryByIdAsync
                (new Guid(variationDto.CatrgoryId));
            if (productCategory == null)
            {
                return new ApiResponse<Variation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Category with id ({variationDto.CatrgoryId}) not exists",
                    ResponseObject = new Variation()
                };
            }
            Variation oldVariation = await _variationRepository.GetVariationByIdAsync
                (new Guid(variationDto.Id));
            if (oldVariation == null)
            {
                return new ApiResponse<Variation>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No variations found with id ({variationDto.Id})",
                    ResponseObject = new Variation()
                };
            }
            Variation variation = await _variationRepository.UpdateVariationAsync(
                ConvertFromDto.ConvertFromVariationDto_Update(variationDto));
            return new ApiResponse<Variation>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Variation updated successfully",
                ResponseObject = variation
            };
        }
    }
}
