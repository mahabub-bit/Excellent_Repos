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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper, ICompanyService companyService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _companyService = companyService;
        }

        public async Task<IActionResult> IndexCategory()
        {
            List<CategoryDTO> list = new();
            var response = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCategory()
        {
            CategoryCreateVM categoryVM = new();
            var response = await _companyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                categoryVM.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(categoryVM);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryService.CreateAsync<APIResponse>(model.Category, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexCategory));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
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
        public async Task<IActionResult> UpdateCategory(int categoryId)
        {
            CategoryUpdateVM categoryVM = new();
            var response = await _categoryService.GetAsync<APIResponse>(categoryId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                CategoryDTO model = JsonConvert.DeserializeObject<CategoryDTO>(Convert.ToString(response.Result));
                categoryVM.Category = _mapper.Map<CategoryUpdateDTO>(model);
            }

            response = await _companyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                categoryVM.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
                        Value = i.Id.ToString()
                    }); 
                return View(categoryVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryService.UpdateAsync<APIResponse>(model.Category, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexCategory));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
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
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            CategoryDeleteVM categoryVM = new();
            var response = await _categoryService.GetAsync<APIResponse>(categoryId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                CategoryDTO model = JsonConvert.DeserializeObject<CategoryDTO>(Convert.ToString(response.Result));
                categoryVM.Category = model;
            }

            response = await _companyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                categoryVM.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
                        Value = i.Id.ToString()
                    });
                return View(categoryVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(CategoryDeleteVM model)
        {

            var response = await _categoryService.DeleteAsync<APIResponse>(model.Category.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexCategory));
            }

            return View(model);
        }

    }
}
