using Nam.Core.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Core.Repositories
{
    public interface IRepository
    {
        Task<TEntity> GetAsync<TEntity>(long id) where TEntity : Entity<long>;
        Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<long>;
        Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<long>;
        bool Any<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<long>;
        Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : Entity<long>;
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : Entity<long>;
        Task<TEntity> DeleteAsync<TEntity>(long id) where TEntity : Entity<long>;
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : Entity<long>;
        IQueryable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<long>;
        TEntity Get<TEntity>(long id) where TEntity : Entity<long>;
        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<long>;
        Task SoftDeleteAsync<TEntity>(TEntity entity) where TEntity : Entity<long>, ISoftDelete;
        void SoftDelete<TEntity>(TEntity entity) where TEntity : Entity<long>, ISoftDelete;
    }
}
