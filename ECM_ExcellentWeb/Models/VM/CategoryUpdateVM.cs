using ECM_ExcellentWeb.Model.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECM_ExcellentWeb.Models.VM
{
    public class CategoryUpdateVM
    {
        public CategoryUpdateVM()
        {
            Category = new CategoryUpdateDTO();
        }

        public CategoryUpdateDTO Category { get; set; }
        [ValidateNever] 
        public IEnumerable<SelectListItem> CompanyList { get; set; }
    }
}
