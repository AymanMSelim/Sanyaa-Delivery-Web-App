using SanyaaDelivery.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SanyaaDatabaseContext context;

        private bool _isTransaction = false;
        public UnitOfWork(SanyaaDatabaseContext context)
        {
            this.context = context;
        }

        public bool IsTransaction { get => _isTransaction; }

        public Task<int> CommitAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if(context != null)
            {
                context.Dispose();
            }
        }

        public void StartTransaction()
        {
            _isTransaction = true;
        }
    }
}
