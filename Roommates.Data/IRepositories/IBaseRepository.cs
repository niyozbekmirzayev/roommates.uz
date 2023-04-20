using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data.IRepositories
{
    public interface IBaseRepository<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
    {
        TDbContext DbContext { get; set; }
        DbSet<TEntity> Entities { get; }

        IQueryable<TEntity> GetAll(bool includeRemovedEntities = false);
        Task<TEntity> GetAsync(Guid id, bool includeRemovedEntities = false);

        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> AddAsync(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        Task<int> SaveChangesAsync();
    }
}
