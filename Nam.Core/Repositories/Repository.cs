using Microsoft.EntityFrameworkCore;
using Nam.Core.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Core.Repositories
{
    public class Repository<DBContext> : IRepository where DBContext : DbContext
    {
        private readonly DBContext context;
        public Repository(DBContext _context)
        {
            context = _context;
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : Entity<long>
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync<TEntity>(long id) where TEntity : Entity<long>
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> GetAsync<TEntity>(long id) where TEntity : Entity<long>
        {
            return await context.Set<TEntity>().FindAsync(id);
        }
        public async Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<long>
        {
            return await context.Set<TEntity>().Where(predicate).SingleOrDefaultAsync();
        }
        public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<long>
        {
            return await context.Set<TEntity>().AnyAsync(predicate);
        }
        public bool Any<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<long>
        {
            return context.Set<TEntity>().Any(predicate);
        }

        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : Entity<long>
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : Entity<long>
        {
            return context.Set<TEntity>().AsQueryable();
        }
        public IQueryable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<long>
        {
            return context.Set<TEntity>().Where(predicate);
        }
        public TEntity Get<TEntity>(long id) where TEntity : Entity<long>
        {
            return context.Set<TEntity>().Find(id);
        }
        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<long>
        {
            return context.Set<TEntity>().Where(predicate).SingleOrDefault();
        }
        public async Task SoftDeleteAsync<TEntity>(TEntity entity) where TEntity : Entity<long>, ISoftDelete
        {
            context.Entry(entity).State = EntityState.Modified;
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            await context.SaveChangesAsync();
        }
        public void SoftDelete<TEntity>(TEntity entity) where TEntity : Entity<long>, ISoftDelete
        {
            context.Entry(entity).State = EntityState.Modified;
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            context.SaveChanges();
        }
    }
}
