using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Infra.Data.Context;
using SanyaaDelivery.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace SanyaaDelivery.Infra.Data.Repositories
{
    public class BaseRepository<Entity> : IRepository<Entity> where Entity : class
    {
        public DbContext DbContext { get ; set; }
        public DbSet<Entity> DbSet { get ; set; }

        sanyaadatabaseContext sanyaaContext;

        public BaseRepository(sanyaadatabaseContext dbContext)
        {
            this.DbContext = dbContext;
            this.sanyaaContext = dbContext;
            DbSet = dbContext.Set<Entity>();
        }

        public void Delete(object id)
        {
            Entity entity = Get(id);
            DbSet.Remove(entity);
        }

        public Entity Get(object id)
        {
            return DbSet.Find(id);
            
        }

        public IEnumerable<Entity> GetAll()
        {
            return DbSet;
        }

        public void Insert(Entity entity)
        {
            DbSet.Add(entity);
        }

        public void Update(object id, Entity entity)
        {
            Entity entity1 = Get(id);
            entity1 = entity;
            DbContext.Entry(entity1).State = EntityState.Modified;
        }

        public IQueryable<Entity> Where(Expression<Func<Entity, bool>> filter)
        {
            return DbSet.Where(filter);
        }
    }
}
