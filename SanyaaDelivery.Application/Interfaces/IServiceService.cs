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
        Task<ServiceT> GetAsync(int serviceId, bool includeDepartment = false);
        Task<DepartmentT> GetServiceMainDepartmentAsync(int serviceId);
        Task<List<ServiceT>> GetServiceMainDepartmentAsync(List<int> serviceIdList);
        Task<List<ServiceT>> GetAsync(string serviceName);
        Task<int> UpdateAsync(ServiceT service);
        Task<List<ServiceT>> GetListByMainDeparmentAsync(int departmentId);
        Task<List<ServiceT>> GetListByDeparmentSub0Async(int departmentSub0Id);
        Task<List<ServiceT>> GetOfferListByMainDeparmentAsync(int departmentId);
        Task<List<ServiceT>> GetOfferListByDeparmentSub0Async(int departmentSub0Id);
        Task<List<ServiceT>> GetListByDepartmentSub1Async(int departmentSub1Id);
        Task<List<ServiceT>> GetOfferListByDepartmentSub1Async(int departmentSub1Id);
        Task<int> DeleteAsync(int serviceId);
        Task<List<ServiceT>> GetServiceList(int? mainDepartmentId = null, int? departmentSub0Id = null, int? departmentSub1Id = null, bool? getOffer = null);
        Task<List<ServiceCustom>> GetCustomServiceList(int clientId, int? mainDepartmentId = null, int? departmentSub0Id = null, int? departmentSub1Id = null, bool? getOffer = null);
        Task<List<ServiceCustom>> ConvertServiceToCustom(List<ServiceT> serviceList, int clientId);
        Task<List<ServiceCustom>> ConvertServiceToCustomMultiDepartment(List<ServiceT> serviceList, int clientId);
    }
}
