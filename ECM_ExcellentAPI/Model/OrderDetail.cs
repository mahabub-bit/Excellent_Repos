using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECM_ExcellentAPI.Model
{
    public class OrderDetail
    {
        [Required]
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Required]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OrderItemAmount { get; set; }
        public decimal Discount { get; set;}
        public decimal CGst { get; set;}
        public decimal SGst { get; set;}
        public decimal TaxableValue { get; set;}
        [Required]
        [ForeignKey("OrderDetailStatusId")]
        public int OrderDeatailStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        [Required]
        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string OrderDetailDesc1 { get; set; }
        public string OrderDetailDesc2 { get; set; }
        public string OrderDetailDesc3 { get; set; }
    }
}
