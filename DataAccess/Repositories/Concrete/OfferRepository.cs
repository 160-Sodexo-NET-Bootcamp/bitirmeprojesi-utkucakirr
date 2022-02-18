using AutoMapper;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using Entities.DataModel;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class OfferRepository : GenericRepository<Offer>, IOfferRepository
    {
        private readonly IMapper _mapper;
        public OfferRepository(ProjectDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Getting offers of user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<GivenOfferModel> GetMyOffers(int id)
        {
            var offerList = base.GetListByExpression(x => x.OffererId == id);
            var myOffers = _mapper.Map<List<Offer>, List<GivenOfferModel>>(offerList);
            return myOffers;
        }

        /// <summary>
        /// Getting given offers to a users products.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TakenOfferModel> GetTakenOffers(int id)
        {
            var offerList = base.GetListByExpression(x => x.OwnerId == id);
            var takenOffers = _mapper.Map<List<TakenOfferModel>>(offerList);
            return takenOffers;
        }

        /// <summary>
        /// Checking offer status for rejecting or accepting the offer process.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="OfferId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<bool> OfferCheck(int UserId, int OfferId,string status)
        {
            var offer = await base.GetByIdAsync(OfferId);
            if(offer.OwnerId!=UserId)
            {
                return false;
            }
            if(offer.Status.Equals("Waiting response."))
            {
                offer.Status = status;
                base.Update(offer);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checking offer status for withdrawing the offer.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="OfferId"></param>
        /// <returns></returns>
        public async Task<bool> OfferWithdraw(int UserId, int OfferId)
        {
            var offer = await base.GetByIdAsync(OfferId);
            if(offer.OffererId!=UserId)
            {
                return false;
            }
            if(offer.Status.Equals("Waiting response."))
            {
                offer.Status = "Offer withdrawed.";
                base.Update(offer);
                return true;
            }
            return false;
        }
    }
}
