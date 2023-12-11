using AutoMapper;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Model.Dto;
using ECM_ExcellentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Net;

namespace ECM_ExcellentAPI.Controllers
{
    [Route("api/ProductRateHistoryAPI")]
    [ApiController]
    public class ProductRateHistoryAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IProductRateHistoryRepository _dbProductRates;
        private readonly IProductRepository _dbProduct;
        private readonly ICategoryTypeRepository _dbCategoryType;
        private readonly ICategoryRepository _dbCategory;
        private readonly IMapper _mapper;

        public ProductRateHistoryAPIController(IProductRateHistoryRepository dbProductRate, IProductRepository dbProduct, 
            ICategoryRepository dbCategory, ICategoryTypeRepository dbCategoryType, IMapper mapper)
        {
            _dbProductRates = dbProductRate;
            _dbProduct = dbProduct;
            _dbCategoryType = dbCategoryType;
            _mapper = mapper;
            _response = new();
            _dbCategory = dbCategory;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetProductRateHistories()
        {
            try
            {
                IEnumerable<Product_Rate_History> productRateHistoryList = await _dbProductRates.GetAllAsync(includeProperties: "Product,Category,CategoryType");
                _response.Result = _mapper.Map<List<ProductRateHistoryDTO>>(productRateHistoryList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetProductRateHistory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetProductRateHistory(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Enter a vaild ID");
                    return BadRequest(_response);
                }
                var productRateHistory = await _dbProductRates.GetAsync(u => u.Id == id);
                if (productRateHistory == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Please Enter a correct ID");
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<ProductRateHistoryDTO>(productRateHistory);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateProductRateHistory([FromBody] ProductRateHistoryCreateDTO createDTO)
        {
            try
            {
                if (await _dbProductRates.GetAsync(u => u.Id == createDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Product Id already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbProduct.GetAsync(u => u.Id == createDTO.ProductId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Product ID is Invalid !");
                    return BadRequest(ModelState);
                }


                if (await _dbCategory.GetAsync(u => u.Id == createDTO.CategoryId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Category ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbCategoryType.GetAsync(u => u.Id == createDTO.CategoryTypeId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "CategoryType ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Product_Rate_History productRateHistory = _mapper.Map<Product_Rate_History>(createDTO);


                await _dbProductRates.CreateAsync(productRateHistory);
                _response.Result = _mapper.Map<ProductRateHistoryDTO>(productRateHistory);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetProduct", new { id = productRateHistory.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{id:int}", Name = "DeleteProductRateHistory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteProductRateHistory(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var productRateHistory = await _dbProductRates.GetAsync(u => u.Id == id);
                if (productRateHistory == null)
                {
                    return NotFound();
                }
                await _dbProductRates.RemoveAsync(productRateHistory);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //[Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateProductRateHistory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateProductRateHistory(int id, [FromBody] ProductRateHistoryUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                if (await _dbProduct.GetAsync(u => u.Id == updateDTO.ProductId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Product ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbCategory.GetAsync(u => u.Id == updateDTO.CategoryId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Category ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbCategoryType.GetAsync(u => u.Id == updateDTO.CategoryTypeId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "CategoryType ID is Invalid !");
                    return BadRequest(ModelState);
                }

                Product_Rate_History model = _mapper.Map<Product_Rate_History>(updateDTO);


                await _dbProductRates.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
