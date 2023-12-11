using AutoMapper;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Model.Dto;
using ECM_ExcellentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace ECM_ExcellentAPI.Controllers
{
    [Route("api/CustomerAddressAPI")]
    [ApiController]
    public class CustomerAddressAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ICustomerAddressRepository _dbCustomerAddress;
        private readonly ICustomerRepository _dbCustomer;
        private readonly IMapper _mapper;
        public CustomerAddressAPIController(ICustomerAddressRepository dbCustomerAddress, IMapper mapper, ICustomerRepository dbCustomer)
        {
            
            _dbCustomerAddress = dbCustomerAddress;
            _mapper = mapper;
            _response = new();
            _dbCustomer = dbCustomer;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetCustomersAddress()
        {
            try
            {
                IEnumerable<CustomerAddress> customerAddressList = await _dbCustomerAddress.GetAllAsync(includeProperties: "Customer");
                _response.Result = _mapper.Map<List<CustomerAddressDTO>>(customerAddressList);
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

        [HttpGet("{id:int}", Name = "GetCustomerAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetCustomerAddress(int id)
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
                var customerAddress = await _dbCustomerAddress.GetAsync(u => u.Id == id);
                if (customerAddress == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Please Enter a correct ID");
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CustomerAddressDTO>(customerAddress);
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
        public async Task<ActionResult<APIResponse>> CreateCustomerAddress([FromBody] CustomerAddressCreateDTO createDTO)
        {
            try
            {
                if (await _dbCustomerAddress.GetAsync(u => u.Id == createDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "CustomerAddress Id already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbCustomer.GetAsync(u => u.Id == createDTO.CustomerId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Customer ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                CustomerAddress customerAddress = _mapper.Map<CustomerAddress>(createDTO);


                await _dbCustomerAddress.CreateAsync(customerAddress);
                _response.Result = _mapper.Map<CustomerAddressDTO>(customerAddress);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetCompany", new { id = customerAddress.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteCustomerAddress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteCustomerAddress(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var customerAddress = await _dbCustomerAddress.GetAsync(u => u.Id == id);
                if (customerAddress == null)
                {
                    return NotFound();
                }
                await _dbCustomerAddress.RemoveAsync(customerAddress);
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
        [HttpPut("{id:int}", Name = "UpdateCustomerAddress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateCustomerAddress(int id, [FromBody] CustomerAddressUpdateDTO updateDTO)
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


                CustomerAddress model = _mapper.Map<CustomerAddress>(updateDTO);


                await _dbCustomerAddress.UpdateAsync(model);
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
