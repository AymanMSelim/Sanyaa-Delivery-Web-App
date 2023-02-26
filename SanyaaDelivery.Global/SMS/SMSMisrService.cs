using App.Global.DTOs.SMS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Global.SMS
{
    public class SMSMisrService : ISMSService
    {
        static RestAPI.APIService APIService = new RestAPI.APIService("https://smsmisr.com/");
        private static string _username = "";
        private static string _password = "";
        private static string _sender = "";

        public static void SetParameters(string username, string password, string sender)
        {
            _username = username;
            _password = password;
            _sender = sender;
        }

        public static Task<bool> SendSmsAsync(string languange, string mobile, string message)
        {

            //WhatsApp.WhatsAppService whatsApp = new WhatsApp.WhatsAppService();
            //whatsApp.SendOTP(mobile, message);
            //return null;
            SMSMisrService sMSMisrService = new SMSMisrService();
            return sMSMisrService.SendSms(languange, mobile, message);
        }

        public async Task<bool> SendSms(string languange, string mobile, string message)
        {
            bool response;
            var result = await APIService.PostAsync<SMSMisrDto>($"/api/webapi/?username={_username}&password={_password}&language=2&sender={_sender}&mobile=2{mobile}&message={message}", null);
            switch (result.Code)
            {
                case "1901":
                    response = true;
                    break;
                default:
                    response = false;
                    break;
            }
            return response;
        }
    }
}
