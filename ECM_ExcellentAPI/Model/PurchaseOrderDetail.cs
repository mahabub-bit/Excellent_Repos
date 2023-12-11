using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECM_ExcellentAPI.Model
{
    public class PurchaseOrderDetail
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("PurchaseId")]
        public int PurchaseId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        [Required]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int PodQty { get; set; }
        public float PodUnitPrice { get; set; }
        public float PodTotalPrice { get; set; }
        public float PodDiscount { get; set; }
        public float PodTaxableValue { get; set; }
        public float SGst { get; set; }
        public float CGst { get; set; }
        public float PodItemAmount { get; set; }
        public DateTime PodMfgDate { get; set; }
        public string PurchaseInvoiceNo { get; set; }
        public string PodAddInfo1 { get; set; }
        public int PodAddInfo2 { get; set; }
        public DateTime PodAddInfo3 { get; set; }
        public float PodAddInfo4 { get; set; }
        public string PodAddInfo5 { get; set; }
        public string PodAddInfo6 { get; set; }
        public string PodAddInfo7 { get; set; }
        public string PodAddInfo8 { get; set; }
    }
}
