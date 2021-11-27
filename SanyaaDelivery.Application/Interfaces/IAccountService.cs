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

        Task<AccountT> Get(int accountType, string referenceId);

        Task<int> Add(AccountT account);

        Task<int> Update(AccountT account);
    }
}
