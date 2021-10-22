using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SanyaaDelivery.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<AccountT> accountRepository;

        public AccountService(IRepository<AccountT> accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public Task<int> Add(AccountT account)
        {
            accountRepository.Insert(account);
            return accountRepository.Save();
        }

        public Task<AccountT> Get(int id)
        {
            return accountRepository.Get(id);
        }

        public Task<AccountT> Get(int accountType, int referenceId)
        {
            return accountRepository.Where(a => a.AccountTypeId == accountType && a.AccountReferenceId == referenceId).FirstOrDefaultAsync();
        }
    }
}
