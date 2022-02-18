namespace Entities.DataModel
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? ColorId { get; set; }
        public int? BrandId { get; set; }
        public int Status { get; set; }
        
        public bool? IsOfferable { get; set; }
        public bool? IsSold { get; set; } = false;
        public string ImageUrl { get; set; }

        public virtual Category Category { get; set; }
        public virtual int CategoryId { get; set; }

        public virtual User User { get; set; }
        public virtual int UserId { get; set; }
    }
}
