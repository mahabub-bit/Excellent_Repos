using ECM_ExcellentWeb.Model.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECM_ExcellentWeb.Models.VM
{
    public class ProductDeleteVM
    {
        public ProductDeleteVM()
        {
            Product = new ProductDTO();
        }

        public ProductDTO Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> CategoryTypeList { get; set; }
        public IEnumerable<SelectListItem> SupplierList { get; set; }
    }
}
