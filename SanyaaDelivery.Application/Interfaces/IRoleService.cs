using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IRoleService
    {
        Task<RoleT> Get(int id);

        Task<List<RoleT>> GetList();

        Task<int> Add(RoleT role);
    }
}
