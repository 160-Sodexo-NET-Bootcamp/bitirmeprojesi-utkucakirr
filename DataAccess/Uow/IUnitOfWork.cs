using DataAccess.Repositories.Abstract;
using Entities.DataModel;

namespace DataAccess.Uow
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IOfferRepository OfferRepository { get; }
        IGenericRepository<Brand> BrandRepository { get; }
        IGenericRepository<Color> ColorRepository { get; }
        IGenericRepository<Mail> MailRepository { get; }
        IGenericRepository<Status> StatusRepository { get; }


        int Complete();
    }
}
