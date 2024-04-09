using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.CountaryRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountaryController : ControllerBase
    {
        private readonly ICountary _countaryRepository;
        public CountaryController(ICountary _countaryRepository)
        {
            this._countaryRepository = _countaryRepository;
        }

        [HttpGet("allcountaries")]
        public IActionResult GetAllCountaries()
        {
            try
            {
                var countaries = _countaryRepository.GetAllCountaries();
                if (countaries.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<Countary>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No countaries founded",
                        ResponseObject = countaries
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<Countary>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Countaries founded successfully",
                        ResponseObject = countaries
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
        public IActionResult AddCountary([FromBody] CountaryDto countaryDto)
        {
            try
            {
                if (countaryDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Countary>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new Countary()
                    });
                }
                Countary newCountary = _countaryRepository.AddCountary(
                    ConvertFromDto.ConvertFromCountaryDto_Add(countaryDto));
                return StatusCode(StatusCodes.Status201Created
                    , new ApiResponse<Countary>
                    {
                        StatusCode = 201,
                        IsSuccess = true,
                        Message = "Country saved successfully",
                        ResponseObject = newCountary
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
        public IActionResult UpdateCountary([FromBody] CountaryDto countaryDto)
        {
            try
            {
                if (countaryDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Countary>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new Countary()
                    });
                }
                if (countaryDto.Id == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Countary>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Country id must not be null",
                        ResponseObject = new Countary()
                    });
                }
                Countary oldCountary = _countaryRepository.GetCountaryByCountaryId(new Guid(countaryDto.Id));
                if (oldCountary == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Countary>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No country founded with id ({countaryDto.Id})",
                        ResponseObject = new Countary()
                    });
                }
                Countary updatedCountary = _countaryRepository.UpdateCountary(
                    ConvertFromDto.ConvertFromCountaryDto_Update(countaryDto));
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<Countary>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Country updated successfully",
                        ResponseObject = updatedCountary
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
        public IActionResult GetCountryById([FromRoute] Guid countaryId)
        {
            try
            {
                Countary countary = _countaryRepository.GetCountaryByCountaryId(countaryId);
                if(countary == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Countary>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No country founded with id ({countaryId})",
                        ResponseObject = new Countary()
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<Countary>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = $"Countary founded successfully",
                        ResponseObject = countary
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
        public IActionResult DeleteCountryById([FromRoute] Guid countaryId)
        {
            try
            {
                Countary countary = _countaryRepository.GetCountaryByCountaryId(countaryId);
                if (countary == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Countary>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No country founded with id ({countaryId})",
                        ResponseObject = new Countary()
                    });
                }
                Countary deletedCountary = _countaryRepository.DeleteCountaryByCountaryId(countaryId);
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<Countary>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = $"Countary deleted successfully",
                        ResponseObject = deletedCountary
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
