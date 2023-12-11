using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ECM_ExcellentAPI.Model
{
    public class Product
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public string PCode { get; set; }
        [Required]
        public string PName { get; set; }
        public string PDesc { get; set; }

        [Required]
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [Required]
        public int CategoryTypeId { get; set; }
        [ForeignKey("CategoryTypeId")]
        [ValidateNever]
        public CategoryType CategoryType { get; set; }

        [Required]
        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        [ValidateNever]
        public Supplier Supplier { get; set; }

        public string QtyPerUnit { get; set; }
        public string PackageSize { get; set; }
        public double RetailerPrice { get; set; }
        public double MRPPrice { get; set; }
        public double Gst { get; set; }
        public string GstSlab { get; set; }
        public Boolean Discontinued { get; set; }
        public double CostPrice { get; set; }
        public DateTime PDate { get; set; }
        public string PAddColumn1 { get; set; }
        public string PAddColumn2 { get; set; }
        public string PImage { get; set; }
        public string PAddColumn3 { get; set; }
        public string PAddColumn4 { get; set; }
        public string PAddColumn5 { get; set; }
        public string PAddColumn6 { get; set; }
    }
}
