using AutoMapper;
using Core.Filters;
using Core.Result;
using DataAccess.Uow;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyAccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MyAccountController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get your given offers.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [TypeFilter(typeof(CheckIdFilter))]
        [Authorize]
        [HttpGet("GivenOffers")]
        public async Task<IActionResult> GetGivenOffers(int UserId)
        {
            var myOffers = _unitOfWork.OfferRepository.GetMyOffers(UserId);
            foreach(var item in myOffers)
            {
                item.Product = _mapper.Map<ProductViewModel>(await _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId));
                item.Owner = _mapper.Map<UserModel>(await _unitOfWork.UserRepository.GetByIdAsync(item.Product.UserId));
            }
            return Ok(new ResultModel<List<GivenOfferModel>>(true, "Your given offers:", myOffers));
        }

        /// <summary>
        /// Get given offers to your products.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [TypeFilter(typeof(CheckIdFilter))]
        [Authorize]
        [HttpGet("TakenOffers")]
        public async Task<IActionResult> GetTakenOffers(int UserId)
        {
            var takenOffers = _unitOfWork.OfferRepository.GetTakenOffers(UserId);
            foreach(var item in takenOffers)
            {
                item.Product = _mapper.Map<ProductViewModel>(await _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId));
                item.Offerer = _mapper.Map<UserModel>(await _unitOfWork.UserRepository.GetByIdAsync(item.OffererId));
            }
            return Ok(new ResultModel<List<TakenOfferModel>>(true, "Your taken offers:", takenOffers));
        }

        /// <summary>
        /// Confirm an offer that given to one of your products.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="OfferId"></param>
        /// <returns></returns>
        [TypeFilter(typeof(CheckIdFilter))]
        [Authorize]
        [HttpPost("ConfirmOffer")]
        public async Task<IActionResult> ConfirmOffer(int UserId,int OfferId)
        {
            var result = await _unitOfWork.OfferRepository.OfferCheck(UserId, OfferId, "Confirmed!");
            if (result)
            {
                var offer = await _unitOfWork.OfferRepository.GetByIdAsync(OfferId);
                await _unitOfWork.ProductRepository.OfferConfirmed(offer.ProductId);
                _unitOfWork.Complete();
                return Ok(new ResultModel(true, "Offer confirmed."));
            }
            return Ok(new ResultModel(false, "You can't make any changes on this offer. The product is not belong to you or you already give answer to this offer."));
        }

        /// <summary>
        /// Reject an offer that given to one of your products.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="OfferId"></param>
        /// <returns></returns>
        [TypeFilter(typeof(CheckIdFilter))]
        [Authorize]
        [HttpPost("RejectOffer")]
        public async Task<IActionResult> RejectOffer(int UserId, int OfferId)
        {
            var result = await _unitOfWork.OfferRepository.OfferCheck(UserId, OfferId, "Rejected!");
            if(result)
            {
                return Ok(new ResultModel(true, "Offer rejected."));
            }
            return Ok(new ResultModel(false, "You can't make any changes on this offer. The product is not belong to you or you already give answer to this offer."));
        }

        /// <summary>
        /// Withdraw an offer that given by you.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="OfferId"></param>
        /// <returns></returns>
        [TypeFilter(typeof(CheckIdFilter))]
        [Authorize]
        [HttpPost("WithdrawOffer")]
        public async Task<IActionResult> WithdrawOffer(int UserId, int OfferId)
        {
            var result = await _unitOfWork.OfferRepository.OfferWithdraw(UserId, OfferId);
            if(result)
            {
                return Ok(new ResultModel(true, "Offer withdrawed."));
            }
            return Ok(new ResultModel(false, "You can't make any changes on this offer. The offer is not belong to you or the offer already been answered."));
        }


    }
}
