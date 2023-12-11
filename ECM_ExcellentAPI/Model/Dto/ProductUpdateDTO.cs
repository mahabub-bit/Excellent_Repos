using System.ComponentModel.DataAnnotations;

namespace ECM_ExcellentAPI.Model.Dto
{
    public class ProductUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string PCode { get; set; }
        [Required]
        public string PName { get; set; }
        public string PDesc { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int CategoryTypeId { get; set; }
        [Required]
        public int SupplierId { get; set; }
        public string QtyPerUnit { get; set; }
        public string PackageSize { get; set; }
        public decimal RetailerPrice { get; set; }
        public decimal MRPPrice { get; set; }
        public decimal Gst { get; set; }
        public string GstSlab { get; set; }
        public Boolean Discontinued { get; set; }
        public float CostPrice { get; set; }
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
