using Microsoft.EntityFrameworkCore;
using Roommates.Api.Data.IRepositories;
using Roommates.Infrastructure.Base;

namespace Roommates.Api.Data.Repositories
{
    public class BaseRepository<TEntity, TDbContext> : IBaseRepository<TEntity, TDbContext> where TEntity : BaseModel where TDbContext : DbContext
    {
        public TDbContext DbContext { get; set; }
        public DbSet<TEntity> Entities { get => DbContext.Set<TEntity>(); }

        public BaseRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("Null value can not be added");
            }

            var entry = await DbContext.AddAsync(entity);

            return entry.Entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new Exception("Null value can not be added");
            }

            await DbContext.AddRangeAsync(entities);
        }

        public virtual IQueryable<TEntity> GetAll(bool includeRemovedEntities = false)
        {
            if (typeof(TEntity).GetInterfaces().Contains(typeof(IPersistentEntity)) && includeRemovedEntities == false)
            {
                return Entities.Where(l => (l as IPersistentEntity).EntityState == Infrastructure.Enums.EntityState.Active);
            }

            return Entities;
        }

        public async Task<TEntity> GetAsync(Guid id, bool includeRemovedEntities = false)
        {
            var entity = await Entities.FindAsync(id);

            if (entity != null && entity is IPersistentEntity && (entity as IPersistentEntity).EntityState == Infrastructure.Enums.EntityState.Inactive && includeRemovedEntities == false)
            {
                return null;
            }

            return entity;
        }

        public virtual void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("Null entity can not be removed");
            }

            if (entity is IPersistentEntity)
            {
                (entity as IPersistentEntity).EntityState = Infrastructure.Enums.EntityState.Inactive;
                (entity as IPersistentEntity).InactivatedDate = DateTime.UtcNow;
                DbContext.Update(entity);
            }
            else
            {
                DbContext.Remove(entity);
            }
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new Exception("Null entities can not be removed");
            }

            if (entities.All(l => l is IPersistentEntity))
            {
                foreach (TEntity entity in entities)
                {
                    (entity as IPersistentEntity).EntityState = Infrastructure.Enums.EntityState.Inactive;
                    (entity as IPersistentEntity).InactivatedDate = DateTime.UtcNow;
                    DbContext.Update(entity);
                }
            }
            else
            {
                DbContext.RemoveRange(entities);
            }
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        public virtual TEntity Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("Null entity can not be updated");
            }

            if (entity is IPersistentEntity)
            {
                if ((entity as IPersistentEntity).EntityState == Infrastructure.Enums.EntityState.Inactive)
                {
                    (entity as IPersistentEntity).InactivatedDate = DateTime.UtcNow;
                }
            }

            entity.LastModifiedDate = DateTime.UtcNow;

            var entry = DbContext.Update(entity);

            return entry.Entity;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new Exception("Null entities can not be updated");
            }

            foreach (TEntity entity in entities)
            {
                entity.LastModifiedDate = DateTime.UtcNow;

                if ((entity as IPersistentEntity).EntityState == Infrastructure.Enums.EntityState.Inactive)
                {
                    (entity as IPersistentEntity).InactivatedDate = DateTime.UtcNow;
                }
            }

            DbContext.UpdateRange(entities);
        }
    }
}
