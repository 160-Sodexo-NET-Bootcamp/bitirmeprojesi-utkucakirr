namespace Entities.DataModel
{
    public class Offer : BaseModel
    {
        public virtual int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public virtual int OwnerId { get; set; }
        public virtual User Owner { get; set; }

        public virtual int OffererId { get; set; }
        public virtual User Offerer { get; set; }

        public double OfferedPrice { get; set; }
        public string Status { get; set; } = "Waiting response.";
    }
}
