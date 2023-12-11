using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace ECM_ExcellentWeb.Model.Dto
{
    public class PurchaseOrderUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        public DateTime PO_Date { get; set; }
        [Required]
        public int SupplierId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int UserId { get; set; }
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
        public int ProductId { get; set; }
        public string AddPurchaseColumn { get; set; }
        public string AddPurchaseColumn2 { get; set; }
        public string AddPurchaseColumn3 { get; set; }
    }
}
