using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.PromotionRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotion _promotionRepository;
        public PromotionController(IPromotion _promotionRepository)
        {
            this._promotionRepository = _promotionRepository;
        }

        [HttpGet("allpromotions")]
        public IActionResult GetAllPromotions()
        {
            try
            {
                var prromotions = _promotionRepository.GetAllPromotions();
                if (prromotions.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<Promotion>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No Promotions founded",
                        ResponseObject = prromotions
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<Promotion>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No Promotions founded",
                        ResponseObject = prromotions
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<Promotion>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new List<Promotion>()
                    });
            }
        }


        [HttpPost("addpromotion")]
        public IActionResult AddPromotion([FromBody] PromotionDto promotionDto)
        {
            try
            {
                if (promotionDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new Promotion()
                    });
                }
                Promotion addedPromotion = _promotionRepository.AddPromotion(
                    ConvertFromDto.ConvertFromPromotionDto_Add(promotionDto));
                return StatusCode(StatusCodes.Status201Created
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 201,
                        IsSuccess = true,
                        Message = "Promotion created successfully",
                        ResponseObject = addedPromotion
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new Promotion()
                    });
            }
        }


        [HttpPut("Updatepromotion")]
        public IActionResult UpdatePromotion([FromBody] PromotionDto promotionDto)
        {
            try
            {
                if (promotionDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new Promotion()
                    });
                }
                if (promotionDto.Id == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"You must enter promotion id",
                        ResponseObject = new Promotion()
                    });
                }
                Promotion oldPromotion = _promotionRepository
                    .GetPromotionById(new Guid(promotionDto.Id));
                if (oldPromotion == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Promotion with id ({promotionDto.Id}) not exists",
                        ResponseObject = new Promotion()
                    });
                }
                Promotion updatedPromotion = _promotionRepository.UpdatePromotion(
                    ConvertFromDto.ConvertFromPromotionDto_Update(promotionDto));
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Promotion updated successfully",
                        ResponseObject = updatedPromotion
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new Promotion()
                    });
            }
        }

        [HttpGet("getpromotionbyid/{promotionId}")]
        public IActionResult GetPromotionById([FromRoute] Guid promotionId)
        {
            try
            {
                Promotion promotion = _promotionRepository.GetPromotionById(promotionId);
                if (promotion == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Promotion with id ({promotionId}) not exists",
                        ResponseObject = new Promotion()
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Promotion founded successfully",
                        ResponseObject = promotion
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new Promotion()
                    });
            }
        }


        [HttpDelete("deletepromotionbyid/{promotionId}")]
        public IActionResult DeletePromotionById([FromRoute] Guid promotionId)
        {
            try
            {
                Promotion promotion = _promotionRepository.GetPromotionById(promotionId);
                if (promotion == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Promotion with id ({promotionId}) not exists",
                        ResponseObject = new Promotion()
                    });
                }
                Promotion deletedPromotion = _promotionRepository.DeletePromotionById(promotionId);
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Promotion deleted successfully",
                        ResponseObject = deletedPromotion
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new Promotion()
                    });
            }
        }

    }
}
