using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Global.Interfaces
{
    public interface IFawryAPIService
    {
        void SetFawryRequest(Models.Fawry.FawryRequest fawryRequest, string securityCode);
        string SignRequest();
        void InitialAPI(string apiUrl, bool sendSMS = false);
        Task<Models.Fawry.FawryRefNumberResponse> GetRefNumberAsync();
        Task<Models.Fawry.FawryStatusResponse> GetStatusAsync(int systemId, string marchantCode, string securityCode);
    }
}
