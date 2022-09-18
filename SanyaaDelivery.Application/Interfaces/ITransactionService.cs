using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<int> AddAsync(TransactionT Transaction);
        Task<List<TransactionT>> GetListAsync(sbyte? referenceType = null, string refernceId = null, DateTime? startDate = null, DateTime? endDate = null, bool? isCanceled = null);
        Task<TransactionT> GetAsync(int id);
        Task<int> DeletetAsync(int id);
        Task<int> UpdateAsync(TransactionT Transaction);
    }
}
