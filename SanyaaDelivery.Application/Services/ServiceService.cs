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
        public async Task<int> AddAsync(ServiceT service)
        {
            await serviceRepository.AddAsync(service);
            return await serviceRepository.SaveAsync();
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

        public Task<List<ServiceT>> GetListByDepartmentSub1Async(int departmentSub1Id)
        {
            return serviceRepository
                .Where(d => d.DepartmentId == departmentSub1Id)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetListByDeparmentSub0Async(int departmentSub0Id)
        {
            return serviceRepository
                .Where(d => d.Department.DepartmentSub0Id == departmentSub0Id)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetListByMainDeparmentAsync(int departmentId)
        {
            return serviceRepository
                .Where(d => d.Department.DepartmentSub0Navigation.DepartmentId == departmentId)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetOfferListByDepartmentSub1Async(int departmentSub1Id)
        {
            return serviceRepository
                .Where(d => d.DepartmentId == departmentSub1Id && d.ServiceDiscount > 0)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetOfferListByDeparmentSub0Async(int departmentSub0Id)
        {
            return serviceRepository
                .Where(d => d.Department.DepartmentSub0Id == departmentSub0Id && d.ServiceDiscount > 0 && d.NoDiscount == false)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetOfferListByMainDeparmentAsync(int departmentId)
        {
            return serviceRepository
                .Where(d => d.Department.DepartmentSub0Navigation.DepartmentId == departmentId && d.ServiceDiscount > 0 && d.NoDiscount == false)
                .ToListAsync();
        }

        public Task<int> UpdateAsync(ServiceT service)
        {
            serviceRepository.Update(service.ServiceId, service);
            return serviceRepository.SaveAsync();
        }
    }
}
