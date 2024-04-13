

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.PromotionCategoryRepository;
using Ecommerce.Repository.Repositories.PromotionRepository;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.Services.PromotionCategoryService
{
    public class PromotionCategoryService : IPromotionCategoryService
    {
        private readonly IPromotionCategory _promotionCategoryRepository;
        private readonly IProductCategory _productCategoryRepository;
        private readonly IPromotion _promotionRepository;
        public PromotionCategoryService
            (
            IPromotionCategory _promotionCategoryRepository,
            IProductCategory _productCategoryRepository,
            IPromotion _promotionRepository
            )
        {
            this._productCategoryRepository = _productCategoryRepository;
            this._promotionCategoryRepository = _promotionCategoryRepository;
            this._promotionRepository = _promotionRepository;
        }

        public async Task<ApiResponse<PromotionCategory>> AddPromotionCategoryAsync(PromotionCategoryDto promotionCategoryDto)
        {
            if (promotionCategoryDto == null)
            {
                return new ApiResponse<PromotionCategory>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null",
                    ResponseObject = new PromotionCategory()
                };
            }
            PromotionCategory promotionCategory = await _promotionCategoryRepository.AddPromotionCategoryAsync(
                ConvertFromDto.ConvertFromPromotionCategoryDto_Add(promotionCategoryDto));
            return new ApiResponse<PromotionCategory>
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = "Promotion category created successfully",
                    ResponseObject = promotionCategory
                };
        }

        public async Task<ApiResponse<PromotionCategory>> DeletePromotionCategoryByIdAsync(Guid promotionCategoryId)
        {
            PromotionCategory promotionCategory = await _promotionCategoryRepository
                                .GetPromotionCategoryByIdAsync(promotionCategoryId);
            if (promotionCategory == null)
            {
                return new ApiResponse<PromotionCategory>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Promotion category with id ({promotionCategoryId}) not exists",
                    ResponseObject = new PromotionCategory()
                };
            }
            PromotionCategory deletedPromotionCategory = await _promotionCategoryRepository
                .DeletePromotionCategoryByIdAsync(promotionCategoryId);
            return new ApiResponse<PromotionCategory>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = $"Promotion category deleted successfully",
                    ResponseObject = deletedPromotionCategory
                };
        }

        public async Task<ApiResponse<IEnumerable<PromotionCategory>>> GetAllPromotionCategoriesAsync()
        {
            var promotionCategories = await _promotionCategoryRepository.GetAllPromotionsAsync();
            if (promotionCategories.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<PromotionCategory>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "No promotion category found",
                    ResponseObject = promotionCategories
                };
            }
            return new ApiResponse<IEnumerable<PromotionCategory>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Promotion category found successfully",
                    ResponseObject = promotionCategories
                };
        }

        public async Task<ApiResponse<IEnumerable<PromotionCategory>>> GetAllPromotionCategoriesByCategoryIdAsync(Guid categoryId)
        {
            var promotionCategories = await _promotionCategoryRepository
                .GetAllPromotionsByCategoryIdAsync(categoryId);
            if (promotionCategories.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<PromotionCategory>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = $"No promotion category found with category id ({categoryId})",
                    ResponseObject = promotionCategories
                };
            }
            return new ApiResponse<IEnumerable<PromotionCategory>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Promotion category found successfully",
                    ResponseObject = promotionCategories
                };
        }

        public async Task<ApiResponse<IEnumerable<PromotionCategory>>> GetAllPromotionCategoriesByPromotionIdAsync(Guid promotionId)
        {
            var promotionCategories = await _promotionCategoryRepository
                .GetAllPromotionsByCategoryIdAsync(promotionId);
            if (promotionCategories.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<PromotionCategory>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = $"No promotion category found with promotion id ({promotionId})",
                    ResponseObject = promotionCategories
                };
            }
            return new ApiResponse<IEnumerable<PromotionCategory>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Promotion category found successfully",
                    ResponseObject = promotionCategories
                };
        }

        public async Task<ApiResponse<PromotionCategory>> GetPromotionCategoryByIdAsync(Guid promotionCategoryId)
        {
            PromotionCategory promotionCategory = await _promotionCategoryRepository
                .GetPromotionCategoryByIdAsync(promotionCategoryId);
            if (promotionCategory == null)
            {
                return new ApiResponse<PromotionCategory>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Promotion category with id ({promotionCategoryId}) not exists",
                    ResponseObject = new PromotionCategory()
                };
            }
            return new ApiResponse<PromotionCategory>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = $"Promotion category found successfully",
                    ResponseObject = promotionCategory
                };
        }

        public async Task<ApiResponse<PromotionCategory>> UpdatePromotionCategoryAsync(PromotionCategoryDto promotionCategoryDto)
        {
            if (promotionCategoryDto == null)
            {
                return new ApiResponse<PromotionCategory>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null",
                    ResponseObject = new PromotionCategory()
                };
            }
            if (promotionCategoryDto.Id == null)
            {
                return new ApiResponse<PromotionCategory>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Id must not be null",
                    ResponseObject = new PromotionCategory()
                };
            }
            Promotion promotion = await _promotionRepository
                .GetPromotionByIdAsync(promotionCategoryDto.PromotionId);
            if (promotion == null)
            {
                return new ApiResponse<PromotionCategory>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Promotion with id ({promotionCategoryDto.PromotionId}) not exists",
                    ResponseObject = new PromotionCategory()
                };
            }
            ProductCategory productCategory = await _productCategoryRepository.GetCategoryByIdAsync
                (promotionCategoryDto.CategoryId);
            if (productCategory == null)
            {
                return new ApiResponse<PromotionCategory>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Category with id ({promotionCategoryDto.CategoryId}) not exists",
                    ResponseObject = new PromotionCategory()
                };
            }
            PromotionCategory promotionCategory = await _promotionCategoryRepository.UpdatePromotionCategoryAsync(
                ConvertFromDto.ConvertFromPromotionCategoryDto_Update(promotionCategoryDto));
            return new ApiResponse<PromotionCategory>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Promotion category updated successfully",
                    ResponseObject = promotionCategory
                };
        }
    }
}
