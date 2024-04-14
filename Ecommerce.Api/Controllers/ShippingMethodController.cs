using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ShippingMethodRepository;
using Ecommerce.Service.Services.ShippingMethodService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingMethodController : ControllerBase
    {
        private readonly IShippingMethodService _shippingMethodService;
        public ShippingMethodController(IShippingMethodService _shippingMethodService)
        {
            this._shippingMethodService = _shippingMethodService;
        }

        [AllowAnonymous]
        [HttpGet("allshipingmethods")]
        public async Task<IActionResult> GetAllShippingMethodAsync()
        {
            try
            {
                var response = await _shippingMethodService.GetAllShippingMethodAsync();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<ShippingMethod>>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addshipingmethod")]
        public async Task<IActionResult> AddShippingMethodAsync([FromBody] ShippingMethodDto shippingMethodDto)
        {
            try
            {
                var response = await _shippingMethodService.AddShippingMethodAsync(shippingMethodDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<ShippingMethod>
                {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateshipingmethod")]
        public async Task<IActionResult> UpdateShippingMethodAsync([FromBody] ShippingMethodDto shippingMethodDto)
        {
            try
            {
                var response = await _shippingMethodService.UpdateShippingMethodAsync(shippingMethodDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IShippingMethod>
                {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [AllowAnonymous]
        [HttpGet("getshipingmethodbyid/{shippinMethodId}")]
        public async Task<IActionResult> GetShippingMethodByIdAsync([FromRoute] Guid shippinMethodId)
        {
            try
            {
                var response = await _shippingMethodService.GetShippingMethodByIdAsync(shippinMethodId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<ShippingMethod>
                {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteshipingmethodbyid/{shippinMethodId}")]
        public async Task<IActionResult> DeleteShippingMethodByIdAsync([FromRoute] Guid shippinMethodId)
        {
            try
            {
                var response = await _shippingMethodService.DeleteShippingMethodByIdAsync(shippinMethodId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<ShippingMethod>
                {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }


    }
}
