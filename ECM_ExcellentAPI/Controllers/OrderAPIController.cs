using AutoMapper;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Model.Dto;
using ECM_ExcellentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECM_ExcellentAPI.Controllers
{
    [Route("apiOrderAPI")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        protected APIResponse _response;
        private IOrderRepository _dbOrder;
        private ICustomerRepository _dbCustomer;
        private IOrderStatusRepository _dbOrderStatus;
        private readonly ICompanyRepository _dbCompany;
        private readonly IUserRepository _dbUser;
        private readonly ICustomerAddressRepository _dbCustomerAddress;
        private readonly IMapper _mapper;
        public OrderAPIController(IOrderRepository dbOrder, ICustomerRepository dbCustomer,
            IOrderStatusRepository dbOrderStatus, IMapper mapper, ICompanyRepository dbCompany, IUserRepository dbUser, ICustomerAddressRepository dbCustomerAddress)
        {
            _dbOrder = dbOrder;
            _dbCustomer = dbCustomer;
            _dbOrderStatus = dbOrderStatus;
            _mapper = mapper;
            _response = new();
            _dbCompany = dbCompany;
            _dbUser = dbUser;
            _dbCustomerAddress = dbCustomerAddress;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetOrders()
        {
            try
            {
                IEnumerable<Order> orderList = await _dbOrder.GetAllAsync(includeProperties: "Company,Customer,OrderStatus,User,CustomerAddress");
                _response.Result = _mapper.Map<List<OrderDTO>>(orderList);
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

        [HttpGet("{id:int}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetOrder(int id)
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
                var order = await _dbOrder.GetAsync(u => u.Id == id);
                if (order == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Please Enter a correct ID");
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<OrderDTO>(order);
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
        public async Task<ActionResult<APIResponse>> CreateOrder([FromBody] OrderCreateDTO createDTO)
        {
            try
            {
                if (await _dbOrder.GetAsync(u => u.Id == createDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Order Id already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbCustomer.GetAsync(u => u.Id == createDTO.CustomerId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Customer Id is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbCompany.GetAsync(u => u.Id == createDTO.CompanyId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Company ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbOrderStatus.GetAsync(u => u.Id == createDTO.OrderStatusId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Order Status ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbCustomerAddress.GetAsync(u => u.Id == createDTO.ShipAddressId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Shipping Address ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Order order = _mapper.Map<Order>(createDTO);


                await _dbOrder.CreateAsync(order);
                _response.Result = _mapper.Map<OrderDTO>(order);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetOrder", new { id = order.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteOrder(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var order = await _dbOrder.GetAsync(u => u.Id == id);
                if (order == null)
                {
                    return NotFound();
                }
                await _dbOrder.RemoveAsync(order);
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
        [HttpPut("{id:int}", Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateOrder(int id, [FromBody] OrderUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                if (await _dbCustomer.GetAsync(u => u.Id == updateDTO.CustomerId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Customer ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbCompany.GetAsync(u => u.Id == updateDTO.CompanyId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Company ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbOrderStatus.GetAsync(u => u.Id == updateDTO.OrderStatusId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Order Status ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbCustomerAddress.GetAsync(u => u.Id == updateDTO.ShipAddressId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Shipping Address ID is Invalid !");
                    return BadRequest(ModelState);
                }


                Order model = _mapper.Map<Order>(updateDTO);


                await _dbOrder.UpdateAsync(model);
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
