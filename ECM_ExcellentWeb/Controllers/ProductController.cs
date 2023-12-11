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
    public class ProductTemp
    {
        public int Id { get; set; }
        public string PCode { get; set; }
        public string PName { get; set; }
        public string PDesc { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> CategoryTypeId { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public string QtyPerUnit { get; set; }
        public string PackageSize { get; set; }
        public Nullable<decimal> RetailerPrice { get; set; }
        public Nullable<decimal> MRPPrice { get; set; }
        public Nullable<decimal> Gst { get; set; }
        public string GstSlab { get; set; }
        public Nullable<bool> Discontinued { get; set; }
        public Nullable<double> CostPrice { get; set; }
        public Nullable<System.DateTime> PDate { get; set; }
        public string PAddColumn1 { get; set; }
        public Nullable<int> PAddColumn2 { get; set; }
        public byte[] PImage { get; set; }
        public string PAddColumn3 { get; set; }
        public Nullable<int> PAddColumn4 { get; set; }

    }

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICompanyService _companyService;
        private readonly ISupplierService _supplierService;
        private readonly ICategoryTypeService _categoryTypeService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(IProductService dbProduct, ICompanyService dbCompany, ISupplierService dbSupplier,
            ICategoryTypeService categoryTypeService, IMapper mapper, ICategoryService categoryService)
        {
            _productService = dbProduct;
            _companyService = dbCompany;
            _supplierService = dbSupplier;
            _categoryTypeService = categoryTypeService;
            _mapper = mapper;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> IndexProduct()
        {
            List<ProductDTO> list = new();
            var response = await _productService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateProduct()
        {
            ProductCreateVM productVM = new();
            var response = await _companyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                productVM.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
                        Value = i.Id.ToString()
                    }); ;
            }

            var respo = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (respo != null && respo.IsSuccess)
            {
                productVM.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
                    (Convert.ToString(respo.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CategoryName,
                        Value = i.Id.ToString()
                    }); ;
            }

            var resp = await _categoryTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                productVM.CategoryTypeList = JsonConvert.DeserializeObject<List<CategoryTypeDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CatTypeName,
                        Value = i.Id.ToString()
                    }); ;
            }

            var res = await _supplierService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.IsSuccess)
            {
                productVM.SupplierList = JsonConvert.DeserializeObject<List<SupplierDTO>>
                    (Convert.ToString(res.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Supplier_Name,
                        Value = i.Id.ToString()
                    }); ;
            }

            return View(productVM);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateAsync<APIResponse>(model.Product, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexProduct));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _productService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
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

                model.SupplierList = JsonConvert.DeserializeObject<List<SupplierDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Supplier_Name,
                        Value = i.Id.ToString()
                    }); ;
            }


            return View(model);
        }


        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateProduct(int productId)
        {
            ProductUpdateVM productVM = new();
            var response = await _productService.GetAsync<APIResponse>(productId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ProductDTO model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                productVM.Product = _mapper.Map<ProductUpdateDTO>(model);
            }
            if (response != null && response.IsSuccess)
            {
                response = await _companyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    productVM.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.CName,
                            Value = i.Id.ToString()
                        });
                }

                response = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    productVM.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.CategoryName,
                            Value = i.Id.ToString()
                        });
                }

                response = await _categoryTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    productVM.CategoryTypeList = JsonConvert.DeserializeObject<List<CategoryTypeDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.CatTypeName,
                            Value = i.Id.ToString()
                        });
                }

                response = await _supplierService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    productVM.SupplierList = JsonConvert.DeserializeObject<List<SupplierDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.Supplier_Name,
                            Value = i.Id.ToString()
                        });

                }
                return View(productVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(ProductUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateAsync<APIResponse>(model.Product, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexProduct));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _productService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
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
                model.SupplierList = JsonConvert.DeserializeObject<List<SupplierDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Supplier_Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            ProductDeleteVM productVM = new();
            var response = await _productService.GetAsync<APIResponse>(productId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ProductDTO model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                productVM.Product = model;
            }

            response = await _companyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                productVM.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
                        Value = i.Id.ToString()
                    });
                return View(productVM);
            }

            response = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                productVM.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CategoryName,
                        Value = i.Id.ToString()
                    });
                return View(productVM);
            }

            response = await _categoryTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                productVM.CategoryTypeList = JsonConvert.DeserializeObject<List<CategoryTypeDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CatTypeName,
                        Value = i.Id.ToString()
                    });
                return View(productVM);
            }

            response = await _supplierService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                productVM.SupplierList = JsonConvert.DeserializeObject<List<SupplierDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Supplier_Name,
                        Value = i.Id.ToString()
                    });
                return View(productVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(ProductDeleteVM model)
        {

            var response = await _productService.DeleteAsync<APIResponse>(model.Product.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexProduct));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> Get_ProductDetails(int productid)
        {
            ProductDeleteVM productVM = new();
            var response = await _productService.GetAsync<APIResponse>(productid, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ProductDTO model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                productVM.Product = model;
            }
            var productobj = JsonConvert.SerializeObject(productVM);


            return Json(productobj);

        }


        //public async Task<JsonResult> GetProductsBySupplerID(string supplierid)
        //{
        //    int statId;
        //    List<SelectListItem> product_ids = new List<SelectListItem>();
        //    List<ProductDTO> products = new List<ProductDTO>();
        //    products.Clear();
        //    product_ids.Clear();
        //    if (!string.IsNullOrEmpty(supplierid))
        //    {
        //        statId = Convert.ToInt32(supplierid);

        //        var response = _productService.GetAsync<APIResponse>(statId, HttpContext.Session.GetString(SD.SessionToken));

        //        products.ForEach(x =>
        //        {
        //            product_ids.Add(new SelectListItem { Text = x.PName, Value = x.Id.ToString() });
        //        });
        //    }
        //    return Json(product_ids);
        //}


    }
}
