using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Infra.Data.Context;
using SanyaaDelivery.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.Infra.Data.Repositories
{
    public class BaseRepository<Entity> : IRepository<Entity> where Entity : class
    {
        public DbContext DbContext { get ; set; }
        public DbSet<Entity> DbSet { get ; set; }
        private readonly IUnitOfWork unitOfWork;

        public BaseRepository(SanyaaDatabaseContext dbContext, IUnitOfWork unitOfWork)
        {
            this.DbContext = dbContext;
            this.unitOfWork = unitOfWork;
            DbSet = dbContext.Set<Entity>();
        }

        public async Task DeleteAsync(object id)
        {
            Entity entity = await GetAsync(id);
            DbSet.Remove(entity);
        }

        public Task<Entity> GetAsync(object id)
        {
            return DbSet.FindAsync(id);
        }

        public Task<List<Entity>> GetListAsync()
        {
            return DbSet.ToListAsync();
        }

        public Task AddAsync(Entity entity)
        {
            return DbSet.AddAsync(entity);
        }

        public void Update(object id, Entity updatedEntity)
        {
            DbSet.Update(updatedEntity);
        }

        public IQueryable<Entity> Where(Expression<Func<Entity, bool>> filter)
        {
            return DbSet.Where(filter);
        }

        public async Task<int> SaveAsync()
        {
            if (unitOfWork.IsTransaction)
            {
                return 1;
            }
            return await DbContext.SaveChangesAsync();
        }
    }
}
