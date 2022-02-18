using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class CategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
