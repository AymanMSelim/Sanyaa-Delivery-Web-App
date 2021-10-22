using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AccountT> Get(int id);

        Task<AccountT> Get(int accountType, int referenceId);

        Task<int> Add(AccountT account);
    }
}
