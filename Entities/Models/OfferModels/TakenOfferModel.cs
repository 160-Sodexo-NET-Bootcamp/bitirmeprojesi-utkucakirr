namespace Entities.Models
{
    public class TakenOfferModel
    {
        public int Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual ProductViewModel Product { get; set; }
        public virtual int OffererId { get; set; }
        public virtual UserModel Offerer { get; set; }

        public double OfferedPrice { get; set; }
        public string Status { get; set; }
    }
}
