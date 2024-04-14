using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Service.Services.OrderLineService;
using Ecommerce.Service.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLineController : ControllerBase
    {
        private readonly IOrderLineService _orderLineService;
        private readonly UserManager<SiteUser> _userManager;
        public OrderLineController(IOrderLineService _orderLineService, UserManager<SiteUser> _userManager)
        {
            this._orderLineService = _orderLineService;
            this._userManager = _userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("allorderline")]
        public async Task<IActionResult> AllOrderLineAsync()
        {
            try
            {
                var response = await _orderLineService.GetAllOrderLinesAsync();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("allorderlinebyproductitemid/{productItemId}")]
        public async Task<IActionResult> AllOrderLineByProductItemIdAsync([FromRoute] Guid productItemId)
        {
            try
            {
                var response = await _orderLineService.GetAllOrderLinesByProductItemIdAsync(productItemId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("allorderlinebyshoporderid/{shopOrderId}")]
        public async Task<IActionResult> AllOrderLineByShopOrderIdAsync([FromRoute] Guid shopOrderId)
        {
            try
            {
                var response = await _orderLineService.GetAllOrderLinesByShopOrderIdAsync(shopOrderId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost("addorderline")]
        public async Task<IActionResult> AddOrderLineAsync([FromBody] OrderLineDto orderLineDto)
        {
            try
            {
                var response = await _orderLineService.AddOrderLineAsync(orderLineDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut("updateorderline")]
        public async Task<IActionResult> UpdateOrderLineAsync([FromBody] OrderLineDto orderLineDto)
        {
            try
            {
                var response = await _orderLineService.UpdateOrderLineAsync(orderLineDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("getorderlinebyid/{orderLineId}")]
        public async Task<IActionResult> GetOrderLineByIdAsync([FromRoute] Guid orderLineId)
        {
            try
            {
                var response = await _orderLineService.GetOrderLineByIdAsync(orderLineId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteorderlinebyid/{orderLineId}")]
        public async Task<IActionResult> DeleteOrderLineByIdAsync([FromRoute] Guid orderLineId)
        {
            try
            {
                var response = await _orderLineService.DeleteOrderLineByIdAsync(orderLineId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }



    }
}
