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
    public class CategoryTypeController : Controller
    {
        private readonly ICategoryTypeService _categoryTypeService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryTypeController(ICategoryTypeService categoryTypeService, IMapper mapper, ICategoryService categoryService)
        {
            _categoryTypeService = categoryTypeService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> IndexCategoryType()
        {
            List<CategoryTypeDTO> list = new();
            var response = await _categoryTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CategoryTypeDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCategoryType()
        {
            CategoryTypeCreateVM categoryTypeVM = new();
            var response = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                categoryTypeVM.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CategoryName,
                        Value = i.Id.ToString()
                    }); ;
            }
            
            return View(categoryTypeVM);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategoryType(CategoryTypeCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryTypeService.CreateAsync<APIResponse>(model.CategoryType, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexCategoryType));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _categoryTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CategoryName,
                        Value = i.Id.ToString()
                    }); ;
            }

            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCategoryType(int categoryTypeId)
        {
            CategoryTypeUpdateVM categoryTypeVM = new();
            var response = await _categoryTypeService.GetAsync<APIResponse>(categoryTypeId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                CategoryTypeDTO model = JsonConvert.DeserializeObject<CategoryTypeDTO>(Convert.ToString(response.Result));
                categoryTypeVM.CategoryType = _mapper.Map<CategoryTypeUpdateDTO>(model);
            }

            response = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                categoryTypeVM.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CategoryName,
                        Value = i.Id.ToString()
                    }); 
                return View(categoryTypeVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategoryType(CategoryTypeUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryTypeService.UpdateAsync<APIResponse>(model.CategoryType, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexCategoryType));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _categoryTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CategoryName,
                        Value = i.Id.ToString()
                    });
            }
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategoryType(int categoryTypeId)
        {
            CategoryTypeDeleteVM categoryTypeVM = new();
            var response = await _categoryTypeService.GetAsync<APIResponse>(categoryTypeId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                CategoryTypeDTO model = JsonConvert.DeserializeObject<CategoryTypeDTO>(Convert.ToString(response.Result));
                categoryTypeVM.CategoryType = model;
            }

            response = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                categoryTypeVM.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CategoryName,
                        Value = i.Id.ToString()
                    });
                return View(categoryTypeVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoryType(CategoryTypeDeleteVM model)
        {

            var response = await _categoryTypeService.DeleteAsync<APIResponse>(model.CategoryType.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexCategoryType));
            }

            return View(model);
        }

    }
}
