using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Service.Services.OrderStatusService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Authorize(Roles ="Admin,User")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusService _orderStatusService;
        public OrderStatusController(IOrderStatusService _orderStatusService)
        {
            this._orderStatusService = _orderStatusService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("allOrderStatus")]
        public async Task<IActionResult> GetAllOrderStatusAsync()
        {
            try
            {
                var response = await _orderStatusService.GetAllOrderStatusAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<string>
                {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpPost("addOrderStatus")]
        public async Task<IActionResult> AddOrderStatusAsync([FromBody] OrderStatusDto orderStatusDto)
        {
            try
            {
                var response = await _orderStatusService.AddOrderStatusAsync(orderStatusDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<string>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut("updateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatusAsync([FromBody] OrderStatusDto orderStatusDto)
        {
            try
            {
                var response = await _orderStatusService.UpdateOrderStatusAsync(orderStatusDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<string>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("orderStatus/{orderStatusId}")]
        public async Task<IActionResult> GetOrderStatusByIdAsync([FromRoute] Guid orderStatusId)
        {
            try
            {
                var response = await _orderStatusService.GetOrderStatusByIdAsync(orderStatusId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<string>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteOrderStatus/{orderStatusId}")]
        public async Task<IActionResult> DeleteOrderStatusByIdAsync([FromRoute] Guid orderStatusId)
        {
            try
            {
                var response = await _orderStatusService.DeleteOrderStatusByIdAsync(orderStatusId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<string>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }


    }
}
