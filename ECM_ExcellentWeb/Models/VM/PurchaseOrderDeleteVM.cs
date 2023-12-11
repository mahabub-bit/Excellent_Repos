using ECM_ExcellentWeb.Model.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECM_ExcellentWeb.Models.VM
{
    public class PurchaseOrderDeleteVM
    {
        public PurchaseOrderDeleteVM()
        {
            PurchaseOrder = new PurchaseOrderDTO();
        }

        public PurchaseOrderDTO PurchaseOrder { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }
        public IEnumerable<SelectListItem> SupplierList { get; set; }
    }
}
