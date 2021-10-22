using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IAccountTypeService
    {
        Task<List<AccountTypeT>> GetList(bool getActiveOnly);

        Task<int> Add(AccountTypeT accountType);
    }
}
