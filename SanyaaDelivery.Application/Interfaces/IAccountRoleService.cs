using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IAccountRoleService
    {
        Task<List<AccountRoleT>> GetList(int accountId, bool getActiveOnly);

        Task<int> Add(AccountRoleT accountRole);
    }
}
