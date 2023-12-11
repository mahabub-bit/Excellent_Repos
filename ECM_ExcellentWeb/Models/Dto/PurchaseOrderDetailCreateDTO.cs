using System.ComponentModel.DataAnnotations;

namespace ECM_ExcellentWeb.Model.Dto
{
    public class PurchaseOrderDetailCreateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PurchaseId { get; set; }
        [Required]
        public int ProductId { get; set; }
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
