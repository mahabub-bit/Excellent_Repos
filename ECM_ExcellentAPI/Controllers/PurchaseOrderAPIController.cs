using AutoMapper;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Model.Dto;
using ECM_ExcellentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECM_ExcellentAPI.Controllers
{
    [Route("api/PurchaseOrderAPI")]
    [ApiController]
    public class PurchaseOrderAPIController : ControllerBase
    {
        protected APIResponse _response;
        private IPurchaseOrderRepository _dbPurchaseOrder;
        private IProductRepository _dbProduct;
        private ISuppilerRepository _dbSuppiler;
        private readonly ICompanyRepository _dbCompany;
        private readonly IUserRepository _dbUser;
        private readonly IMapper _mapper;
        public PurchaseOrderAPIController(IPurchaseOrderRepository dbPurchaseOrder, IProductRepository dbProduct, ISuppilerRepository dbSupplier, IMapper mapper, ICompanyRepository dbCompany, IUserRepository dbUser)
        {
            _dbPurchaseOrder = dbPurchaseOrder;
            _dbProduct = dbProduct;
            _dbSuppiler = dbSupplier;
            _mapper = mapper;
            _response = new();
            _dbCompany = dbCompany;
            _dbUser = dbUser;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetPurchaseOrders()
        {
            try
            {
                IEnumerable<PurchaseOrder> purchaseOrderList = await _dbPurchaseOrder.GetAllAsync(includeProperties: "Company,Supplier,Product,User");
                _response.Result = _mapper.Map<List<PurchaseOrderDTO>>(purchaseOrderList);
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

        [HttpGet("{id:int}", Name = "GetPurchaseOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetPurchaseOrder(int id)
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
                var product = await _dbPurchaseOrder.GetAsync(u => u.Id == id);
                if (product == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Please Enter a correct ID");
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<PurchaseOrderDTO>(product);
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
        public async Task<ActionResult<APIResponse>> CreatePurchaseOrder([FromBody] PurchaseOrderCreateDTO createDTO)
        {
            try
            {
                if (await _dbPurchaseOrder.GetAsync(u => u.Id == createDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Purchase Order Id already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbProduct.GetAsync(u => u.Id == createDTO.ProductId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Product Id is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbCompany.GetAsync(u => u.Id == createDTO.CompanyId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Company ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbSuppiler.GetAsync(u => u.Id == createDTO.SupplierId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Supplier ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                PurchaseOrder purchaseOrder = _mapper.Map<PurchaseOrder>(createDTO);


                await _dbPurchaseOrder.CreateAsync(purchaseOrder);
                _response.Result = _mapper.Map<PurchaseOrderDTO>(purchaseOrder);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetPurchaseOrder", new { id = purchaseOrder.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeletePurchaseOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeletePurchaseOrder(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var purchaseOrder = await _dbPurchaseOrder.GetAsync(u => u.Id == id);
                if (purchaseOrder == null)
                {
                    return NotFound();
                }
                await _dbPurchaseOrder.RemoveAsync(purchaseOrder);
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
        [HttpPut("{id:int}", Name = "UpdatePurchaseOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdatePurchaseOrder(int id, [FromBody] PurchaseOrderUpdateDTO updateDTO)
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

                if (await _dbCompany.GetAsync(u => u.Id == updateDTO.CompanyId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Company ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbSuppiler.GetAsync(u => u.Id == updateDTO.SupplierId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Supplier ID is Invalid !");
                    return BadRequest(ModelState);
                }


                PurchaseOrder model = _mapper.Map<PurchaseOrder>(updateDTO);


                await _dbPurchaseOrder.UpdateAsync(model);
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
