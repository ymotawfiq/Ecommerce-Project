using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.PromotionRepository;


namespace Ecommerce.Service.Services.PromotionService
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotion _promotionRepository;
        public PromotionService(IPromotion _promotionRepository)
        {
            this._promotionRepository = _promotionRepository;
        }

        public async Task<ApiResponse<Promotion>> AddPromotionAsync(PromotionDto promotionDto)
        {
            if (promotionDto == null)
            {
                return new ApiResponse<Promotion>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null",
                    ResponseObject = new Promotion()
                };
            }
            Promotion addedPromotion = await _promotionRepository.AddPromotionAsync(
                ConvertFromDto.ConvertFromPromotionDto_Add(promotionDto));
            return new ApiResponse<Promotion>
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = "Promotion created successfully",
                    ResponseObject = addedPromotion
                };
        }

        public async Task<ApiResponse<Promotion>> DeletePromotionByIdAsync(Guid promotionId)
        {
            Promotion promotion = await _promotionRepository.GetPromotionByIdAsync(promotionId);
            if (promotion == null)
            {
                return new ApiResponse<Promotion>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Promotion with id ({promotionId}) not exists",
                    ResponseObject = new Promotion()
                };
            }
            Promotion deletedPromotion = await _promotionRepository.DeletePromotionByIdAsync(promotionId);
            return new ApiResponse<Promotion>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Promotion deleted successfully",
                    ResponseObject = deletedPromotion
                };
        }

        public async Task<ApiResponse<IEnumerable<Promotion>>> GetAllPromotionsAsync()
        {
            var prromotions = await _promotionRepository.GetAllPromotionsAsync();
            if (prromotions.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<Promotion>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "No Promotions found",
                    ResponseObject = prromotions
                };
            }
            return new ApiResponse<IEnumerable<Promotion>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Promotions found successfully",
                    ResponseObject = prromotions
                };
        }

        public async Task<ApiResponse<Promotion>> GetPromotionByIdAsync(Guid promotionId)
        {
            Promotion promotion = await _promotionRepository.GetPromotionByIdAsync(promotionId);
            if (promotion == null)
            {
                return new ApiResponse<Promotion>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Promotion with id ({promotionId}) not exists",
                    ResponseObject = new Promotion()
                };
            }
            return new ApiResponse<Promotion>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Promotion found successfully",
                    ResponseObject = promotion
                };
        }

        public async Task<ApiResponse<Promotion>> UpdatePromotionAsync(PromotionDto promotionDto)
        {
            if (promotionDto == null)
            {
                return new ApiResponse<Promotion>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null",
                    ResponseObject = new Promotion()
                };
            }
            if (promotionDto.Id == null)
            {
                return new ApiResponse<Promotion>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"You must enter promotion id",
                    ResponseObject = new Promotion()
                };
            }
            Promotion oldPromotion = await _promotionRepository
                .GetPromotionByIdAsync(new Guid(promotionDto.Id));
            if (oldPromotion == null)
            {
                return new ApiResponse<Promotion>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Promotion with id ({promotionDto.Id}) not exists",
                    ResponseObject = new Promotion()
                };
            }
            Promotion updatedPromotion = await _promotionRepository.UpdatePromotionAsync(
                ConvertFromDto.ConvertFromPromotionDto_Update(promotionDto));
            return new ApiResponse<Promotion>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Promotion updated successfully",
                    ResponseObject = updatedPromotion
                };
        }
    }
}
