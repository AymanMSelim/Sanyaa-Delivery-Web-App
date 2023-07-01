using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class SystemUserService : ISystemUserService
    {
        private readonly IRepository<SystemUserT> systemUserRepository;

        public SystemUserService(IRepository<SystemUserT> systemUserRepository)
        {
            this.systemUserRepository = systemUserRepository;
        }
        public Task<SystemUserT> GetAsync(string userName)
        {
            return systemUserRepository.Where(u => u.SystemUserUsername == userName).SingleOrDefaultAsync();
        }
    }
}
