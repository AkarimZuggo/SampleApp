using System.Linq.Expressions;

namespace DataAccess.Interfaces
{
    public interface IRepository<TEntity, in TPrimaryKeyType> where TEntity : class
    {
        ValueTask<TEntity> GetByIdAsync(TPrimaryKeyType id);
        ValueTask<TEntity> GetByIdAsync(string id);
        Task<List<TEntity>> GetAllAsync();
        List<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        Task AddRangeAsync(List<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(List<TEntity> entities);
        int Count();

    }
}
