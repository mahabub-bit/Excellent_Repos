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
using NuGet.Protocol;
using System.Data;

namespace ECM_ExcellentWeb.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        public SupplierController(ISupplierService supplierService, IMapper mapper, ICompanyService companyService)
        {
            _supplierService = supplierService;
            _mapper = mapper;
            _companyService = companyService;
        }

        public async Task<IActionResult> IndexSupplier()
        {
            List<SupplierDTO> list = new();
            var response = await _supplierService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<SupplierDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateSupplier()
        {
            SupplierCreateVM supplierVM = new();
            var response = await _companyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                supplierVM.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(supplierVM);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSupplier(SupplierCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _supplierService.CreateAsync<APIResponse>(model.Supplier, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexSupplier));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _supplierService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
                        Value = i.Id.ToString()
                    }); ;
            }

            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateSupplier(int supplierId)
        {
            SupplierUpdateVM supplierVM = new();
            var response = await _supplierService.GetAsync<APIResponse>(supplierId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                SupplierDTO model = JsonConvert.DeserializeObject<SupplierDTO>(Convert.ToString(response.Result));
                supplierVM.Supplier = _mapper.Map<SupplierUpdateDTO>(model);
            }

            response = await _companyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                supplierVM.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
                        Value = i.Id.ToString()
                    }); 
                return View(supplierVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSupplier(SupplierUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _supplierService.UpdateAsync<APIResponse>(model.Supplier, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexSupplier));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _supplierService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
                        Value = i.Id.ToString()
                    });
            }
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteSupplier(int supplierId)
        {
            SupplierDeleteVM supplierVM = new();
            var response = await _supplierService.GetAsync<APIResponse>(supplierId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                SupplierDTO model = JsonConvert.DeserializeObject<SupplierDTO>(Convert.ToString(response.Result));
                supplierVM.Supplier = model;
            }

            response = await _companyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                supplierVM.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
                        Value = i.Id.ToString()
                    });
                return View(supplierVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSupplier(SupplierDeleteVM model)
        {

            var response = await _supplierService.DeleteAsync<APIResponse>(model.Supplier.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexSupplier));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetSuppliersByCompanyID(string stateId)
        {
            int statId;
            PurchaseOrderCreateVM supplierVM = new PurchaseOrderCreateVM();
            List<SelectListItem> suppliers_ids = new List<SelectListItem>();
            List<SupplierDTO> suppliers = new List<SupplierDTO>();
            suppliers.Clear();
            suppliers_ids.Clear();
            if (!string.IsNullOrEmpty(stateId))
            {
                statId = Convert.ToInt32(stateId);
                
                
            }
            var response = await _supplierService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                supplierVM.SupplierList = JsonConvert.DeserializeObject<List<SupplierDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Supplier_Name,
                        Value = i.Id.ToString()
                    });
            }
            return Json(supplierVM);
        }

    }
}
