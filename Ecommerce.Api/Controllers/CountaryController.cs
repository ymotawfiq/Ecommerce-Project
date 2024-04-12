using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.CountaryRepository;
using Ecommerce.Service.Services.CountaryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Authorize(Roles = "User,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CountaryController : ControllerBase
    {
        private readonly ICountaryService _countaryService;
        public CountaryController(ICountaryService _countaryService)
        {
            this._countaryService = _countaryService;
        }

        [HttpGet("allcountaries")]
        public async Task<IActionResult> GetAllCountariesAsync()
        {
            try
            {
                var response = await _countaryService.GetAllCountariesAsync();
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<IEnumerable<Address>>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new List<Address>()
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<Countary>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new List<Countary>()
                    });
            }
        }

        [HttpPost("addcountary")]
        public async Task<IActionResult> AddCountaryAsync([FromBody] CountaryDto countaryDto)
        {
            try
            {
                var response = await _countaryService.AddCountaryAsync(countaryDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Address()
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<Countary>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new Countary()
                    });
            }
        }


        [HttpPut("updatecountary")]
        public async Task<IActionResult> UpdateCountaryAsync([FromBody] CountaryDto countaryDto)
        {
            try
            {
                var response = await _countaryService.UpdateCountaryAsync(countaryDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Address()
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<Countary>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new Countary()
                    });
            }
        }

        [HttpGet("getcountrybyid/{countaryId}")]
        public async Task<IActionResult> GetCountryByIdAsync([FromRoute] Guid countaryId)
        {
            try
            {
                var response = await _countaryService.GetCountryByIdAsync(countaryId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Address()
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<Countary>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new Countary()
                    });
            }
        }


        [HttpDelete("deletecountrybyid/{countaryId}")]
        public async Task<IActionResult> DeleteCountryByIdAsync([FromRoute] Guid countaryId)
        {
            try
            {
                var response = await _countaryService.DeleteCountryByIdAsync(countaryId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Address()
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<Countary>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new Countary()
                    });
            }
        }




    }
}
