using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class AccountRoleService : IAccountRoleService
    {
        private readonly IRepository<AccountRoleT> accountRoleRepository;

        public AccountRoleService(IRepository<AccountRoleT> accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
        }
        public async Task<int> Add(AccountRoleT accountRole)
        {
            await accountRoleRepository.AddAsync(accountRole);
            return await accountRoleRepository.SaveAsync();
        }

        public Task<List<AccountRoleT>> GetList(int accountId, bool getActiveOnly = true)
        {
            var result = accountRoleRepository.Where(r => r.AccountId == accountId).Include("Role").ToListAsync();
            return result;
        }
    }
}
