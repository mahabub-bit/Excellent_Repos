using ECM_ExcellentWeb.Model.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECM_ExcellentWeb.Models.VM
{
    public class CustomerAddressUpdateVM
    {
        public CustomerAddressUpdateVM()
        {
            CustomerAddress = new CustomerAddressUpdateDTO();
        }

        public CustomerAddressUpdateDTO CustomerAddress { get; set; }
        [ValidateNever] 
        public IEnumerable<SelectListItem> CustomerList { get; set; }
    }
}
