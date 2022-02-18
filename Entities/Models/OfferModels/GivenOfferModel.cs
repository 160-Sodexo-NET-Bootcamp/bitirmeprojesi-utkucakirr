namespace Entities.Models
{
    public class GivenOfferModel
    {
        public int Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual ProductViewModel Product { get; set; }
        public double OfferedPrice { get; set; }
        public virtual UserModel Owner { get; set; }
        public string Status { get; set; }
    }
}
