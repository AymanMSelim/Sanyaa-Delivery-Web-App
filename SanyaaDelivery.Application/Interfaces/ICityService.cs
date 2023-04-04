using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ICityService
    {
        Task<int> AddAsync(CityT city);
        Task<List<CityT>> GetListAsync(int? countryId = null, int? governorateId = null, string cityName = null);
        Task<List<CityT>> GetListByBranchIdAsync(int branchId);
        Task<CityT> GetAsync(int id, bool includeGov = false);
        Task<CityT> GetAsync(string cityName);
        Task<int> GetMinimumChargeAsync(int? cityId = null, int? branchId = null);
        Task<int> DeletetAsync(int id);
        Task<int> UpdateAsync(CityT city);
        Task<BranchT> GetCityBranchAsync(int cityId);
        Task<int> GetCityBranchIdAsync(int cityId);
    }
}
