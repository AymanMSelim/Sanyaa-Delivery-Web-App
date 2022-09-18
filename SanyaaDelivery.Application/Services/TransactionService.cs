using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<TransactionT> repo;

        public TransactionService(IRepository<TransactionT> repo)
        {
            this.repo = repo;
        }

        public async Task<int> AddAsync(TransactionT transaction)
        {
            await repo.AddAsync(transaction);
            return await repo.SaveAsync();
        }

        public async Task<int> DeletetAsync(int id)
        {
            await repo.DeleteAsync(id);
            return await repo.SaveAsync();
        }

        public Task<TransactionT> GetAsync(int id)
        {
            return repo.GetAsync(id);
        }

        public Task<List<TransactionT>> GetListAsync(sbyte? referenceType = null, string refernceId = null, DateTime? startDate = null, DateTime? endDate = null, bool? isCanceled = null)
        {
            var query = repo.DbSet.AsQueryable();
            if (referenceType.HasValue)
            {
                query = query.Where(d => d.ReferenceType == referenceType);
            }
            if (!string.IsNullOrEmpty(refernceId))
            {
                query = query.Where(d => d.ReferenceId == refernceId);
            }
            if (startDate.HasValue)
            {
                query = query.Where(d => d.Date.Value >= startDate);
            }
            if (endDate.HasValue)
            {
                query = query.Where(d => d.Date.Value <= endDate);
            }
            if (isCanceled.HasValue)
            {
                query = query.Where(d => d.IsCanceled == isCanceled);
            }
            return query.ToListAsync();
        }

        public Task<int> UpdateAsync(TransactionT transaction)
        {
            repo.Update(transaction.Id, transaction);
            return repo.SaveAsync();
        }
    }
}
