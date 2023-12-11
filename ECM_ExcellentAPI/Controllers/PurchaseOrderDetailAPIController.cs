using AutoMapper;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Model.Dto;
using ECM_ExcellentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECM_ExcellentAPI.Controllers
{
    [Route("api/PurchaseOrderDetailAPI")]
    [ApiController]
    public class PurchaseOrderDetailAPIController : ControllerBase
    {
        protected APIResponse _response;
        private IPurchaseOrderDetailRepository _dbPurchaseOrderDetail;
        private IPurchaseOrderRepository _dbPurchaseOrder;
        private IProductRepository _dbProduct;
        private readonly IMapper _mapper;
        public PurchaseOrderDetailAPIController(IPurchaseOrderDetailRepository dbPurchaseOrderDetail, IPurchaseOrderRepository dbPurchaseOrder, IProductRepository dbProduct, IMapper mapper)
        {
            _dbPurchaseOrderDetail = dbPurchaseOrderDetail;
            _dbPurchaseOrder = dbPurchaseOrder;
            _dbProduct = dbProduct;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetPurchaseOrderDetails()
        {
            try
            {
                IEnumerable<PurchaseOrderDetail> purchaseOrderDetailList = await _dbPurchaseOrderDetail.GetAllAsync(includeProperties: "PurchaseOrder,Product");
                _response.Result = _mapper.Map<List<PurchaseOrderDetailDTO>>(purchaseOrderDetailList);
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

        [HttpGet("{id:int}", Name = "GetPurchaseOrderDetail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetPurchaseOrderDetail(int id)
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
                var purchaseOrderDetail = await _dbPurchaseOrderDetail.GetAsync(u => u.Id == id);
                if (purchaseOrderDetail == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Please Enter a correct ID");
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<PurchaseOrderDetailDTO>(purchaseOrderDetail);
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
        public async Task<ActionResult<APIResponse>> CreatePurchaseOrderDetail([FromBody] PurchaseOrderDetailCreateDTO createDTO)
        {
            try
            {
                if (await _dbPurchaseOrderDetail.GetAsync(u => u.Id == createDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Purchase Order Detail Id already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbPurchaseOrder.GetAsync(u => u.Id == createDTO.PurchaseId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Purchase Order ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbProduct.GetAsync(u => u.Id == createDTO.ProductId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Product ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                PurchaseOrderDetail purchaseOrderDetail = _mapper.Map<PurchaseOrderDetail>(createDTO);


                await _dbPurchaseOrderDetail.CreateAsync(purchaseOrderDetail);
                _response.Result = _mapper.Map<PurchaseOrderDetailDTO>(purchaseOrderDetail);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetPurchaseOrderDetail", new { id = purchaseOrderDetail.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeletePurchaseOrderDetail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeletePurchaseOrderDetail(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var purchaseOrderDetail = await _dbPurchaseOrderDetail.GetAsync(u => u.Id == id);
                if (purchaseOrderDetail == null)
                {
                    return NotFound();
                }
                await _dbPurchaseOrderDetail.RemoveAsync(purchaseOrderDetail);
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
        [HttpPut("{id:int}", Name = "UpdatePurchaseOrderDetail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdatePurchaseOrderDetail(int id, [FromBody] PurchaseOrderDetailUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                if (await _dbPurchaseOrder.GetAsync(u => u.Id == updateDTO.PurchaseId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Purchase ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbProduct.GetAsync(u => u.Id == updateDTO.ProductId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Product ID is Invalid !");
                    return BadRequest(ModelState);
                }

                PurchaseOrderDetail model = _mapper.Map<PurchaseOrderDetail>(updateDTO);


                await _dbPurchaseOrderDetail.UpdateAsync(model);
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
