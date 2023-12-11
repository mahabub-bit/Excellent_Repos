using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECM_ExcellentAPI.Model
{
    public class PurchaseOrder
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime PO_Date { get; set; }
        [Required]
        [ForeignKey("SupplierId")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        [Required]
        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal Rate { get; set; }
        public float Quantity { get; set; }
        public string PO_Status { get; set; }
        public decimal PO_TotalPrice { get; set; }
        public int PO_Invoice { get; set; }
        public decimal ShippingFee { get; set; }
        public float Taxes { get; set; }
        public string Note { get; set; }
        public DateTime PaymentDate { get; set; }
        public int PaymentType { get; set; }
        public int ClosedBy { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime ExpectedDate { get; set; }
        public DateTime ClosedDate { get; set; }
        [Required]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string AddPurchaseColumn { get; set; }
        public string AddPurchaseColumn2 { get; set; }
        public string AddPurchaseColumn3 { get; set; }
    }
}
