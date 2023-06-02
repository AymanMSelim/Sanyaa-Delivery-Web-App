using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanyaaDelivery.Infra.Data
{
    public static class ExtensionMethod
    {
        public static IQueryable<TEntity> AsTracking<TEntity>(this IRepository<TEntity> query, bool trackingEnabled)where TEntity : class
        {
            return trackingEnabled ? query.DbSet.AsQueryable() : query.DbSet.AsNoTracking();
        }

        public static IQueryable<TEntity> AsTracking<TEntity>(this IQueryable<TEntity> query, bool trackingEnabled) where TEntity : class
        {
            return trackingEnabled ? query : query.AsNoTracking();
        }
    }
}
