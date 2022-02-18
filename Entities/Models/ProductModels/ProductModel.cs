using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class ProductModel
    {

        [Required]
        [StringLength(100, ErrorMessage = "{0} field max {1} chars")]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "{0} field max {1} chars")]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Required]
        public int Status { get; set; }
        public int? ColorId { get; set; }
        public int? BrandId { get; set; }
        public bool? IsOfferable { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
    }
}
