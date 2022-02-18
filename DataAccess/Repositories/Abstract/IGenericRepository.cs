using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IGenericRepository<T> where T: class
    {
        Task AddAsync(T entity);
        bool Update(T entity);
        bool Delete(int id);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByExpression(Expression<Func<T, bool>> exp);
        List<T> GetListByExpression(Expression<Func<T, bool>> exp);
    }
}
