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
        private bool _isRollBack = false;
        private bool _isCommit;
        private int _noOfTransaction = 0;
        public UnitOfWork(SanyaaDatabaseContext context)
        {
            this.context = context;
        }

        public bool IsTransaction { get => _isTransaction; }

        public int NoOfTransaction { get => _noOfTransaction; }
        
        private DbContext Context { get => context; }

        private IDbContextTransaction _contextTransaction;

        public async Task<int> CommitAsync(bool checkNoOfTransaction = true)
        {
            if (checkNoOfTransaction)
            {
                if (NoOfTransaction <= 1)
                {
                    var affectedRows = await context.SaveChangesAsync();
                    _contextTransaction.Commit();
                    _isCommit = true;
                    return affectedRows;
                }
                else
                {
                    _noOfTransaction--;
                    return 1;
                }
            }
            else
            {
                var affectedRows = await context.SaveChangesAsync();
                _contextTransaction.Commit();
                _isCommit = true;
                return affectedRows;
            }
        }

        public void RollBack()
        {
            if(_contextTransaction != null)
            {
                _contextTransaction.Rollback();
                _isRollBack = true;
            }
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public void DisposeTransaction(bool checkNoOfTransaction = true)
        {
            if (checkNoOfTransaction)
            {
                if (_noOfTransaction > 0)
                {
                    _noOfTransaction--;
                }
                if (_isTransaction && _noOfTransaction > 0)
                {
                    return;
                }
                Dispose();
            }
            else
            {
                Dispose();
            }
            //if(context != null)
            //{
            //    context.Dispose();
            //}
        }

        public void Dispose()
        {
            if (_contextTransaction != null)
            {
                if (_isRollBack == false && _isCommit == false)
                {
                    _contextTransaction.Rollback();
                }
                _contextTransaction.Dispose();
                _contextTransaction = null;
            }
        }

        public bool StartTransaction()
        {
            bool isNewTransaction = false;
            _isTransaction = true;
            _noOfTransaction++;
            if (_contextTransaction is null)
            {
                _contextTransaction = context.Database.BeginTransaction();
                isNewTransaction = true;
            }
            return isNewTransaction;
        }
    }
}
