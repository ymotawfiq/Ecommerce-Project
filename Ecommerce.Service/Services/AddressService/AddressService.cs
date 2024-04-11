

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.AddressRepository;
using Ecommerce.Repository.Repositories.CountaryRepository;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly IAddress _addressRepository;
        private readonly ICountary _countaryRepository;
        public AddressService(IAddress _addressRepository, ICountary _countaryRepository)
        {
            this._addressRepository = _addressRepository;
            this._countaryRepository = _countaryRepository;
        }
        public async Task<ApiResponse<Address>> AddAddressAsync(AddressDto addressDto)
        {
            if (addressDto == null)
            {
                return new ApiResponse<Address>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null",
                    ResponseObject = new Address()
                };
            }
            Countary countary = await _countaryRepository.GetCountaryByCountaryIdAsync(addressDto.CountaryId);
            if (countary == null)
            {
                return new ApiResponse<Address>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No countaries founded with id ({addressDto.CountaryId})",
                    ResponseObject = new Address()
                };
            }
            Address addedAddress = await _addressRepository.AddAddressAsync(
                ConvertFromDto.ConvertFromAddressDto_Add(addressDto));
            return new ApiResponse<Address>
            {
                StatusCode = 201,
                IsSuccess = true,
                Message = $"Address added successfully",
                ResponseObject = addedAddress
            };
        }

        public async Task<ApiResponse<Address>> DeleteAddressByIdAsync(Guid addressId)
        {
            Address address = await _addressRepository.GetAddressByIdAsync(addressId);
            if (address == null)
            {
                return new ApiResponse<Address>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No address founded with id ({addressId})",
                    ResponseObject = new Address()
                };
            }
            Address deletedAddress = await _addressRepository.DeleteAddressByIdAsync(addressId);
            return new ApiResponse<Address>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = $"Address deleted successfully",
                ResponseObject = deletedAddress
            };
        }

        public async Task<ApiResponse<Address>> GetAddressByIdAsync(Guid addressId)
        {
            Address address = await _addressRepository.GetAddressByIdAsync(addressId);
            if (address == null)
            {
                return new ApiResponse<Address>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No address founded with id ({addressId})",
                    ResponseObject = new Address()
                };
            }
            return new ApiResponse<Address>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = $"Address founded successfully",
                ResponseObject = address
            };
        }

        public async Task<ApiResponse<IEnumerable<Address>>> GetAllAddressesAsync()
        {
            var addresses = await _addressRepository.GetAllAddressesAsync();
            if (addresses.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<Address>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No addresses founded",
                        ResponseObject = addresses
                    };
            }
            return new ApiResponse<IEnumerable<Address>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Addresses founded successfully",
                        ResponseObject = addresses
                    };
        }

        public async Task<ApiResponse<IEnumerable<Address>>> GetAllAddressesByCountaryIdAsync(Guid countaryId)
        {
            var addresses = await _addressRepository.GetAllAddressesByCountaryIdAsync(countaryId);
            if (addresses.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<Address>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No addresses founded for this countary",
                        ResponseObject = addresses
                    };
            }
            return new ApiResponse<IEnumerable<Address>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Addresses founded successfully",
                        ResponseObject = addresses
                    };
        }

        public async Task<ApiResponse<Address>> UpdateAddressAsync(AddressDto addressDto)
        {
            if (addressDto == null)
            {
                return new ApiResponse<Address>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null",
                    ResponseObject = new Address()
                };
            }
            if (addressDto.Id == null)
            {
                return new ApiResponse<Address>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Id must not be null",
                    ResponseObject = new Address()
                };
            }
            Address oldAddress = await _addressRepository.GetAddressByIdAsync(new Guid(addressDto.Id));
            if (oldAddress == null)
            {
                return new ApiResponse<Address>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No addresses founded with id ({addressDto.Id})",
                    ResponseObject = new Address()
                };
            }
            Countary countary = await _countaryRepository.GetCountaryByCountaryIdAsync(addressDto.CountaryId);
            if (countary == null)
            {
                return new ApiResponse<Address>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No countaries founded with id ({addressDto.CountaryId})",
                    ResponseObject = new Address()
                };
            }
            Address updatedAddress = await _addressRepository.UpdateAddressAsync(
                ConvertFromDto.ConvertFromAddressDto_Update(addressDto));
            return new ApiResponse<Address>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = $"Address updated successfully",
                ResponseObject = updatedAddress
            };
        }
    }
}
