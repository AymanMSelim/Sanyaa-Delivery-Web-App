using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IAccountTypeService
    {
        Task<List<AccountTypeT>> GetList();

        Task<int> Add(AccountTypeT accountType);
    }
}
