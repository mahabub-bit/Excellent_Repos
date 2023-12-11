using ECM_ExcellentWeb.Model.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECM_ExcellentWeb.Models.VM
{
    public class SupplierUpdateVM
    {
        public SupplierUpdateVM()
        {
            Supplier = new SupplierUpdateDTO();
        }

        public SupplierUpdateDTO Supplier { get; set; }
        [ValidateNever] 
        public IEnumerable<SelectListItem> CompanyList { get; set; }
    }
}
