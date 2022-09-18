using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IGovernorateService
    {
        Task<int> AddAsync(GovernorateT governorate);
        Task<List<GovernorateT>> GetListAsync(int? countryId, string governorateName = null);
        Task<GovernorateT> GetAsync(int id);
        Task<GovernorateT> GetAsync(string governorateName);
        Task<int> DeletetAsync(int governorateId);
        Task<int> UpdateAsync(GovernorateT governorate);
    }
}
