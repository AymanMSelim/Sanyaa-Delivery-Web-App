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
        Task<List<ServiceT>> GetListByDepartmentSub1Async(int departmentSub1Id);
        Task<List<ServiceT>> GetOfferListByDepartmentSub1Async(int departmentSub1Id);
        Task<int> DeleteAsync(int serviceId);

    }
}
