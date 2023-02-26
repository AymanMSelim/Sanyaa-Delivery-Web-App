using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
        private int _noOfTransaction = 0;
        public UnitOfWork(SanyaaDatabaseContext context)
        {
            this.context = context;
        }

        public bool IsTransaction { get => _isTransaction; }

        public int NoOfTransaction { get => _noOfTransaction; }
        
        private DbContext Context { get => context; }

        private IDbContextTransaction _contextTransaction;

        public async Task<int> CommitAsync()
        {
            if (NoOfTransaction <= 1)
            {
                var affectedRows = await context.SaveChangesAsync();
                _contextTransaction.Commit();
                return affectedRows;
            }
            else
            {
                _noOfTransaction--;
                return 1;
            }
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if(_isTransaction && _noOfTransaction > 0)
            {
                return;
            }
            if(_contextTransaction != null)
            {
                _contextTransaction.Dispose();
            }
            if(context != null)
            {
                context.Dispose();
            }
        }

        public void StartTransaction()
        {
            _isTransaction = true;
            _noOfTransaction++;
            if (_contextTransaction is null)
            {
                _contextTransaction = context.Database.BeginTransaction();
            }
        }
    }
}
