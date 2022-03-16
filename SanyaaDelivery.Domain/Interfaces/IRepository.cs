using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SanyaaDelivery.Domain
{
    public interface IRepository<Entity> where Entity : class
    {
        DbContext DbContext { get; set; }

        DbSet<Entity> DbSet { get; set; }

        void AddAsync(Entity entity);

        void Update(object id, Entity entity);

        Task DeleteAsync(object id);

        Task<Entity> GetAsync(object id);

        Task<List<Entity>> GetListAsync();

        IQueryable<Entity> Where(Expression<Func<Entity, bool>> filter);

        Task<int> SaveAsync();
    }
}
