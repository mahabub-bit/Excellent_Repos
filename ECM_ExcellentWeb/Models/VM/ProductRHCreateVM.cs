using ECM_ExcellentWeb.Model.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECM_ExcellentWeb.Models.VM
{
    public class ProductRHCreateVM
    {
        public ProductRHCreateVM()
        {
            ProductRateHistory = new ProductRateHistoryCreateDTO();
        }

        public ProductRateHistoryCreateDTO ProductRateHistory { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> CategoryTypeList { get; set; }
    }
}
