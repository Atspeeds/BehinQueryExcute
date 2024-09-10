using _01_FrameWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _0_FrameWork.FW.Infrastrure
{
	public class RepositoryBase<TKey, T> : IRepositoryBase<TKey, T> where T : class
    {
        private DbContext DbContext;

        public RepositoryBase(DbContext dbContext)
        {
            DbContext = dbContext;
        }
        public Task AddAysenc(T entity)
        {
            DbContext.Set<T>().AddAsync(entity);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAysenc(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().AnyAsync(expression);
        }

        public async Task<T> GetAysenc(TKey key)
        {
            return await DbContext.Set<T>().FindAsync(key);
        }

        public async Task<IEnumerable<T>> GetAysenc()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }

        public Task SaveAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }
}
