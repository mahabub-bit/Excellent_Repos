using System.ComponentModel.DataAnnotations;

namespace ECM_ExcellentWeb.Model.Dto
{
    public class CategoryUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryImage { get; set; }
    }
}
