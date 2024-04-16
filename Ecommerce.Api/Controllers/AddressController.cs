using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Service.Services.AddressService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Api.Controllers
{
    [Authorize(Roles ="Admin")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly UserManager<SiteUser> _userManager;
        public AddressController(IAddressService _addressService, UserManager<SiteUser> _userManager)
        {
            this._addressService = _addressService;
            this._userManager = _userManager;
        }

        [AllowAnonymous]
        [HttpGet("all-addresses")]
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

        [AllowAnonymous]
        [HttpGet("allAddressesByCountary/{countaryId}")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost("add-address")]
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

        [Authorize(Roles = "Admin")]
        [HttpPut("update-address")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("address/{addressId}")]
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-address/{addressId}")]
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
