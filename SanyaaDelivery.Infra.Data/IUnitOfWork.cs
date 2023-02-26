using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Infra.Data
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsTransaction { get; }
        int NoOfTransaction { get; }
        Task<int> SaveAsync();
        Task<int> CommitAsync();
        void StartTransaction();
    }
}
