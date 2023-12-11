using ECM_ExcellentWeb.Model.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECM_ExcellentWeb.Models.VM
{
    public class CustomerAddressCreateVM
    {
        public CustomerAddressCreateVM()
        {
            CustomerAddress = new CustomerAddressCreateDTO();
        }

        public CustomerAddressCreateDTO CustomerAddress { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CustomerList { get; set; }
    }
}
