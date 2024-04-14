using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Service.Services.OrderLineService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLineController : ControllerBase
    {
        private readonly IOrderLineService _orderLineService;
        public OrderLineController(IOrderLineService _orderLineService)
        {
            this._orderLineService = _orderLineService;
        }


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
