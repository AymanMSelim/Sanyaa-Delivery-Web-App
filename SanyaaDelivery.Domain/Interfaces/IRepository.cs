using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SanyaaDelivery.Domain
{
    public interface IRepository<Entity> where Entity : class
    {
        DbContext DbContext { get; set; }

        DbSet<Entity> DbSet { get; set; }

        void Insert(Entity entity);

        void Update(object id, Entity entity);

        void Delete(object id);

        Entity Get(object id);

        IEnumerable<Entity> GetAll();

        IQueryable<Entity> Where(Expression<Func<Entity, bool>> filter);
    }
}
