using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModel
{
    public class BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
