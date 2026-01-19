using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Implementation
{
    public class Repository<TEntity, TPrimaryKeyType> : IRepository<TEntity, TPrimaryKeyType> where TEntity : class
    {

        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            Context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public void UpdateRange(List<TEntity> entities)
        {
            Context.Set<TEntity>().UpdateRange(entities);
        }

        public List<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public ValueTask<TEntity> GetByIdAsync(TPrimaryKeyType id)
        {
            return Context.Set<TEntity>().FindAsync(id);
        }

        public ValueTask<TEntity> GetByIdAsync(string id)
        {
            return Context.Set<TEntity>().FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(List<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public int Count()
        {
            return Context.Set<TEntity>().Count();
        }
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }
    }
}
