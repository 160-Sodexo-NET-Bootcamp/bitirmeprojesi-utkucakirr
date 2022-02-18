using AutoMapper;
using Core.Filters;
using Core.Result;
using DataAccess.Uow;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OfferController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Give an offer.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [TypeFilter(typeof(OfferAddFilter))]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GiveOffer(OfferModel entity)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(entity.ProductId);
            if(product is null)
            {
                return Ok(new ResultModel(false, "Product id is invalid. Try again."));
            }
            var offer = await _unitOfWork.ProductRepository.OfferCheck(entity);
            if(offer is null)
            {
                return Ok(new ResultModel(false, "This product is not offerable."));
            }
            await _unitOfWork.OfferRepository.AddAsync(offer);
            _unitOfWork.Complete();
            return Ok(new ResultModel(true, "Offer created!"));
        }

        /// <summary>
        /// Give an offer using the product's price.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [TypeFilter(typeof(OfferAddFilterP))]
        [Authorize]
        [HttpPost("PercentageOffer")]
        public async Task<IActionResult> GivePercentageOffer(OfferPercentageModel entity)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(entity.ProductId);
            if (product is null)
            {
                return Ok(new ResultModel(false, "Product id is invalid. Try again."));
            }
            var model = await _unitOfWork.ProductRepository.MakeChange(entity);
            var offer = await _unitOfWork.ProductRepository.OfferCheck(model);
            if(offer is null)
            {
                return Ok(new ResultModel(false, "This product is not offerable."));
            }
            await _unitOfWork.OfferRepository.AddAsync(offer);
            _unitOfWork.Complete();
            return Ok(new ResultModel(true, "Offer created!"));
        }

        /// <summary>
        /// Buy the product directly without offering.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("BuyProduct")]
        public async Task<IActionResult> BuyProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product is null)
            {
                return Ok(new ResultModel(false, "Product id is invalid. Try again."));
            }
            var check = await _unitOfWork.ProductRepository.SellCheck(id);
            if(!check)
            {
                return Ok(new ResultModel(false, "This product already sold!"));
            }
            await _unitOfWork.ProductRepository.SellProduct(id);
            _unitOfWork.Complete();
            return Ok(new ResultModel(true, "Product bought succesfully."));
        }
    }
}
