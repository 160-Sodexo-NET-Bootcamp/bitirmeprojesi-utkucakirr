using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class ParameterAddModel
    {
        [Required]
        public string Name { get; set; }
    }
}
