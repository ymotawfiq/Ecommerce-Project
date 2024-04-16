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
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class CountaryController : ControllerBase
    {
        private readonly ICountaryService _countaryService;
        public CountaryController(ICountaryService _countaryService)
        {
            this._countaryService = _countaryService;
        }

        [AllowAnonymous]
        [HttpGet("all-countaries")]
        public async Task<IActionResult> GetAllCountariesAsync()
        {
            try
            {
                var response = await _countaryService.GetAllCountariesAsync();
                return Ok(response);
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

        [Authorize(Roles = "Admin")]
        [HttpPost("add-countary")]
        public async Task<IActionResult> AddCountaryAsync([FromBody] CountaryDto countaryDto)
        {
            try
            {
                var response = await _countaryService.AddCountaryAsync(countaryDto);
                return Ok(response);
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

        [Authorize(Roles = "Admin")]
        [HttpPut("update-countary")]
        public async Task<IActionResult> UpdateCountaryAsync([FromBody] CountaryDto countaryDto)
        {
            try
            {
                var response = await _countaryService.UpdateCountaryAsync(countaryDto);
                return Ok(response);
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

        [Authorize(Roles = "Admin")]
        [HttpGet("country/{countaryId}")]
        public async Task<IActionResult> GetCountryByIdAsync([FromRoute] Guid countaryId)
        {
            try
            {
                var response = await _countaryService.GetCountryByIdAsync(countaryId);
                return Ok(response);
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-country/{countaryId}")]
        public async Task<IActionResult> DeleteCountryByIdAsync([FromRoute] Guid countaryId)
        {
            try
            {
                var response = await _countaryService.DeleteCountryByIdAsync(countaryId);
                return Ok(response);
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
