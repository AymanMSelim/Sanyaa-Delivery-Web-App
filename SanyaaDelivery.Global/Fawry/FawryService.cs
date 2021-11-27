using App.Global.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Global.Fawry
{
    public class FawryService : IFawryService
    {
        private static string _securityCode;
        static RestAPI.APIService fawryApi; 
        static FawryService()
        {
            fawryApi = new RestAPI.APIService("https://www.atfawry.com");
        }

        private Models.Fawry.FawryRequest _fawryRequest;

        public FawryService()
        {

        }

        public FawryService(Models.Fawry.FawryRequest fawryRequest, string securityCode)
        {
            SetFawryRequest(fawryRequest, securityCode);
        }

        public void SetFawryRequest(Models.Fawry.FawryRequest fawryRequest, string securityCode)
        {
            _securityCode = securityCode;
            _fawryRequest = fawryRequest;
        }
        public async Task<Models.Fawry.FawryRefNumberResponse> GetRefNumberAsync()
        {
            _fawryRequest.Signature = SetSignature();
            return await fawryApi.PostAsync<Models.Fawry.FawryRefNumberResponse>("/ECommerceWeb/Fawry/payments/charge", _fawryRequest);
        }

        public string SetSignature()
        {
            return Encreption.Hashing.ComputeSha256Hash(
                _fawryRequest.MerchantCode +
                _fawryRequest.MerchantRefNum +
                _fawryRequest.CustomerProfileId +
                _fawryRequest.PaymentMethod +
                _fawryRequest.Amount +
                _securityCode
                );
        }
    }
}
