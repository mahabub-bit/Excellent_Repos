using ECM_ExcellentWeb.Model.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECM_ExcellentWeb.Models.VM
{
    public class PurchaseOrderUpdateVM
    {
        public PurchaseOrderUpdateVM()
        {
            PurchaseOrder = new PurchaseOrderUpdateDTO();
        }

        public PurchaseOrderUpdateDTO PurchaseOrder { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }
        public IEnumerable<SelectListItem> SupplierList { get; set; }
    }
}
