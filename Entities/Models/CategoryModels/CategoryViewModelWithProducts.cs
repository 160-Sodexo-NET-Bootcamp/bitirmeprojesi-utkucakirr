using System.Collections.Generic;

namespace Entities.Models
{
    public class CategoryViewModelWithProducts
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
