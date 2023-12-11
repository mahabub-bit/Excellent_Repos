using ECM_ExcellentWeb.Model.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECM_ExcellentWeb.Models.VM
{
    public class SupplierCreateVM
    {
        public SupplierCreateVM()
        {
            Supplier = new SupplierCreateDTO();
        }

        public SupplierCreateDTO Supplier { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
    }
}
