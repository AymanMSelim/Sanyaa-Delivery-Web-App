using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Infra.Data
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsTransaction { get; }
        Task<int> CommitAsync();
        void StartTransaction();
    }
}
