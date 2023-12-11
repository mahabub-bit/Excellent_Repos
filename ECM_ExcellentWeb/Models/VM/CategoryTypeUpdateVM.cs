using ECM_ExcellentWeb.Model.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECM_ExcellentWeb.Models.VM
{
    public class CategoryTypeUpdateVM
    {
        public CategoryTypeUpdateVM()
        {
            CategoryType = new CategoryTypeUpdateDTO();
        }

        public CategoryTypeUpdateDTO CategoryType { get; set; }
        [ValidateNever] 
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
