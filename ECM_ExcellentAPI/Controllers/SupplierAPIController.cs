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
    [Route("api/SupplierAPI")]
    [ApiController]
    public class SupplierAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ISuppilerRepository _dbSupplier;
        private readonly ICompanyRepository _dbCompany;
        private readonly IMapper _mapper;
        public SupplierAPIController(ISuppilerRepository dbSupplier, IMapper mapper, ICompanyRepository dbCompany)
        {
            _dbSupplier = dbSupplier;
            _mapper = mapper;
            _response = new();
            _dbCompany = dbCompany;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetSuppliers()
        {
            try
            {
                IEnumerable<Supplier> supplierList = await _dbSupplier.GetAllAsync(includeProperties: "Company");
                _response.Result = _mapper.Map<List<SupplierDTO>>(supplierList);
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

        [HttpGet("{id:int}", Name = "GetSupplier")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetSupplier(int id)
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
                var supplier = await _dbSupplier.GetAsync(u => u.Id == id);
                if (supplier == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Please Enter a correct ID");
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<SupplierDTO>(supplier);
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
        public async Task<ActionResult<APIResponse>> CreateSupplier([FromBody] SupplierCreateDTO createDTO)
        {
            try
            {
                if (await _dbSupplier.GetAsync(u => u.Id == createDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Supplier Id already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbCompany.GetAsync(u => u.Id == createDTO.CompanyId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Company ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Supplier supplier = _mapper.Map<Supplier>(createDTO);


                await _dbSupplier.CreateAsync(supplier);
                _response.Result = _mapper.Map<SupplierDTO>(supplier);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetCompany", new { id = supplier.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteSupplier")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteSupplier(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var supplier = await _dbSupplier.GetAsync(u => u.Id == id);
                if (supplier == null)
                {
                    return NotFound();
                }
                await _dbSupplier.RemoveAsync(supplier);
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
        [HttpPut("{id:int}", Name = "UpdateSupplier")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateSupplier(int id, [FromBody] SupplierUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                if (await _dbCompany.GetAsync(u => u.Id == updateDTO.CompanyId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Company ID is Invalid !");
                    return BadRequest(ModelState);
                }


                Supplier model = _mapper.Map<Supplier>(updateDTO);


                await _dbSupplier.UpdateAsync(model);
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
