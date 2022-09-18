using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IRegionService
    {
        Task<int> AddAsync(RegionT region);
        Task<List<RegionT>> GetListAsync(int? countryId = null, int? governorateId = null, int? cityId = null, string regionName = null);
        Task<RegionT> GetAsync(int id);
        Task<RegionT> GetAsync(string regionName);
        Task<int> DeletetAsync(int id);
        Task<int> UpdateAsync(RegionT region);
    }
}
