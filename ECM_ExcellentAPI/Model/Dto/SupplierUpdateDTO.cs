using System.ComponentModel.DataAnnotations;

namespace ECM_ExcellentAPI.Model.Dto
{
    public class SupplierUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string Supplier_Company_Name { get; set; }
        [Required]
        public string Supplier_Name { get; set; }

        public string Supplier_JobTitle { get; set; }

        public string Supplier_Address { get; set; }
        public string Supplier_Pan { get; set; }
        public string Supplier_Contact { get; set; }
        public string Supplier_Email { get; set; }
        public string Supplier_Webpage { get; set; }
        public string Supplier_Business_Phone { get; set; }
        public string Supplier_Fax { get; set; }
        public string Supplier_Home_Phone { get; set; }
        public string Supplier_Moblie_Phone { get; set; }
        public string Supplier_City { get; set; }
        public string Supplier_State { get; set; }
        public string Supplier_ZipCode { get; set; }
        public string Supplier_Region { get; set; }
        public string Supplier_Note { get; set; }
        public string Supplier_D { get; set; }
        
    }
}
