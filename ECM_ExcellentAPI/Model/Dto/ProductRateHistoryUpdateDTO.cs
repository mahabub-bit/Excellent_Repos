using System.ComponentModel.DataAnnotations;

namespace ECM_ExcellentAPI.Model.Dto
{
    public class ProductRateHistoryUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        public DateTime DateTime { get; set; }
        public double Daily_Product_Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int CategoryTypeId { get; set; }
        public string PRH_AddColunm { get; set; }
        public int PRH_AddColunm2 { get; set; }
        public decimal PRH_AddColunm3 { get; set; }
        public string PRH_AddColunm4 { get; set; }
    }
}
