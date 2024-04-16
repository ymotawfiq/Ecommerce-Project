using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Service.Services.PaymentTypeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeService _paymentTypeService;
        public PaymentTypeController(IPaymentTypeService _paymentTypeService)
        {
            this._paymentTypeService = _paymentTypeService;
        }

        [AllowAnonymous]
        [HttpGet("payment-types")]
        public async Task<IActionResult> GetAllPaymentTypesAsync()
        {
            try
            {
                var response = await _paymentTypeService.GetAllPaymentTypes();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<PaymentType>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addPaymentType")]
        public async Task<IActionResult> AddPaymentTypeAsync([FromBody] PaymentTypeDto paymentTypeDto)
        {
            try
            {
                var response = await _paymentTypeService.AddPaymentTypeAsync(paymentTypeDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<PaymentType>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updatePaymentType")]
        public async Task<IActionResult> UpdatePaymentTypeAsync([FromBody] PaymentTypeDto paymentTypeDto)
        {
            try
            {
                var response = await _paymentTypeService.UpdatePaymentTypeAsync(paymentTypeDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<PaymentType>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [AllowAnonymous]
        [HttpGet("paymentType/{paymentTypeId}")]
        public async Task<IActionResult> GetPaymentTypeByIdAsync([FromRoute] Guid paymentTypeId)
        {
            try
            {
                var response = await _paymentTypeService.GetPaymentTypeByIdAsync(paymentTypeId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<PaymentType>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deletePaymentType/{paymentTypeId}")]
        public async Task<IActionResult> DeletePaymentTypeByIdAsync([FromRoute] Guid paymentTypeId)
        {
            try
            {
                var response = await _paymentTypeService.DeletePaymentTypeByIdAsync(paymentTypeId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<PaymentType>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }


    }
}
