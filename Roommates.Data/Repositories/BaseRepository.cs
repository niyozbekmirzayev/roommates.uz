using Microsoft.EntityFrameworkCore;
using Roommates.Data.IRepositories;
using Roommates.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data.Repositories
{
    public class BaseRepository<TEntity, TDbContext> : IBaseRepository<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
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

        public virtual IQueryable<TEntity> GetAll()
        {
            if (typeof(TEntity).GetInterfaces().Contains(typeof(IPersistentEntity)))
            {
                return Entities.Where(l => (l as IPersistentEntity).EntityState == Domain.Enums.EntityState.Active);
            }

            return Entities;
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            var entity = await Entities.FindAsync(id);

            if (entity != null && entity is IPersistentEntity &&
                (entity as IPersistentEntity).EntityState == Domain.Enums.EntityState.Inactive)
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
                (entity as IPersistentEntity).EntityState = Domain.Enums.EntityState.Inactive;
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
                    (entity as IPersistentEntity).EntityState = Domain.Enums.EntityState.Inactive;
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

            var entry = DbContext.Update(entity);

            return entry.Entity;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new Exception("Null entities can not be updated");
            }

            DbContext.UpdateRange(entities);
        }
    }
}
