using AutoMapper;
using ECM_ExcellentAPI.Data;
using ECM_ExcellentWeb.Model.Dto;
using ECM_ExcellentWeb.Models;
using ECM_ExcellentWeb.Models.Dto;
using ECM_ExcellentWeb.Models.VM;
using ECM_ExcellentWeb.Service;
using ECM_ExcellentWeb.Service.IService;
using ECM_Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Reflection;

namespace ECM_ExcellentWeb.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IPurchaseOrderDetailService _purchaseOrderDetailService;
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;
        private readonly ICompanyService _companyService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public PurchaseOrderController(IPurchaseOrderService dbPurchaseOrder, IPurchaseOrderDetailService dbPurchaseOrderDetailService, IProductService dbProduct, ICompanyService dbCompany, ISupplierService dbSupplier, IAuthService dbAuth, IMapper mapper)
        {
            _purchaseOrderService = dbPurchaseOrder;
            _purchaseOrderDetailService = dbPurchaseOrderDetailService;
            _productService = dbProduct;
            _supplierService = dbSupplier;
            _mapper = mapper;
            _companyService = dbCompany;
            _authService = dbAuth;
        }
        public async Task<IActionResult> IndexPurchaseOrder()
        {
            List<PurchaseOrderDTO> list = new();
            var response = await _purchaseOrderService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<PurchaseOrderDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreatePurchaseOrder()
        {
            PurchaseOrderCreateVM purchaseOrderVM = new();
            //var response = await _companyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            //if (response != null && response.IsSuccess)
            //{
            //    purchaseOrderVM.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
            //        (Convert.ToString(response.Result)).Select(i => new SelectListItem
            //        {
            //            Text = i.CName,
            //            Value = i.Id.ToString()
            //        });
            //}

            var resp = await _productService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                purchaseOrderVM.ProductList = JsonConvert.DeserializeObject<List<ProductDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.PName,
                        Value = i.Id.ToString()
                    }); ;
            }
            
            var res = await _supplierService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.IsSuccess)
            {
                purchaseOrderVM.SupplierList = JsonConvert.DeserializeObject<List<SupplierDTO>>
                    (Convert.ToString(res.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Supplier_Name,
                        Value = i.Id.ToString()
                    }); 
            }

            return View(purchaseOrderVM);
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

        [HttpPost]
        public async Task<JsonResult> SavePurchased_Products(PurchaseOrderCreateVM model, int sid, string podate, string Purchae_Invoice_No, int company_id)
        {
            
            return Json(model);
        }


        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePurchaseOrder(PurchaseOrderCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _purchaseOrderService.CreateAsync<APIResponse>(model.PurchaseOrder, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexPurchaseOrder));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }


            var resp = await _purchaseOrderService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
                        Value = i.Id.ToString()
                    }); ;

                model.UserList = JsonConvert.DeserializeObject<List<UserDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.UserName,
                        Value = i.Id.ToString()
                    }); ;

                model.ProductList = JsonConvert.DeserializeObject<List<ProductDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.PName,
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
        public async Task<IActionResult> UpdatePurchaseOrder(int purchaseOrderId)
        {
            PurchaseOrderUpdateVM purchaseOrderVM = new();
            var response = await _purchaseOrderService.GetAsync<APIResponse>(purchaseOrderId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                PurchaseOrderDTO model = JsonConvert.DeserializeObject<PurchaseOrderDTO>(Convert.ToString(response.Result));
                purchaseOrderVM.PurchaseOrder = _mapper.Map<PurchaseOrderUpdateDTO>(model);
            }
            if (response != null && response.IsSuccess)
            {
                response = await _companyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    purchaseOrderVM.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.CName,
                            Value = i.Id.ToString()
                        });
                }

                response = await _productService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    purchaseOrderVM.ProductList = JsonConvert.DeserializeObject<List<ProductDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.PName,
                            Value = i.Id.ToString()
                        });
                }

                response = await _supplierService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    purchaseOrderVM.SupplierList = JsonConvert.DeserializeObject<List<SupplierDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.Supplier_Name,
                            Value = i.Id.ToString()
                        });

                }

                return View(purchaseOrderVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePurchaseOrder(PurchaseOrderUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _purchaseOrderService.UpdateAsync<APIResponse>(model.PurchaseOrder, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexPurchaseOrder));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _purchaseOrderService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
                        Value = i.Id.ToString()
                    });
                model.ProductList = JsonConvert.DeserializeObject<List<ProductDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.PName,
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
        public async Task<IActionResult> DeletePurchaseOrder(int purchaseOrderId)
        {
            PurchaseOrderDeleteVM purchaseOrderVM = new();
            var response = await _purchaseOrderService.GetAsync<APIResponse>(purchaseOrderId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                PurchaseOrderDTO model = JsonConvert.DeserializeObject<PurchaseOrderDTO>(Convert.ToString(response.Result));
                purchaseOrderVM.PurchaseOrder = model;
            }

            response = await _companyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                purchaseOrderVM.CompanyList = JsonConvert.DeserializeObject<List<CompanyDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CName,
                        Value = i.Id.ToString()
                    });
                return View(purchaseOrderVM);
            }

            response = await _productService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                purchaseOrderVM.ProductList = JsonConvert.DeserializeObject<List<ProductDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.PName,
                        Value = i.Id.ToString()
                    });
                return View(purchaseOrderVM);
            }

            response = await _supplierService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                purchaseOrderVM.SupplierList = JsonConvert.DeserializeObject<List<SupplierDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Supplier_Name,
                        Value = i.Id.ToString()
                    });
                return View(purchaseOrderVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePurchaseOrder(PurchaseOrderDeleteVM model)
        {

            var response = await _purchaseOrderService.DeleteAsync<APIResponse>(model.PurchaseOrder.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexPurchaseOrder));
            }

            return View(model);
        }
    }



}


