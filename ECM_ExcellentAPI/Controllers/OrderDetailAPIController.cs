using AutoMapper;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Model.Dto;
using ECM_ExcellentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECM_ExcellentAPI.Controllers
{
    [Route("api/OrderDetailAPI")]
    [ApiController]
    public class OrderDetailAPIController : ControllerBase
    {
        protected APIResponse _response;
        private IOrderDetailRepository _dbOrderDetail;
        private IOrderRepository _dbOrder;
        private IProductRepository _dbProduct;
        private IOrderStatusRepository _dbOrderStatus;
        private readonly ICompanyRepository _dbCompany;
        private readonly IMapper _mapper;
        public OrderDetailAPIController(IOrderDetailRepository dbPurchaseOrder, IOrderRepository dbOrder,
            IProductRepository dbProduct, IOrderStatusRepository dbOrderStatus, IMapper mapper, ICompanyRepository dbCompany)
        {
            _dbOrderDetail = dbPurchaseOrder;
            _dbOrder = dbOrder;
            _dbProduct = dbProduct;
            _dbOrderStatus = dbOrderStatus;
            _mapper = mapper;
            _response = new();
            _dbCompany = dbCompany;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetOrdersDetail()
        {
            try
            {
                IEnumerable<OrderDetail> orderDetailList = await _dbOrderDetail.GetAllAsync(includeProperties: "Company,OrderStatus,Product,Order");
                _response.Result = _mapper.Map<List<OrderDetailDTO>>(orderDetailList);
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

        [HttpGet("{id:int}", Name = "GetOrderDetail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetOrderDetail(int id)
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
                var orderDetail = await _dbOrderDetail.GetAsync(u => u.Id == id);
                if (orderDetail == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Please Enter a correct ID");
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<OrderDetailDTO>(orderDetail);
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
        public async Task<ActionResult<APIResponse>> CreateOrderDetail([FromBody] OrderDetailCreateDTO createDTO)
        {
            try
            {
                if (await _dbOrderDetail.GetAsync(u => u.Id == createDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Order Detail Id already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbOrder.GetAsync(u => u.Id == createDTO.OrderId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Order Id is Invalid !");
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

                if (await _dbOrderStatus.GetAsync(u => u.Id == createDTO.OrderDeatailStatusId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Order Detail Status ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                OrderDetail orderDetail = _mapper.Map<OrderDetail>(createDTO);


                await _dbOrderDetail.CreateAsync(orderDetail);
                _response.Result = _mapper.Map<OrderDetailDTO>(orderDetail);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetOrderDetail", new { id = orderDetail.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteOrderDetail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteOrderDetail(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var orderDetail = await _dbOrderDetail.GetAsync(u => u.Id == id);
                if (orderDetail == null)
                {
                    return NotFound();
                }
                await _dbOrderDetail.RemoveAsync(orderDetail);
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
        [HttpPut("{id:int}", Name = "UpdateOrderDetail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateOrderDetail(int id, [FromBody] OrderDetailUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                if (await _dbOrder.GetAsync(u => u.Id == updateDTO.OrderId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Order ID is Invalid !");
                    return BadRequest(ModelState);
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

                if (await _dbOrderStatus.GetAsync(u => u.Id == updateDTO.OrderDeatailStatusId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Order Detail Status ID is Invalid !");
                    return BadRequest(ModelState);
                }


                OrderDetail model = _mapper.Map<OrderDetail>(updateDTO);


                await _dbOrderDetail.UpdateAsync(model);
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
