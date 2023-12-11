using System.ComponentModel.DataAnnotations;

namespace ECM_ExcellentWeb.Model.Dto
{
    public class CustomerUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerLandLine { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerFax { get; set; }
        public string CustomerCompany { get; set; }
        public string CustomerJobTitle { get; set; }
        public string CustomerHomeNo { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public int CustomerZip { get; set; }
        public string CustomerPan { get; set; }
        public string CustomerAccNo { get; set; }
        public string CustomerBank { get; set; }
        public string CustomerBankBranch { get; set; }
        public string CustomerBankDetails { get; set; }
        public string CustomerGSTIN { get; set; }
        public string CustomerDetails1 { get; set; }
        public string CustomerDetails2 { get; set; }
        public string CustomerDetails3 { get; set; }
        public string CustomerDetails4 { get; set; }
    }
}
