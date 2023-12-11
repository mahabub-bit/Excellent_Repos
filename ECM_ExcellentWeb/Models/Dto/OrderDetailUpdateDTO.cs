using System.ComponentModel.DataAnnotations;

namespace ECM_ExcellentWeb.Model.Dto
{
    public class OrderDetailUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OrderItemAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal CGst { get; set; }
        public decimal SGst { get; set; }
        public decimal TaxableValue { get; set; }
        [Required]
        public int OrderDeatailStatusId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public string OrderDetailDesc1 { get; set; }
        public string OrderDetailDesc2 { get; set; }
        public string OrderDetailDesc3 { get; set; }
    }
}
