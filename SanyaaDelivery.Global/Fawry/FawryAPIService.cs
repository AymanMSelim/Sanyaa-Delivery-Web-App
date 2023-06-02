using App.Global.ExtensionMethods;
using App.Global.Interfaces;
using App.Global.SMS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Global.Fawry
{
    public class FawryAPIService : IFawryAPIService
    {
        private static string _securityCode;
        private bool sendSMS;
        static RestAPI.APIService fawryApi; 
        static FawryAPIService()
        {
        }

        private Models.Fawry.FawryRequest _fawryRequest;
        private readonly ISMSService smsService;

        public FawryAPIService(ISMSService smsService)
        {
            this.smsService = smsService;
        }
        public void InitialAPI(string apiUrl, bool sendSMS = false)
        {
            if (fawryApi == null)
            {
                fawryApi = new RestAPI.APIService(apiUrl);
            }
            this.sendSMS = sendSMS;
        }

        public FawryAPIService(Models.Fawry.FawryRequest fawryRequest, string securityCode)
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
            _fawryRequest.Signature = SignRequest();
            var result = await fawryApi.PostAsync<Models.Fawry.FawryRefNumberResponse>("/ECommerceWeb/Fawry/payments/charge", _fawryRequest);
            if  (sendSMS && result.IsNotNull() && !string.IsNullOrEmpty(result.ReferenceNumber))
            {
                string message = $"طلبك #{result.ReferenceNumber} معلق يرجى دفع  {_fawryRequest.Amount} ج للدفع اختار خدمه الدفع برقم فوري باي أو بكود الخدمة 788";
                await smsService.SendSmsAsync(_fawryRequest.CustomerMobile, message);
            }
            return result;
        }

        public async Task<Models.Fawry.FawryStatusResponse> GetStatusAsync(int systemId, string marchantCode, string securityCode)
        {
            string signature = Encreption.Hashing.ComputeSha256Hash(marchantCode + systemId + securityCode);
            var url = $"/ECommerceWeb/Fawry/payments/status?merchantCode={marchantCode}&merchantRefNumber={systemId}&signature={signature}";
            return await fawryApi.GetResponseAsync<Models.Fawry.FawryStatusResponse>(url);
        }

        public string SignRequest()
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
