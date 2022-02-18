using Entities.DataModel;
using Entities.Models;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        CategoryViewModelWithProducts GetByCategoryId(Category category);
        Task<Offer> OfferCheck(OfferModel entity);
        Task<OfferModel> MakeChange(OfferPercentageModel entity);
        Task<bool> SellCheck(int id);
        Task SellProduct(int id);
        Task OfferConfirmed(int ProductId);
        Task<bool> UpdateWithConfirmation(int UserId, ProductUpdateModel entity);
        Task<bool> DeleteWithConfirmation(int UserId, int id);
    }
}
