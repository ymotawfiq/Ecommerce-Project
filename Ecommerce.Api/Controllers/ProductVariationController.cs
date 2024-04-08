using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Ecommerce.Repository.Repositories.ProductVariationRepository;
using Ecommerce.Repository.Repositories.VariationOptionsRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariationController : ControllerBase
    {
        private readonly IProductVariation _producVariationRepository;
        private readonly IVariationOptions _variationOptionsRepository;
        private readonly IProductItem _productItemRepository;
        public ProductVariationController(IProductVariation _producVariationRepository,
            IVariationOptions _variationOptionsRepository, IProductItem _productItemRepository)
        {
            this._productItemRepository = _productItemRepository;
            this._producVariationRepository = _producVariationRepository;
            this._variationOptionsRepository = _variationOptionsRepository;
        }

        [HttpGet("allproductvariations")]
        public IActionResult GetAllProductVariations()
        {
            try
            {
                var productVariations = _producVariationRepository.GetAllProductVariations();
                if (productVariations.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<ProductVariation>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No product variations founded",
                        ResponseObject = new List<ProductVariation>()
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<ProductVariation>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Product variations founded successfully",
                        ResponseObject = new List<ProductVariation>()
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<ProductVariation>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new List<ProductVariation>()
                    });
            }
        }

        [HttpGet("allproductvariationsbyvariationoptionid/{variationOptionId}")]
        public IActionResult GetAllProductVariationsByVariationOptionId([FromRoute]Guid variationOptionId)
        {
            try
            {
                var productVariations = _producVariationRepository.GetAllVariationsByVariationOptionId(variationOptionId);
                if (productVariations.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<ProductVariation>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No products founded for this variation",
                        ResponseObject = new List<ProductVariation>()
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<ProductVariation>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Product variations founded successfully",
                        ResponseObject = new List<ProductVariation>()
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<ProductVariation>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new List<ProductVariation>()
                    });
            }
        }


        [HttpGet("allproductvariationsbyproductitemid/{productItemId}")]
        public IActionResult GetAllProductVariationsByProductItemId([FromRoute] Guid productItemId)
        {
            try
            {
                var productVariations = _producVariationRepository.GetAllVariationsByProductItemId(productItemId);
                if (productVariations.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<ProductVariation>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No variations founded for this product",
                        ResponseObject = new List<ProductVariation>()
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<ProductVariation>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Product variations founded successfully",
                        ResponseObject = new List<ProductVariation>()
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<ProductVariation>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new List<ProductVariation>()
                    });
            }
        }

        [HttpPost("addproductvariation")]
        public IActionResult AddProductVariation([FromBody] ProductVariationDto productVariationDto)
        {
            try
            {
                if (productVariationDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new ProductVariation()
                    });
                }
                ProductItem productItem = _productItemRepository.GetProductItemById(productVariationDto.ProductItemId);
                if(productItem == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No product items founded with product id ({productVariationDto.ProductItemId})",
                        ResponseObject = new ProductVariation()
                    });
                }
                VariationOptions variationOptions = _variationOptionsRepository.GetVariationOptionsById(productVariationDto.VariationOptionId);
                if (variationOptions == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No variation options founded with id ({productVariationDto.VariationOptionId})",
                        ResponseObject = new ProductVariation()
                    });
                }

                ProductVariation productVariation = _producVariationRepository.AddProductVariation(
                    ConvertFromDto.ConvertFromProductVariationsDto_Add(productVariationDto));
                return StatusCode(StatusCodes.Status201Created
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 201,
                        IsSuccess = true,
                        Message = "Product variation created successfully",
                        ResponseObject = productVariation
                    });

            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new ProductVariation()
                    });
            }
        }


        [HttpPut("updateproductvariation")]
        public IActionResult UpdateProductVariation([FromBody] ProductVariationDto productVariationDto)
        {
            try
            {
                if (productVariationDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new ProductVariation()
                    });
                }
                if (productVariationDto.Id == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Product variation id must not be null",
                        ResponseObject = new ProductVariation()
                    });
                }
                ProductItem productItem = _productItemRepository.GetProductItemById(productVariationDto.ProductItemId);
                if (productItem == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No product items founded with product id ({productVariationDto.ProductItemId})",
                        ResponseObject = new ProductVariation()
                    });
                }
                VariationOptions variationOptions = _variationOptionsRepository.GetVariationOptionsById(productVariationDto.VariationOptionId);
                if (variationOptions == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No variation options founded with id ({productVariationDto.VariationOptionId})",
                        ResponseObject = new ProductVariation()
                    });
                }
                ProductVariation productVariation = _producVariationRepository.UpdateProductVariation(
                    ConvertFromDto.ConvertFromProductVariationsDto_Update(productVariationDto));
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Product variation updated successfully",
                        ResponseObject = productVariation
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new ProductVariation()
                    });
            }
        }

        [HttpGet("getproductvariationbyid/{productVariationId}")]
        public IActionResult GetProductVariationById([FromRoute] Guid productVariationId)
        {
            try
            {
                ProductVariation productVariation = _producVariationRepository
                    .GetProductVariationById(productVariationId);
                if (productVariation == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No product variation founded with id ({productVariationId})",
                        ResponseObject = new ProductVariation()
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Product variation founded successfully",
                        ResponseObject = productVariation
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new ProductVariation()
                    });
            }
        }

        [HttpDelete("deleteproductvariationbyid/{productVariationId}")]
        public IActionResult DeleteProductVariationById([FromRoute] Guid productVariationId)
        {
            try
            {
                ProductVariation productVariation = _producVariationRepository
                    .GetProductVariationById(productVariationId);
                if (productVariation == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No product variation founded with id ({productVariationId})",
                        ResponseObject = new ProductVariation()
                    });
                }
                var deletedProductVariation = _producVariationRepository
                    .DeleteProductVariationById(productVariationId);
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Product variation founded successfully",
                        ResponseObject = deletedProductVariation
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new ProductVariation()
                    });
            }
        }


    }
}
