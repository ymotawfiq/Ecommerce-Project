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
        public IActionResult GetAllAddresses()
        {
            try
            {
                var addresses = _addressRepository.GetAllAddresses();
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
        public IActionResult GetAllAddressesByCountaryId([FromRoute] Guid countaryId)
        {
            try
            {
                var addresses = _addressRepository.GetAllAddressesByCountaryId(countaryId);
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
        public IActionResult AddAddress([FromBody] AddressDto addressDto)
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
                Countary countary = _countaryRepository.GetCountaryByCountaryId(addressDto.CountaryId);
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
                Address addedAddress = _addressRepository.AddAddress(
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
        public IActionResult UpdateAddress([FromBody] AddressDto addressDto)
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
                Address oldAddress = _addressRepository.GetAddressById(new Guid(addressDto.Id));
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
                Countary countary = _countaryRepository.GetCountaryByCountaryId(addressDto.CountaryId);
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
                Address updatedAddress = _addressRepository.UpdateAddress(
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
        public IActionResult GetAddressById([FromRoute] Guid addressId)
        {
            try
            {
                Address address = _addressRepository.GetAddressById(addressId);
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
        public IActionResult DeleteAddressById([FromRoute] Guid addressId)
        {
            try
            {
                Address address = _addressRepository.GetAddressById(addressId);
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
                Address deletedAddress = _addressRepository.DeleteAddressById(addressId);
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
