using AutoMapper;
using ECM_ExcellentWeb.Model.Dto;
using ECM_ExcellentWeb.Models;
using ECM_ExcellentWeb.Models.VM;
using ECM_ExcellentWeb.Service;
using ECM_ExcellentWeb.Service.IService;
using ECM_Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ECM_ExcellentWeb.Controllers
{
    public class ProductRateHistoryController : Controller
    {
        private readonly IProductRateHistoryService _productRateHistoryService;
        private readonly IProductService _productService;
        private readonly ICategoryTypeService _categoryTypeService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductRateHistoryController(IProductRateHistoryService dbProductRate, IProductService dbProduct,
            ICategoryTypeService categoryTypeService, IMapper mapper, ICategoryService categoryService)
        {
            _productRateHistoryService = dbProductRate;
            _productService = dbProduct;
            _categoryTypeService = categoryTypeService;
            _mapper = mapper;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> IndexProductRateHistory()
        {
            List<ProductRateHistoryDTO> list = new();
            var response = await _productRateHistoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductRateHistoryDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateProductRateHistory()
        {
            ProductRHCreateVM productRHVM = new();
            var response = await _productService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                productRHVM.ProductList = JsonConvert.DeserializeObject<List<ProductDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.PName,
                        Value = i.Id.ToString()
                    }); ;
            }

            var respo = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (respo != null && respo.IsSuccess)
            {
                productRHVM.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
                    (Convert.ToString(respo.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CategoryName,
                        Value = i.Id.ToString()
                    }); ;
            }

            var resp = await _categoryTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                productRHVM.CategoryTypeList = JsonConvert.DeserializeObject<List<CategoryTypeDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CatTypeName,
                        Value = i.Id.ToString()
                    }); ;
            }

            return View(productRHVM);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProductRateHistory(ProductRHCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productRateHistoryService.CreateAsync<APIResponse>(model.ProductRateHistory, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexProductRateHistory));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _productRateHistoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.ProductList = JsonConvert.DeserializeObject<List<ProductDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.PName,
                        Value = i.Id.ToString()
                    }); ;

                model.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CategoryName,
                        Value = i.Id.ToString()
                    }); ;

                model.CategoryTypeList = JsonConvert.DeserializeObject<List<CategoryTypeDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CatTypeName,
                        Value = i.Id.ToString()
                    }); ;

            }
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateProductRateHistory(int productRateHistoryId)
        {
            ProductRHUpdateVM productRHVM = new();
            var response = await _productRateHistoryService.GetAsync<APIResponse>(productRateHistoryId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ProductRateHistoryDTO model = JsonConvert.DeserializeObject<ProductRateHistoryDTO>(Convert.ToString(response.Result));
                productRHVM.ProductRateHistory = _mapper.Map<ProductRateHistoryUpdateDTO>(model);
            }

            if (response != null && response.IsSuccess)
            {
                response = await _productService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    productRHVM.ProductList = JsonConvert.DeserializeObject<List<ProductDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.PName,
                            Value = i.Id.ToString()
                        });
                }

                response = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    productRHVM.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.CategoryName,
                            Value = i.Id.ToString()
                        });
                }

                response = await _categoryTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    productRHVM.CategoryTypeList = JsonConvert.DeserializeObject<List<CategoryTypeDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.CatTypeName,
                            Value = i.Id.ToString()
                        });
                }
                return View(productRHVM);
            }
            return NotFound();
        }


        //[Authorize(Roles = "admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProductRateHistory(ProductRHUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productRateHistoryService.UpdateAsync<APIResponse>(model.ProductRateHistory, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexProductRateHistory));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _productRateHistoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.ProductList = JsonConvert.DeserializeObject<List<ProductDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.PName,
                        Value = i.Id.ToString()
                    });
                model.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CategoryName,
                        Value = i.Id.ToString()
                    });
                model.CategoryTypeList = JsonConvert.DeserializeObject<List<CategoryTypeDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CatTypeName,
                        Value = i.Id.ToString()
                    });
            }
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProductRateHistory(int productRateHistoryId)
        {
            ProductRHDeleteVM productRHVM = new();
            var response = await _productRateHistoryService.GetAsync<APIResponse>(productRateHistoryId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ProductRateHistoryDTO model = JsonConvert.DeserializeObject<ProductRateHistoryDTO>(Convert.ToString(response.Result));
                productRHVM.ProductRateHistory = model;
            }

            response = await _productService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                productRHVM.ProductList = JsonConvert.DeserializeObject<List<ProductDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.PName,
                        Value = i.Id.ToString()
                    });
                return View(productRHVM);
            }

            response = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                productRHVM.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CategoryName,
                        Value = i.Id.ToString()
                    });
                return View(productRHVM);
            }

            response = await _categoryTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                productRHVM.CategoryTypeList = JsonConvert.DeserializeObject<List<CategoryTypeDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CatTypeName,
                        Value = i.Id.ToString()
                    });
                return View(productRHVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductRateHistory(ProductRHDeleteVM model)
        {

            var response = await _productRateHistoryService.DeleteAsync<APIResponse>(model.ProductRateHistory.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexProductRateHistory));
            }

            return View(model);
        }
    }
}
