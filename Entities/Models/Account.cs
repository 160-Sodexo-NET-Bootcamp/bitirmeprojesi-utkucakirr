using System.Collections.Generic;

namespace Entities.Models
{
    public class Account
    {
        public int UserId { get; set; }
        public List<GivenOfferModel> MyOffers { get; set; }
    }
}
