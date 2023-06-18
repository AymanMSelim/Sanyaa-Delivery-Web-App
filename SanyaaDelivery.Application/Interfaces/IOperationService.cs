using App.Global.DTOs;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IOperationService
    {
        Task<AppLandingIndexDto> SwitchActiveAsync(string employeeId);
        Task<int> SwitchOpenVacationAsync(string employeeId);
        Task<int> UpdatePreferredWorkingHourAsync(UpdatePreferredWorkingHourDto model);
        Task<AppLandingIndexDto> GetAppLandingIndexAsync(string employeeId);
        Task<BroadcastRequestDetailsDto> GetRequestDetailsAsync(BroadcastRequestActionDto model);
        Task<Result<BroadcastRequestT>> TakeBroadcastRequestAsync(BroadcastRequestActionDto model);
        Task<Result<BroadcastRequestT>> RejectBroadcastRequestAsync(BroadcastRequestActionDto model);
        Task<Result<object>> ApproveRequestAsync(BroadcastRequestActionDto model);
        Task<Result<RejectRequestT>> RejectRequestAsync(BroadcastRequestActionDto model);
        Task<Result<List<BroadcastRequestT>>> BroadcastAsync(int requestId);
        Task BroadcastTask();
    }
}
