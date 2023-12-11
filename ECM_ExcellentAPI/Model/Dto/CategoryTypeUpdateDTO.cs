using System.ComponentModel.DataAnnotations;

namespace ECM_ExcellentAPI.Model.Dto
{
    public class CategoryTypeUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string CatTypeName { get; set; }
        public string CatTypeDesc2 { get; set; }
        public string CatTypeDesc3 { get; set; }
        public string CatTypeDesc4 { get; set; }
    }
}
