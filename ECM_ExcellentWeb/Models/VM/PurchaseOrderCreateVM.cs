using ECM_ExcellentWeb.Model.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECM_ExcellentWeb.Models.VM
{
    public class PurchaseOrderCreateVM
    {
        public PurchaseOrderCreateVM()
        {
            Company = new CompanyDTO();
            PurchaseOrder = new PurchaseOrderCreateDTO();
            PurchaseOrderDetail = new PurchaseOrderDetailCreateDTO();
            ProductDetails = new ProductDTO();
        }

        public CompanyDTO Company { get; set; }
        public ProductDTO ProductDetails { get; set; }
        [ValidateNever]
        public PurchaseOrderCreateDTO PurchaseOrder { get; set; }
        [ValidateNever]
        public PurchaseOrderDetailCreateDTO PurchaseOrderDetail { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }
        public IEnumerable<SelectListItem> SupplierList { get; set; }
        public IEnumerable<SelectListItem> PurchaseOrderDetailList { get; set; }
    }
}
