using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.AddressRepository;
using Ecommerce.Repository.Repositories.CountaryRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddress _addressRepository;
        private readonly ICountary _countaryRepository;
        public AddressController(IAddress _addressRepository, ICountary _countaryRepository)
        {
            this._addressRepository = _addressRepository;
            this._countaryRepository = _countaryRepository;
        }


        [HttpGet("alladdresses")]
        public async Task<IActionResult> GetAllAddressesAsync()
        {
            try
            {
                var addresses = await _addressRepository.GetAllAddressesAsync();
                if (addresses.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK
                        , new ApiResponse<IEnumerable<Address>>
                        {
                            StatusCode = 200,
                            IsSuccess = true,
                            Message = "No addresses founded",
                            ResponseObject = addresses
                        });
                }
                return StatusCode(StatusCodes.Status200OK
                        , new ApiResponse<IEnumerable<Address>>
                        {
                            StatusCode = 200,
                            IsSuccess = true,
                            Message = "Addresses founded successfully",
                            ResponseObject = addresses
                        });
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
                var addresses = await _addressRepository.GetAllAddressesByCountaryIdAsync(countaryId);
                if (addresses.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK
                        , new ApiResponse<IEnumerable<Address>>
                        {
                            StatusCode = 200,
                            IsSuccess = true,
                            Message = "No addresses founded for this countary",
                            ResponseObject = addresses
                        });
                }
                return StatusCode(StatusCodes.Status200OK
                        , new ApiResponse<IEnumerable<Address>>
                        {
                            StatusCode = 200,
                            IsSuccess = true,
                            Message = "Addresses founded successfully",
                            ResponseObject = addresses
                        });
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
                if (addressDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new Address()
                    });
                }
                Countary countary = await _countaryRepository.GetCountaryByCountaryIdAsync(addressDto.CountaryId);
                if (countary == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No countaries founded with id ({addressDto.CountaryId})",
                        ResponseObject = new Address()
                    });
                }
                Address addedAddress = await _addressRepository.AddAddressAsync(
                    ConvertFromDto.ConvertFromAddressDto_Add(addressDto));
                return StatusCode(StatusCodes.Status201Created, new ApiResponse<Address>
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = $"Address added successfully",
                    ResponseObject = addedAddress
                });
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
                if (addressDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new Address()
                    });
                }
                if (addressDto.Id == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Id must not be null",
                        ResponseObject = new Address()
                    });
                }
                Address oldAddress = await _addressRepository.GetAddressByIdAsync(new Guid(addressDto.Id));
                if (oldAddress == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No addresses founded with id ({addressDto.Id})",
                        ResponseObject = new Address()
                    });
                }
                Countary countary = await _countaryRepository.GetCountaryByCountaryIdAsync(addressDto.CountaryId);
                if (countary == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No countaries founded with id ({addressDto.CountaryId})",
                        ResponseObject = new Address()
                    });
                }
                Address updatedAddress = await _addressRepository.UpdateAddressAsync(
                    ConvertFromDto.ConvertFromAddressDto_Update(addressDto));
                return StatusCode(StatusCodes.Status201Created, new ApiResponse<Address>
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = $"Address added successfully",
                    ResponseObject = updatedAddress
                });
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
                Address address = await _addressRepository.GetAddressByIdAsync(addressId);
                if(address == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No address founded with id ({addressId})",
                        ResponseObject = new Address()
                    });
                }
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<Address>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = $"Address founded successfully",
                    ResponseObject = address
                });
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
                Address address = await _addressRepository.GetAddressByIdAsync(addressId);
                if (address == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No address founded with id ({addressId})",
                        ResponseObject = new Address()
                    });
                }
                Address deletedAddress = await _addressRepository.DeleteAddressByIdAsync(addressId);
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<Address>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = $"Address deleted successfully",
                    ResponseObject = deletedAddress
                });
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
