using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Service.Services.ShopOrderService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopOrderController : ControllerBase
    {
        private readonly IShopOrderService _shopOrderService;
        public ShopOrderController(IShopOrderService _shopOrderService)
        {
            this._shopOrderService = _shopOrderService;
        }

        [HttpGet("allshoppingorders")]
        public async Task<IActionResult> GetAllShopOrdersAsync()
        {
            try
            {
                var response = await _shopOrderService.GetAllShopOrdersAsync();
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

        [HttpGet("allshoppingordersbyusernameoremail")]
        public async Task<IActionResult> GetAllShopOrdersByUserUsernameOrEmailAsync(string usernameOrEmail)
        {
            try
            {
                var response = await _shopOrderService.GetAllShopOrdersByUserUsernameOrEmailAsync(usernameOrEmail);
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

        [HttpGet("allshoppingordersbydate")]
        public async Task<IActionResult> GetAllShopOrdersByDateAsync(DateTime date)
        {
            try
            {
                var response = await _shopOrderService.GetAllShopOrdersByDateAsync(date);
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

        [HttpGet("allshoppingordersbyshippingmethodid/{shippingMethodId}")]
        public async Task<IActionResult> GetAllShopOrdersByShippingMethodIdAsync(Guid shippingMethodId)
        {
            try
            {
                var response = await _shopOrderService.GetAllShopOrdersByShippingMethodIdAsync(shippingMethodId);
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

        [HttpGet("allshoppingordersbyaddressid/{addressId}")]
        public async Task<IActionResult> GetAllShopOrdersByAddressIdAsync(Guid addressId)
        {
            try
            {
                var response = await _shopOrderService.GetAllShopOrdersByAddressIdAsync(addressId);
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

        [HttpGet("allshoppingordersbypaymentmethodid/{paymentMethodId}")]
        public async Task<IActionResult> GetAllShopOrdersByPaymentIdAsync(Guid paymentMethodId)
        {
            try
            {
                var response = await _shopOrderService.GetAllShopOrdersByPaymentMethodIdAsync(paymentMethodId);
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


        [HttpPost("addshoporder")]
        public async Task<IActionResult> AddShopOrderAsync([FromBody] ShopOrderDto shopOrderDto)
        {
            try
            {
                var response = await _shopOrderService.AddShopOrderAsync(shopOrderDto);
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


        [HttpPut("updateshoporder")]
        public async Task<IActionResult> UpdateShopOrderAsync([FromBody] ShopOrderDto shopOrderDto)
        {
            try
            {
                var response = await _shopOrderService.UpdateShopOrderAsync(shopOrderDto);
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

        [HttpGet("getshoporderbyid/{shopOrderId}")]
        public async Task<IActionResult> GetShopOrderByIdAsync([FromRoute] Guid shopOrderId)
        {
            try
            {
                var response = await _shopOrderService.GetShopOrderByIdAsync(shopOrderId);
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

        [HttpDelete("deleteshoporderbyid/{shopOrderId}")]
        public async Task<IActionResult> DeleteShopOrder([FromRoute] Guid shopOrderId)
        {
            try
            {
                var response = await _shopOrderService.DeleteShopOrderByIdAsync(shopOrderId);
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
