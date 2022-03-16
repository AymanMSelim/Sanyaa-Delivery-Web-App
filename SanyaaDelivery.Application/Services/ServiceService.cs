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
    public class ServiceService : IServiceService
    {
        private readonly IRepository<ServiceT> serviceRepository;

        public ServiceService(IRepository<ServiceT> serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }
        public Task<int> AddAsync(ServiceT service)
        {
            serviceRepository.AddAsync(service);
            return serviceRepository.SaveAsync();
        }

        public async Task<int> DeleteAsync(int serviceId)
        {
            await serviceRepository.DeleteAsync(serviceId);
            return await serviceRepository.SaveAsync();
        }

        public Task<ServiceT> GetAsync(int serviceId)
        {
            return serviceRepository.GetAsync(serviceId);
        }

        public Task<List<ServiceT>> GetAsync(string serviceName)
        {
            return serviceRepository
                .Where(d => d.ServiceName.Contains(serviceName))
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetListAsync(int departmentSub1Id)
        {
            return serviceRepository
                .Where(d => d.DepartmentId == departmentSub1Id)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetListAsync(string departmentName, string sub0DepartmentName, string sub1DeparmetnName, string serviceName, bool getOfferOnly = false)
        {
            var serviceQuery = serviceRepository.DbSet.AsQueryable();
            if (!string.IsNullOrEmpty(departmentName))
            {
                serviceQuery = serviceQuery.Where(d => d.Department.DepartmentName.Contains(departmentName));
            }
            if (!string.IsNullOrEmpty(sub0DepartmentName))
            {
                serviceQuery = serviceQuery.Where(d => d.Department.DepartmentSub0.Contains(sub0DepartmentName));
            }
            if (!string.IsNullOrEmpty(sub1DeparmetnName))
            {
                serviceQuery = serviceQuery.Where(d => d.Department.DepartmentSub1.Contains(sub1DeparmetnName));
            }
            if (!string.IsNullOrEmpty(serviceName))
            {
                serviceQuery = serviceQuery.Where(d => d.ServiceName.Contains(serviceName));
            }
            if (getOfferOnly)
            {
                serviceQuery = serviceQuery.Where(d => d.ServiceDiscount > 0);
            }
            return serviceQuery.ToListAsync();
        }

        public Task<List<ServiceT>> GetListByDeparmentSub0Async(int departmentSub0Id)
        {
            return serviceRepository
                .Where(d => d.Department.Department.DepartmentSub0Id == departmentSub0Id)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetListByMainDeparmentAsync(int departmentId)
        {
            return serviceRepository
                .Where(d => d.Department.Department.DepartmentNameNavigation.DepartmentId == departmentId)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetOfferListAsync(int departmentSub1Id)
        {
            return serviceRepository
                .Where(d => d.DepartmentId == departmentSub1Id && d.ServiceDiscount > 0)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetOfferListAsync(string departmentName, string sub0DepartmentName, string sub1DeparmetnName, string serviceName)
        {
            return GetListAsync(departmentName, sub0DepartmentName, sub1DeparmetnName, serviceName, true);
        }

        public Task<List<ServiceT>> GetOfferListByDeparmentSub0Async(int departmentSub0Id)
        {
            return serviceRepository
                .Where(d => d.Department.Department.DepartmentSub0Id == departmentSub0Id && d.ServiceDiscount > 0)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetOfferListByMainDeparmentAsync(int departmentId)
        {
            return serviceRepository
                .Where(d => d.Department.Department.DepartmentNameNavigation.DepartmentId == departmentId && d.ServiceDiscount > 0)
                .ToListAsync();
        }

        public Task<int> UpdateAsync(ServiceT service)
        {
            serviceRepository.Update(service.ServiceId, service);
            return serviceRepository.SaveAsync();
        }
    }
}
