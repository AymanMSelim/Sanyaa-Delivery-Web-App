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

        public async Task<int> Add(AccountT account)
        {
            await accountRepository.AddAsync(account);
            return await accountRepository.SaveAsync();
        }

        public Task<int> Update(AccountT account)
        {
            accountRepository.Update(account.AccountId, account);
            return accountRepository.SaveAsync();
        }

        public Task<AccountT> Get(int id)
        {
            return accountRepository.GetAsync(id);
        }

        public Task<AccountT> Get(int accountType, string referenceId)
        {
            return accountRepository.Where(a => a.AccountTypeId == accountType && a.AccountReferenceId == referenceId).FirstOrDefaultAsync();
        }
    }
}
