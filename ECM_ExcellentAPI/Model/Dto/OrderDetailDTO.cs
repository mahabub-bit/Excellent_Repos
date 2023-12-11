using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECM_ExcellentAPI.Model.Dto
{
    public class OrderDetailDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        public OrderDTO Order { get; set; }
        [Required]
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OrderItemAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal CGst { get; set; }
        public decimal SGst { get; set; }
        public decimal TaxableValue { get; set; }
        [Required]
        public int OrderDeatailStatusId { get; set; }
        public OrderStatusDTO OrderStatus { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public CompanyDTO Company { get; set; }
        public string OrderDetailDesc1 { get; set; }
        public string OrderDetailDesc2 { get; set; }
        public string OrderDetailDesc3 { get; set; }
    }
}
