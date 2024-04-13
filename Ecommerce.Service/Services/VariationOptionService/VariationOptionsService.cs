

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.VariationOptionsRepository;
using Ecommerce.Repository.Repositories.VariationRepository;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.Services.VariationOptionService
{
    public class VariationOptionsService :IVariationOptionsService
    {
        private readonly IVariationOptions _variationOptionsRepository;
        private readonly IVariation _variationRepository;
        public VariationOptionsService(IVariationOptions _variationOptionsRepository,
            IVariation _variationRepository)
        {
            this._variationOptionsRepository = _variationOptionsRepository;
            this._variationRepository = _variationRepository;
        }

        public async Task<ApiResponse<VariationOptions>> AddVariationOptionAsync(VariationOptionsDto variationOptionsDto)
        {
            if (variationOptionsDto == null)
            {
                return new ApiResponse<VariationOptions>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null",
                    ResponseObject = new VariationOptions()
                };
            }
            Variation variation = await _variationRepository.GetVariationByIdAsync
                (new Guid(variationOptionsDto.VariationId));
            if (variation == null)
            {
                return new ApiResponse<VariationOptions>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No variations found with id ({variationOptionsDto.VariationId})",
                    ResponseObject = new VariationOptions()
                };
            }
            VariationOptions variationOptions = await _variationOptionsRepository.AddVariationOptionsAsync(
                ConvertFromDto.ConvertFromVariationOptions_Add(variationOptionsDto));
            return new ApiResponse<VariationOptions>
            {
                StatusCode = 201,
                IsSuccess = true,
                Message = $"Variation option created successfully",
                ResponseObject = variationOptions
            };
        }

        public async Task<ApiResponse<VariationOptions>> DeleteVariationOptionByVariationIdAsync(Guid variationOptionId)
        {
            VariationOptions variationOptions = await _variationOptionsRepository
                                .GetVariationOptionsByIdAsync(variationOptionId);
            if (variationOptions == null)
            {
                return new ApiResponse<VariationOptions>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No variation options found with id ({variationOptionId})",
                    ResponseObject = new VariationOptions()
                };
            }
            VariationOptions deletedVariationOption = await _variationOptionsRepository
                .DeleteVariationOptionsByIdAsync(variationOptionId);
            return new ApiResponse<VariationOptions>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Variation option deleted successfully",
                ResponseObject = deletedVariationOption
            };
        }

        public async Task<ApiResponse<IEnumerable<VariationOptions>>> GetAllVariationOptionsAsync()
        {
            var variationOptions = await _variationOptionsRepository.GetAllVariationOptionsAsync();
            if (variationOptions.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<VariationOptions>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "No variation options found",
                    ResponseObject = variationOptions
                };
            }
            return new ApiResponse<IEnumerable<VariationOptions>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Variation options found successfully",
                    ResponseObject = variationOptions
                };
        }

        public async Task<ApiResponse<IEnumerable<VariationOptions>>> GetAllVariationOptionsByVariationIdAsync(Guid variationId)
        {
            var variationOptions = await _variationOptionsRepository.GetAllVariationOptionsByVariationIdAsync
                                (variationId);
            if (variationOptions.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<VariationOptions>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "No variation options found",
                    ResponseObject = variationOptions
                };
            }
            return new ApiResponse<IEnumerable<VariationOptions>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Variation options found successfully",
                    ResponseObject = variationOptions
                };
        }

        public async Task<ApiResponse<VariationOptions>> GetVariationOptionByVariationIdAsync(Guid variationOptionId)
        {
            VariationOptions variationOptions = await _variationOptionsRepository.GetVariationOptionsByIdAsync
                                (variationOptionId);
            if (variationOptions == null)
            {
                return new ApiResponse<VariationOptions>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No variation options found with id ({variationOptionId})",
                    ResponseObject = new VariationOptions()
                };
            }
            return new ApiResponse<VariationOptions>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Variation option found successfully",
                ResponseObject = variationOptions
            };
        }

        public async Task<ApiResponse<VariationOptions>> UpdateVariationOptionAsync(VariationOptionsDto variationOptionsDto)
        {
            if (variationOptionsDto == null)
            {
                return new ApiResponse<VariationOptions>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null",
                    ResponseObject = new VariationOptions()
                };
            }
            if (variationOptionsDto.Id == null)
            {
                return new ApiResponse<VariationOptions>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Variation option id must not be null",
                    ResponseObject = new VariationOptions()
                };
            }
            Variation variation = await _variationRepository.GetVariationByIdAsync
                (new Guid(variationOptionsDto.VariationId));
            if (variation == null)
            {
                return new ApiResponse<VariationOptions>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No variations found with id ({variationOptionsDto.VariationId})",
                    ResponseObject = new VariationOptions()
                };
            }
            VariationOptions oldVariationOptions = await _variationOptionsRepository
                .GetVariationOptionsByIdAsync(new Guid(variationOptionsDto.Id));
            if (oldVariationOptions == null)
            {
                return new ApiResponse<VariationOptions>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"No variation options found with id ({variationOptionsDto.Id})",
                    ResponseObject = new VariationOptions()
                };
            }
            VariationOptions variationOptions = await _variationOptionsRepository.UpdateVariationOptionsAsync(
                ConvertFromDto.ConvertFromVariationOptions_Update(variationOptionsDto));
            return new ApiResponse<VariationOptions>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = $"Variation option updated successfully",
                ResponseObject = variationOptions
            };
        }
    }
}
