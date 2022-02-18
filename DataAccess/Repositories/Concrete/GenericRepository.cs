using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class GenericRepository<T>:IGenericRepository<T> where T:class
    {
        private readonly ProjectDbContext _context;
        internal DbSet<T> dbSet;

        public GenericRepository(ProjectDbContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }


        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public bool Update(T entity)
        {
            try
            {
                dbSet.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var temp = dbSet.Find(id);
            if(temp is null)
            {
                return false;
            }
            dbSet.Remove(temp);
            _context.SaveChanges();
            return true;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetByExpression(Expression<Func<T, bool>> exp)
        {
            return await dbSet.FirstOrDefaultAsync(exp);
        }

        public List<T> GetListByExpression(Expression<Func<T, bool>> exp)
        {
            return dbSet.Where(exp).ToList();
        }
    }
}
