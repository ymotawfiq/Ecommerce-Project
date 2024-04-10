

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.CountaryRepository;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.Services.CountaryService
{
    public class CountaryService : ICountaryService
    {
        private readonly ICountary _countaryRepository;
        public CountaryService(ICountary _countaryRepository)
        {
            this._countaryRepository = _countaryRepository;
        }
        public async Task<ApiResponse<Countary>> AddCountaryAsync(CountaryDto countaryDto)
        {
            if (countaryDto == null)
            {
                return new ApiResponse<Countary>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null",
                    ResponseObject = new Countary()
                };
            }
            Countary newCountary = await _countaryRepository.AddCountaryAsync(
                ConvertFromDto.ConvertFromCountaryDto_Add(countaryDto));
            return new ApiResponse<Countary>
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = "Country saved successfully",
                    ResponseObject = newCountary
                };
        }

        public async Task<ApiResponse<Countary>> DeleteCountryByIdAsync(Guid countaryId)
        {
            Countary countary = await _countaryRepository.GetCountaryByCountaryIdAsync(countaryId);
            if (countary == null)
            {
                return new ApiResponse<Countary>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No country founded with id ({countaryId})",
                    ResponseObject = new Countary()
                };
            }
            Countary deletedCountary = await _countaryRepository.DeleteCountaryByCountaryIdAsync(countaryId);
            return new ApiResponse<Countary>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = $"Countary deleted successfully",
                    ResponseObject = deletedCountary
                };
        }

        public async Task<ApiResponse<IEnumerable<Countary>>> GetAllCountariesAsync()
        {
            var countaries = await _countaryRepository.GetAllCountariesAsync();
            if (countaries.ToList().Count == 0)
            {
                new ApiResponse<IEnumerable<Countary>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "No countaries founded",
                    ResponseObject = countaries
                };
            }
            return new ApiResponse<IEnumerable<Countary>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Countaries founded successfully",
                    ResponseObject = countaries
                };
        }

        public async Task<ApiResponse<Countary>> GetCountryByIdAsync(Guid countaryId)
        {
            Countary countary = await _countaryRepository.GetCountaryByCountaryIdAsync(countaryId);
            if (countary == null)
            {
                return new ApiResponse<Countary>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No country founded with id ({countaryId})",
                    ResponseObject = new Countary()
                };
            }
            return new ApiResponse<Countary>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = $"Countary founded successfully",
                    ResponseObject = countary
                };
        }

        public async Task<ApiResponse<Countary>> UpdateCountaryAsync(CountaryDto countaryDto)
        {
            if (countaryDto == null)
            {
                return new ApiResponse<Countary>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null",
                    ResponseObject = new Countary()
                };
            }
            if (countaryDto.Id == null)
            {
                return new ApiResponse<Countary>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Country id must not be null",
                    ResponseObject = new Countary()
                };
            }
            Countary oldCountary = await _countaryRepository
                .GetCountaryByCountaryIdAsync(new Guid(countaryDto.Id));
            if (oldCountary == null)
            {
                return new ApiResponse<Countary>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No country founded with id ({countaryDto.Id})",
                    ResponseObject = new Countary()
                };
            }
            Countary updatedCountary = await _countaryRepository.UpdateCountaryAsync(
                ConvertFromDto.ConvertFromCountaryDto_Update(countaryDto));
            return new ApiResponse<Countary>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Country updated successfully",
                    ResponseObject = updatedCountary
                };
        }
    }
}
