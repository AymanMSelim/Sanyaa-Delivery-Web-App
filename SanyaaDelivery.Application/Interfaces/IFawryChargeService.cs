using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IFawryChargeService
    {
        Task<FawryChargeT> GetAsync(int id, bool includeRequest = false);
        Task<bool> IsThisChargeExistAsync(string employeeId, decimal amount);
        Task<FawryChargeT> GetByReferenceNumberAsync(long referenceNumber, bool includeRequest = false);
        Task<int> AddAsync(FawryChargeT fawryCharge);
        Task<int> UpdateAsync(FawryChargeT fawryCharge);
        Task<int> UpdateStatus(int id, App.Global.Enums.FawryRequestStatus status);
        Task<int> AddChargeRequestAsync(FawryChargeRequestT fawryChargeRequest);
    }
}
