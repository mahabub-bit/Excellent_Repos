using ECM_ExcellentWeb.Model.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECM_ExcellentWeb.Models.VM
{
    public class PurchaseOrderDetailCreateVM
    {
        public PurchaseOrderDetailCreateVM()
        {
            PurchaseOrderDetail = new PurchaseOrderDetailCreateDTO();
        }

        public PurchaseOrderDetailCreateDTO PurchaseOrderDetail { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> PurchaseOrderList { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}
