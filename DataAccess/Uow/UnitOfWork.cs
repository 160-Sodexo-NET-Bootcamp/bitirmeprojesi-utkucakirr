using AutoMapper;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Entities.DataModel;

namespace DataAccess.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        public IUserRepository UserRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IOfferRepository OfferRepository { get; }
        public IGenericRepository<Brand> BrandRepository { get; }
        public IGenericRepository<Color> ColorRepository { get; }
        public IGenericRepository<Mail> MailRepository { get; }
        public IGenericRepository<Status> StatusRepository { get; }



        public UnitOfWork(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            UserRepository = new UserRepository(_context);
            ProductRepository = new ProductRepository(_context,_mapper);
            CategoryRepository = new CategoryRepository(_context);
            OfferRepository = new OfferRepository(_context,_mapper);
            BrandRepository = new GenericRepository<Brand>(_context);
            ColorRepository = new GenericRepository<Color>(_context);
            MailRepository = new GenericRepository<Mail>(_context);
            StatusRepository = new GenericRepository<Status>(_context);
        }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}