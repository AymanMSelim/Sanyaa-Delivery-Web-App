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
    public class AccountTypeService : IAccountTypeService
    {
        private readonly IRepository<AccountTypeT> accountTypeRepository;

        public AccountTypeService(IRepository<AccountTypeT> accountTypeRepository)
        {
            this.accountTypeRepository = accountTypeRepository;
        }
        public async Task<int> Add(AccountTypeT accountType)
        {
            await accountTypeRepository.AddAsync(accountType);
            return await accountTypeRepository.SaveAsync();
        }

        public Task<List<AccountTypeT>> GetList()
        {
            return accountTypeRepository.GetListAsync();
        }
    }
}
