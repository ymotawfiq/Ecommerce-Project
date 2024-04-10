using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;


namespace Ecommerce.Service.Services.AddressService
{
    public interface IAddressService
    {
        Task<ApiResponse<IEnumerable<Address>>> GetAllAddressesAsync();
        Task<ApiResponse<IEnumerable<Address>>> GetAllAddressesByCountaryIdAsync(Guid countaryId);
        Task<ApiResponse<Address>> AddAddressAsync(AddressDto addressDto);
        Task<ApiResponse<Address>> UpdateAddressAsync(AddressDto addressDto);
        Task<ApiResponse<Address>> GetAddressByIdAsync(Guid addressId);
        Task<ApiResponse<Address>> DeleteAddressByIdAsync(Guid addressId);
    }
}
