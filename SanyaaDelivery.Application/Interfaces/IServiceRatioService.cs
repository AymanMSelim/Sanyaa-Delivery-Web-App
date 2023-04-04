using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IServiceRatioService
    {
        Task<int> AddAsync(ServiceRatioT serviceRatio);
        Task<int> AddDetailAsync(ServiceRatioDetailsT serviceRatioDetail);
        Task<List<ServiceRatioT>> GetListAsync(string descrition);
        Task<List<ServiceRatioT>> GetListAsync(int? cityId = null, int? departmentId = null, bool getDetails = false, bool ? isActive = null);
        Task<decimal> GetRatioAsync(int cityId, int departmentId);
        Task<List<ServiceRatioT>> GetListByClientIdAsync(int clientId);
        Task<List<ServiceRatioDetailsT>> GetDetailsListAsync(int? serviceRatioId = null);
        Task<ServiceRatioT> GetAsync(int id);
        Task<ServiceRatioDetailsT> GetDetailsAsync(int id);
        Task<int> DeletetAsync(int id);
        Task<int> DeletetDetailAsync(int id);
        Task<int> UpdateAsync(ServiceRatioT ServiceRatio);
        Task<int> UpdateDetailAsync(ServiceRatioDetailsT serviceRatioDetail);
    }
}
