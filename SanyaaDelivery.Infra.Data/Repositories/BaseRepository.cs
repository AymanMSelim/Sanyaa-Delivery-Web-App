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
            try
            {
                Entity entity = await GetAsync(id);
                DbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
            }
          
        }

        public Task<Entity> GetAsync(object id)
        {
            try
            {
                return DbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
                return null;
            }
        }

        public Task<List<Entity>> GetListAsync()
        {
            try
            {
                return DbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
                return null;
            }
        }

        public Task AddAsync(Entity entity)
        {
            try
            {
                return DbSet.AddAsync(entity);
            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
                return null;
            }
        }

        public void Update(object id, Entity updatedEntity)
        {
            try
            {
                DbSet.Update(updatedEntity);
            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
            }
        }

        public IQueryable<Entity> Where(Expression<Func<Entity, bool>> filter)
        {
            try
            {
                return DbSet.Where(filter);
            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
                return null;
            }
        }

        public Task<int> SaveAsync()
        {
            try
            {
                if (unitOfWork.IsTransaction)
                {
                    return null;
                }
                return DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
                return null;
            }
        }
    }
}
