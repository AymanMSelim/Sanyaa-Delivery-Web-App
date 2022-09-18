using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class AppSettingService : IAppSettingService
    {
        private readonly IRepository<AppSettingT> appSettingRepository;

        public AppSettingService(IRepository<AppSettingT> appSettingRepository)
        {
            this.appSettingRepository = appSettingRepository;
        }
        public Task<AppSettingT> Get(string key)
        {
            return appSettingRepository.Where(d => d.SettingKey == key).FirstOrDefaultAsync();
        }

        public Task<List<AppSettingT>> GetListAsync()
        {
            return appSettingRepository.DbSet.ToListAsync();
        }
    }
}
