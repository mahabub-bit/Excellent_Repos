using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECM_ExcellentAPI.Model
{
    public class Order
    {
        [Required]
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [ForeignKey("OrderStatusId")]
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        [Required]
        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public decimal OrderAmount { get; set; }
        
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime ShipDate { get; set; }
        public decimal ShipAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public decimal Taxes { get; set; }
        [Required]
        [ForeignKey("ShipAddressId")]
        public int ShipAddressId { get; set;}
        public CustomerAddress CustomerAddress { get; set; }
        public DateTime OrderCloseDate { get; set; }
        public string OrderDesc1 { get; set; }
        public string OrderDesc2 { get; set;}
        public string OrderDesc3 { get; set;}
        public decimal PaymentAmount { get; set; }

    }
}
