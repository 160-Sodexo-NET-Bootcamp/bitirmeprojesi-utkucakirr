using Entities.DataModel;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IOfferRepository:IGenericRepository<Offer>
    {
        List<GivenOfferModel> GetMyOffers(int id);
        List<TakenOfferModel> GetTakenOffers(int id);
        Task<bool> OfferCheck(int UserId, int OfferId,string status);
        Task<bool> OfferWithdraw(int UserId, int OfferId);
    }
}
