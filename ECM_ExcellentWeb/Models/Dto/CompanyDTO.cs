using System.ComponentModel.DataAnnotations;

namespace ECM_ExcellentWeb.Model.Dto
{
    public class CompanyDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CName { get; set; }
        public string CAddress { get; set; }
        public string CCity { get; set; }
        public string CState { get; set; }
        public string CPostalCode { get; set; }
        public string CBusinessPhone { get; set; }
        public string CGST { get; set; }
        public string CBank { get; set; }
        public string CBankAccount { get; set; }
        public string CBankBranch { get; set; }
        public string CIFSC { get; set; }
        public string CC1 { get; set; }
        public DateTime? CC2 { get; set; }
        public int? CC3 { get; set; }
        public string CC4 { get; set; }
    }
}
