using Application.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public GenericRepository(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected DbContext Context { get; }
        protected DbSet<TEntity> Entity => Context.Set<TEntity>();

        public async Task<int> AddAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;

            return await Context.SaveChangesAsync();
        }

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>().Where(x => !x.Deleted);
        }

        public async Task BulkAdd(IEnumerable<TEntity> entities)
        {
            if(entities != null && !entities.Any())
                await Task.CompletedTask;

            await Context.AddRangeAsync(entities);

            await Context.SaveChangesAsync();
        }

        public Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            if(predicate == null)
                return Task.CompletedTask;

            Context.RemoveRange(Entity.Where(predicate));

            return Context.SaveChangesAsync();
        }

        public Task BulkDelete(IEnumerable<TEntity> entities)
        {
            if(entities != null && !entities.Any())
                return Task.CompletedTask;

            Context.RemoveRange(entities);

            return Context.SaveChangesAsync();
        }

        public Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            if(ids != null && !ids.Any())
                return Task.CompletedTask;

            Context.RemoveRange(Entity.Where(x => ids.Contains(x.Id)));

            return Context.SaveChangesAsync();
        }

        public async Task BulkUpdate(IEnumerable<TEntity> entities)
        {
            if(entities != null && !entities.Any())
                await Task.CompletedTask;

            Context.UpdateRange(entities);

            await Context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;

            return await Context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            return await DeleteAsync(Entity.Find(id));
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Query();

            if(predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if(noTracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<List<TEntity>> GetAllAsync(bool noTracking = true)
        {
            var query = Query();

            if(noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var foundEntity = await Entity.FindAsync(id);

            if(foundEntity == null)
                return null;

            if(noTracking)
                Context.Entry(foundEntity).State = EntityState.Detached;

            foreach(var include in includes)
            {
                Context.Entry(foundEntity).Reference(include).Load();
            }

            return foundEntity;
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Query();

            if(predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if(noTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Query();

            if(predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if(orderBy != null)
                query = orderBy(query);

            if(noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Query();

            if(predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if(noTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            return await Context.SaveChangesAsync();
        }

        private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            if(includes != null)
            {
                foreach(var item in includes)
                {
                    query = query.Include(item);
                }
            }

            return query;
        }

        public async Task<List<TEntity>> GetListNotDeletedAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Query();

            if(predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if(orderBy != null)
                query = orderBy(query);

            if(noTracking)
                query = query.AsNoTracking();

            query = query.Where(x => !x.Deleted);

            return await query.ToListAsync();
        }
    }
}
