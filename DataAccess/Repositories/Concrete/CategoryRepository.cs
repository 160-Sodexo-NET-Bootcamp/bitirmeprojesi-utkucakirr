using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using Entities.DataModel;

namespace DataAccess.Repositories.Concrete
{
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
