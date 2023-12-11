using ECM_ExcellentWeb.Model.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECM_ExcellentWeb.Models.VM
{
    public class PurchaseOrderDetailUpdateVM
    {
        public PurchaseOrderDetailUpdateVM()
        {
            PurchaseOrderDetail = new PurchaseOrderDetailUpdateDTO();
        }

        public PurchaseOrderDetailUpdateDTO PurchaseOrderDetail { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> PurchaseOrderList { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}
