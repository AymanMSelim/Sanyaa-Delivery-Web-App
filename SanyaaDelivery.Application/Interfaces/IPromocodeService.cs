using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IPromocodeService
    {
        Task<int> AddAsync(PromocodeT promocode);
        Task<PromocodeT> GetAsync(string promocode, bool includeDetails = false, bool getActiveOnly = true);
        Task<PromocodeT> GetAsync(int promocodeId, bool includeDetails = false, bool getActiveOnly = true);
        Task<bool> IsAvailableForClientAsync(int promocodeId, int clientId);
        Task<int> UpdateAsync(PromocodeT promocode);
        Task<int> DecreaseUsageCountAsync(PromocodeT promocode, int value = 1);
    }
}
