using App.Global.ExtensionMethods;
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
    public class ServiceRatioService : IServiceRatioService
    {
        private readonly IRepository<ServiceRatioT> serviceRatioRepository;
        private readonly IRepository<ServiceRatioDetailsT> serviceRatioDetailsRepository;
        private readonly IClientService clientService;

        public ServiceRatioService(IRepository<ServiceRatioT> serviceRatioRepository, IRepository<ServiceRatioDetailsT> serviceRatioDetailsRepository, IClientService clientService)
        {
            this.serviceRatioRepository = serviceRatioRepository;
            this.serviceRatioDetailsRepository = serviceRatioDetailsRepository;
            this.clientService = clientService;
        }

        public async Task<int> AddAsync(ServiceRatioT serviceRatio)
        {
            await serviceRatioRepository.AddAsync(serviceRatio);
            return await serviceRatioRepository.SaveAsync();
        }

        public async Task<int> AddDetailAsync(ServiceRatioDetailsT serviceRatioDetail)
        {
            await serviceRatioDetailsRepository.AddAsync(serviceRatioDetail);
            return await serviceRatioRepository.SaveAsync();
        }

        public async Task<int> DeletetAsync(int id)
        {
            await serviceRatioRepository.DeleteAsync(id);
            return await serviceRatioRepository.SaveAsync();
        }

        public async Task<int> DeletetDetailAsync(int id)
        {
            await serviceRatioDetailsRepository.DeleteAsync(id);
            return await serviceRatioDetailsRepository.SaveAsync();
        }

        public Task<ServiceRatioT> GetAsync(int id)
        {
            return serviceRatioRepository.GetAsync(id);
        }

        public Task<ServiceRatioDetailsT> GetDetailsAsync(int id)
        {
            return serviceRatioDetailsRepository.GetAsync(id);
        }

        public Task<List<ServiceRatioDetailsT>> GetDetailsListAsync(int? serviceRatioId = null)
        {
            var query = serviceRatioDetailsRepository.DbSet.AsQueryable();
            if (serviceRatioId.HasValue)
            {
                query = query.Where(d => d.ServiceRatioId == serviceRatioId);
            }
            return query.ToListAsync();
        }

        public Task<List<ServiceRatioT>> GetListAsync(string descrition)
        {
            if (string.IsNullOrEmpty(descrition))
            {
                return serviceRatioRepository.DbSet.ToListAsync();
            }
            else
            {
                return serviceRatioRepository.Where(d => d.Description.Contains(descrition)).ToListAsync();
            }
        }

        public Task<List<ServiceRatioT>> GetListAsync(int? cityId = null, int? departmentId = null, bool getDetails = false, bool? isActive = null)
        {
            var query = serviceRatioRepository.DbSet.AsQueryable();
            if (cityId.HasValue)
            {
                query = query.Where(d => d.ServiceRatioDetailsT.Any(t => t.CityId == cityId));
            }
            if (departmentId.HasValue)
            {
                query = query.Where(d => d.ServiceRatioDetailsT.Any(t => t.DepartmentId == departmentId));
            }
            if (isActive.HasValue)
            {
                query = query.Where(d => d.IsActive == isActive);
            }
            if (getDetails)
            {
                query = query.Include(d => d.ServiceRatioDetailsT);
            }
            return query.ToListAsync();
        }

        public async Task<List<ServiceRatioT>> GetListByClientIdAsync(int clientId)
        {
            int? cityId = null; int? departmentId = null;
            var client =  await clientService.GetAsync(clientId);
            var clientAddressList =  await clientService.GetAddressListAsync(clientId);
            if (clientAddressList.IsEmpty())
            {
                return null; 
            }
            if(clientAddressList.Count == 1)
            {
                cityId = clientAddressList.FirstOrDefault().CityId;
            }
            else
            {
                
            }
            return null;
        }

        public async Task<decimal> GetRatioAsync(int? cityId = null, int? departmentId = null)
        {
            var serviceRatioList = await GetListAsync(cityId, departmentId, true);
            if (serviceRatioList.IsEmpty())
            {
                return 1;
            }
            else
            {
                var ratio = serviceRatioList.LastOrDefault().Ratio;
                if (ratio.IsNull() || ratio == 0)
                {
                    return  1;
                }
                else
                {
                    return 1 + (ratio.Value / 100);
                }
            }
        }

        public Task<int> UpdateAsync(ServiceRatioT serviceRatio)
        {
            serviceRatioRepository.Update(serviceRatio.ServiceRatioId, serviceRatio);
            return serviceRatioRepository.SaveAsync();
        }

        public Task<int> UpdateDetailAsync(ServiceRatioDetailsT serviceRatioDetail)
        {
            serviceRatioDetailsRepository.Update(serviceRatioDetail.ServiceRatioDetailsId, serviceRatioDetail);
            return serviceRatioDetailsRepository.SaveAsync();
        }
    }
}
