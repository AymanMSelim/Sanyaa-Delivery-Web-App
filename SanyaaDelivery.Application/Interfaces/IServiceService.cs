using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IServiceService
    {
        Task<int> AddAsync(ServiceT service);
        Task<ServiceT> GetAsync(int serviceId);
        Task<List<ServiceT>> GetAsync(string serviceName);
        Task<int> UpdateAsync(ServiceT service);
        Task<List<ServiceT>> GetListByMainDeparmentAsync(int departmentId);
        Task<List<ServiceT>> GetListByDeparmentSub0Async(int departmentSub0Id);
        Task<List<ServiceT>> GetOfferListByMainDeparmentAsync(int departmentId);
        Task<List<ServiceT>> GetOfferListByDeparmentSub0Async(int departmentSub0Id);
        Task<List<ServiceT>> GetListAsync(int departmentSub1Id);
        Task<List<ServiceT>> GetListAsync(string departmentName, string sub0DepartmentName, string sub1DeparmetnName, string serviceName, bool getOfferOnly = false);
        Task<List<ServiceT>> GetOfferListAsync(int departmentSub1Id);
        Task<List<ServiceT>> GetOfferListAsync(string departmentName, string sub0DepartmentName, string sub1DeparmetnName, string serviceName);
        Task<int> DeleteAsync(int serviceId);

    }
}
