using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class EmployeeAppAccountService : IEmployeeAppAccountService
    {
        private readonly IRepository<LoginT> accountRepository;

        public EmployeeAppAccountService(IRepository<LoginT> accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        
        public Task<LoginT> Get(string id)
        {
            return accountRepository.GetAsync(id);
        }

        public bool IsActive(string id)
        {
            var empLogin = Get(id).Result;
            return empLogin.LoginAccountState != 0;
        }

        public bool IsOnline(string id)
        {
            return accountRepository.GetAsync(id).Result.LastActiveTimestamp > DateTime.Now.AddMinutes(-3);
        }

        public DateTime LastSeenTime(string id)
        {
            return accountRepository.GetAsync(id).Result.LastActiveTimestamp;
        }
    }
}
