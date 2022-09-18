using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IFawryService
    {
        Task<App.Global.Models.Fawry.FawryRefNumberResponse> SendAllUnpaidRequestAsync(string employeeId);
        Task<App.Global.Models.Fawry.FawryRefNumberResponse> SendRequest(int requestId);
        List<App.Global.Models.Fawry.FawryChargeItem> ConvertRequestToChargeItem(List<RequestT> requestList);
        App.Global.Models.Fawry.FawryRequest PrepareFawryRequest(List<App.Global.Models.Fawry.FawryChargeItem> fawtyChargeItems, EmployeeT employee);
    }
}
