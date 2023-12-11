using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECM_ExcellentAPI.Model
{
    public class Product_Rate_History
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime DateTime { get; set; }
        public double Daily_Product_Price { get; set; }
        [Required]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        [ForeignKey("CategoryType")]
        public int CategoryTypeId { get; set; }
        public CategoryType CategoryType { get; set; }
        public string PRH_AddColunm { get; set; }
        public int PRH_AddColunm2 { get; set; }
        public decimal PRH_AddColunm3 { get; set; }
        public string PRH_AddColunm4 { get; set; }
    }
}
