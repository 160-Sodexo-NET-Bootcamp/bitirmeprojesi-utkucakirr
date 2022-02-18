using AutoMapper;
using Entities.DataModel;
using Entities.Models;

namespace Core
{
    /// <summary>
    /// Needed mappings.
    /// </summary>
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductAddModel, Product>();
            CreateMap<ProductModel, ProductAddModel>();
            CreateMap<ProductUpdateModel, Product>();

            CreateMap<OfferAddModel, Offer>();
            CreateMap<OfferModel, OfferAddModel>();
            CreateMap<OfferModel, Offer>();

            CreateMap<OfferPercentageModel, OfferAddModelP>();
            CreateMap<OfferAddModelP, OfferAddModel>();

            CreateMap<Offer, GivenOfferModel>();
            CreateMap<Offer, TakenOfferModel>();

            CreateMap<ParameterAddModel, Color>();
            CreateMap<ParameterAddModel, Brand>();
            CreateMap<ParameterAddModel, Status>();

            CreateMap<CategoryViewModel, Category>();

            CreateMap<User, UserModel>();
        }
    }
}
