using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.Services
{
    public class EmployeeAppAccountService : IEmployeeAppAccountService
    {
        private readonly IRepository<LoginT> accountRepository;

        public EmployeeAppAccountService(IRepository<LoginT> accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        
        public LoginT Get(string id)
        {
            return accountRepository.Get(id);
        }

        public bool IsActive(string id)
        {
            return accountRepository.Get(id).LoginAccountState == 0 ? false : true;
        }

        public bool IsOnline(string id)
        {
            return accountRepository.Get(id).LastActiveTimestamp > DateTime.Now.AddMinutes(-3) ? true : false;
        }

        public DateTime LastSeenTime(string id)
        {
            return accountRepository.Get(id).LastActiveTimestamp;
        }
    }
}
