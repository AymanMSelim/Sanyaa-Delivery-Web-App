using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<RoleT> roleRepository;

        public RoleService(IRepository<RoleT> roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public Task<int> Add(RoleT role)
        {
            roleRepository.AddAsync(role);
            return roleRepository.SaveAsync();
        }

        public Task<RoleT> Get(int id)
        {
            return roleRepository.GetAsync(id);
        }

        public Task<List<RoleT>> GetList()
        {
            return roleRepository.GetListAsync();
        }
    }
}
