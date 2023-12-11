using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECM_ExcellentWeb.Model.Dto
{
    public class CustomerAddressDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public string ShipHNo { get; set; }
        public string ShipStreet { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipCountry { get; set; }
        public string ShipZip { get; set; }
        public string ShipCol1 { get; set; }
        public string ShipCol2 { get; set; }
        public string ShipCol3 { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
