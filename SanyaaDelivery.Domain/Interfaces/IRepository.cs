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

        void Insert(Entity entity);

        void Update(object id, Entity entity);

        void Delete(object id);

        Task<Entity> Get(object id);

        Task<List<Entity>> GetAll();

        IQueryable<Entity> Where(Expression<Func<Entity, bool>> filter);

        Task<int> Save();
    }
}
