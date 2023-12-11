using AutoMapper;
using ECM_ExcellentWeb.Model.Dto;
using ECM_ExcellentWeb.Models;
using ECM_ExcellentWeb.Models.VM;
using ECM_ExcellentWeb.Service.IService;
using ECM_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;

namespace ECM_ExcellentWeb.Controllers
{
    public class CustomerAddressController : Controller
    {
        private readonly ICustomerAddressService _customerAddressService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomerAddressController(ICustomerAddressService customerAddressService, IMapper mapper, ICustomerService customerService)
        {
            _customerAddressService = customerAddressService;
            _mapper = mapper;
            _customerService = customerService;
        }

        public async Task<IActionResult> IndexCustomerAddress()
        {
            List<CustomerAddressDTO> list = new();
            var response = await _customerAddressService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CustomerAddressDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCustomerAddress()
        {
            CustomerAddressCreateVM customerAddressVM = new();
            var response = await _customerService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                customerAddressVM.CustomerList = JsonConvert.DeserializeObject<List<CustomerDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CustomerName,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(customerAddressVM);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomerAddress(CustomerAddressCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _customerAddressService.CreateAsync<APIResponse>(model.CustomerAddress, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexCustomerAddress));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _customerAddressService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.CustomerList = JsonConvert.DeserializeObject<List<CustomerDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CustomerName,
                        Value = i.Id.ToString()
                    }); ;
            }

            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCustomerAddress(int customerAddressId)
        {
            CustomerAddressUpdateVM customerAddressVM = new();
            var response = await _customerAddressService.GetAsync<APIResponse>(customerAddressId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                CustomerAddressDTO model = JsonConvert.DeserializeObject<CustomerAddressDTO>(Convert.ToString(response.Result));
                customerAddressVM.CustomerAddress = _mapper.Map<CustomerAddressUpdateDTO>(model);
            }

            response = await _customerService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                customerAddressVM.CustomerList = JsonConvert.DeserializeObject<List<CustomerDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CustomerName,
                        Value = i.Id.ToString()
                    }); 
                return View(customerAddressVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomerAddress(CustomerAddressUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _customerAddressService.UpdateAsync<APIResponse>(model.CustomerAddress, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexCustomerAddress));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _customerAddressService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.CustomerList = JsonConvert.DeserializeObject<List<CustomerDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CustomerName,
                        Value = i.Id.ToString()
                    });
            }
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCustomerAddress(int customerAddressId)
        {
            CustomerAddressDeleteVM customerAddressVM = new();
            var response = await _customerAddressService.GetAsync<APIResponse>(customerAddressId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                CustomerAddressDTO model = JsonConvert.DeserializeObject<CustomerAddressDTO>(Convert.ToString(response.Result));
                customerAddressVM.CustomerAddress = model;
            }

            response = await _customerService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                customerAddressVM.CustomerList = JsonConvert.DeserializeObject<List<CustomerDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CustomerName,
                        Value = i.Id.ToString()
                    });
                return View(customerAddressVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCustomerAddress(CustomerAddressDeleteVM model)
        {

            var response = await _customerAddressService.DeleteAsync<APIResponse>(model.CustomerAddress.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexCustomerAddress));
            }

            return View(model);
        }

    }
}
