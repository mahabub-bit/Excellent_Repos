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
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexCompany()
        {
            List<CompanyDTO> list = new();
            var response = await _companyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CompanyDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCompany()
        {
            return View();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCompany(CompanyCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _companyService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Company created successfully";
                    return RedirectToAction(nameof(IndexCompany));
                }
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCompany(int companyId)
        {
            var response = await _companyService.GetAsync<APIResponse>(companyId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                CompanyDTO model = JsonConvert.DeserializeObject<CompanyDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<CompanyUpdateDTO>(model));
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCompany(CompanyUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Company updated successfully";
                var response = await _companyService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexCompany));
                }
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCompany(int companyId)
        {
            var response = await _companyService.GetAsync<APIResponse>(companyId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                CompanyDTO model = JsonConvert.DeserializeObject<CompanyDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCompany(CompanyDTO model)
        {

            var response = await _companyService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Company deleted successfully";
                return RedirectToAction(nameof(IndexCompany));
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> Get_Companies()
        {
            PurchaseOrderCreateVM companyids = new();
            var response = await _companyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                companyids.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = i.CName.ToString(),
                    });
            }
            return Json(companyids);
        }

    }
}
