using AutoMapper;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Model.Dto;
using ECM_ExcellentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ECM_ExcellentAPI.Controllers
{
    [Route("api/OrderStatusAPI")]
    [ApiController]
    public class OrderStatusAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IOrderStatusRepository _dbOrderStatuses;
        private readonly IMapper _mapper;
        public OrderStatusAPIController(IOrderStatusRepository dbOrderStatuses, IMapper mapper)
        {
            _dbOrderStatuses = dbOrderStatuses;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetOrderStatuses()
        {
            try
            {
                IEnumerable<OrderStatus> orderStatusList = await _dbOrderStatuses.GetAllAsync();
                _response.Result = _mapper.Map<List<OrderStatusDTO>>(orderStatusList);
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

        [HttpGet("{id:int}", Name = "GetOrderStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetOrderStatus(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var orderStatus = await _dbOrderStatuses.GetAsync(u => u.Id == id);
                if (orderStatus == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<OrderStatusDTO>(orderStatus);
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
        public async Task<ActionResult<APIResponse>> CreateOrderStatus([FromBody] OrderStatusCreateDTO createDTO)
        {

            try
            {
                if (await _dbOrderStatuses.GetAsync(u => u.Status.ToLower() == createDTO.Status.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "OrderStatus already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                OrderStatus orderStatus = _mapper.Map<OrderStatus>(createDTO);



                await _dbOrderStatuses.CreateAsync(orderStatus);
                _response.Result = _mapper.Map<OrderStatusDTO>(orderStatus);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetOrderStatus", new { id = orderStatus.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteOrderStatus")]
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteOrderStatus(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var orderStatus = await _dbOrderStatuses.GetAsync(u => u.Id == id);
                if (orderStatus == null)
                {
                    return NotFound();
                }
                await _dbOrderStatuses.RemoveAsync(orderStatus);
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

        [HttpPut("{id:int}", Name = "UpdateOrderStatus")]
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateOrderStatus(int id, [FromBody] OrderStatusUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }


                OrderStatus model = _mapper.Map<OrderStatus>(updateDTO);


                await _dbOrderStatuses.UpdateAsync(model);
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
