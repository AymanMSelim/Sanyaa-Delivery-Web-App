using App.Global.DTOs;
using App.Global.Models.Fawry;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IFawryService
    {
        Task<Result<FawryRefNumberResponse>> SendAllUnpaidRequestAsync(string employeeId, List<RequestT> requestList = null, bool ignoreValidFawryRequest = false);
        Task<Result<App.Global.Models.Fawry.FawryRefNumberResponse>> SendUnpaidRequestAsync(GenerateRefNumberForRequestDto model);
        Task<Result<List<App.Global.Models.Fawry.FawryRefNumberResponse>>> SendAllUnpaidRequestAsync(bool ignoreValidFawryRequest = false);
        Task<App.Global.Models.Fawry.FawryRefNumberResponse> SendRequest(int requestId);
        List<App.Global.Models.Fawry.FawryChargeItem> ConvertRequestToChargeItem(List<RequestT> requestList);
        App.Global.Models.Fawry.FawryRequest PrepareFawryRequest(List<App.Global.Models.Fawry.FawryChargeItem> fawtyChargeItems, EmployeeT employee);
        Task UpdateStatusTask();
        Task UpdateEmployeeValidChargeAsync(string employeeId);
        Task CheckUpdateFawryChargeAsync(FawryChargeT fawryCharge);
        Task<int> CallbackNotification(FawryNotificationCallback model);
    }
}
