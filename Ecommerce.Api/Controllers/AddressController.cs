using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

using Ecommerce.Service.Services.AddressService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService _addressService)
        {
            this._addressService = _addressService;
        }


        [HttpGet("alladdresses")]
        public async Task<IActionResult> GetAllAddressesAsync()
        {
            try
            {
                var response = await _addressService.GetAllAddressesAsync();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<Address>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new List<Address>()
                    });
            }
        }

        [HttpGet("alladdressesbycountaryid/{countaryId}")]
        public async Task<IActionResult> GetAllAddressesByCountaryIdAsync([FromRoute] Guid countaryId)
        {
            try
            {
                var response = await _addressService.GetAllAddressesByCountaryIdAsync(countaryId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<Address>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new List<Address>()
                    });
            }
        }


        [HttpPost("addaddress")]
        public async Task<IActionResult> AddAddressAsync([FromBody] AddressDto addressDto)
        {
            try
            {
                var response = await _addressService.AddAddressAsync(addressDto);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<Address>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Address()
                });
            }
        }

        [HttpPut("updateaddress")]
        public async Task<IActionResult> UpdateAddressAsync([FromBody] AddressDto addressDto)
        {
            try
            {
                var response = await _addressService.UpdateAddressAsync(addressDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<Address>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Address()
                });
            }
        }


        [HttpGet("getaddressbyid/{addressId}")]
        public async Task<IActionResult> GetAddressByIdAsync([FromRoute] Guid addressId)
        {
            try
            {
                var response = await _addressService.GetAddressByIdAsync(addressId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<Address>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Address()
                });
            }
        }

        [HttpDelete("deleteaddressbyid/{addressId}")]
        public async Task<IActionResult> DeleteAddressByIdAsync([FromRoute] Guid addressId)
        {
            try
            {
                var response = await _addressService.DeleteAddressByIdAsync(addressId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<Address>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Address()
                });
            }
        }



    }
}
