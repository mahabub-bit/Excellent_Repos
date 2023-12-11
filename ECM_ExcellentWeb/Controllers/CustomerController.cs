using AutoMapper;
using ECM_ExcellentWeb.Model.Dto;
using ECM_ExcellentWeb.Models;
using ECM_ExcellentWeb.Service.IService;
using ECM_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace ECM_ExcellentWeb.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerService companyService, IMapper mapper)
        {
            _customerService = companyService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexCustomer()
        {
            List<CustomerDTO> list = new();
            var response = await _customerService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CustomerDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCustomer()
        {
            return View();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer(CustomerCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _customerService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Customer created successfully";
                    return RedirectToAction(nameof(IndexCustomer));
                }
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCustomer(int customerId)
        {
            var response = await _customerService.GetAsync<APIResponse>(customerId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                CustomerDTO model = JsonConvert.DeserializeObject<CustomerDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<CustomerUpdateDTO>(model));
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomer(CustomerUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Customer updated successfully";
                var response = await _customerService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexCustomer));
                }
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            var response = await _customerService.GetAsync<APIResponse>(customerId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                CustomerDTO model = JsonConvert.DeserializeObject<CustomerDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCustomer(CustomerDTO model)
        {

            var response = await _customerService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Customer deleted successfully";
                return RedirectToAction(nameof(IndexCustomer));
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }
    }
}
