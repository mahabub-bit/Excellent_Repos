using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ECM_ExcellentWeb.Models.Dto;

namespace ECM_ExcellentWeb.Model.Dto
{
    public class OrderDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public CustomerDTO Customer { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public int OrderStatusId { get; set; }
        public OrderStatusDTO OrderStatus { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public CompanyDTO Company { get; set; }
        public decimal OrderAmount { get; set; }
        [Required]
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public DateTime ShipDate { get; set; }
        public decimal ShipAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public decimal Taxes { get; set; }
        [Required]
        public int ShipAddressId { get; set; }
        public CustomerAddressDTO CustomerAddress { get; set; }
        public DateTime OrderCloseDate { get; set; }
        public string OrderDesc1 { get; set; }
        public string OrderDesc2 { get; set; }
        public string OrderDesc3 { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
