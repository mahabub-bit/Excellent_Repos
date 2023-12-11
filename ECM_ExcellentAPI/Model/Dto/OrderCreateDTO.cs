using System.ComponentModel.DataAnnotations;

namespace ECM_ExcellentAPI.Model.Dto
{
    public class OrderCreateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public int OrderStatusId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public decimal OrderAmount { get; set; }
        [Required]
        public int UserId { get; set; }
        public DateTime ShipDate { get; set; }
        public decimal ShipAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public decimal Taxes { get; set; }
        [Required]
        public int ShipAddressId { get; set; }
        public DateTime OrderCloseDate { get; set; }
        public string OrderDesc1 { get; set; }
        public string OrderDesc2 { get; set; }
        public string OrderDesc3 { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
