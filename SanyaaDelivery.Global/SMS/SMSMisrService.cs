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
        private static string _environment = "";

        public static void SetParameters(string username, string password, string sender, string environment)
        {
            _username = username;
            _password = password;
            _sender = sender;
            _environment = environment;
        }

        public static Task<bool> SendSmsAsync(string languange, string mobile, string message)
        {

            //WhatsApp.WhatsAppService whatsApp = new WhatsApp.WhatsAppService();
            //whatsApp.SendOTP(mobile, message);
            //return null;
            SMSMisrService sMSMisrService = new SMSMisrService();
            return sMSMisrService.SendSms(languange, mobile, message);
        }

        public static Task<bool> SendOTPAsync(string mobile, string otp)
        {
            SMSMisrService sMSMisrService = new SMSMisrService();
            return sMSMisrService.SendOTP(mobile, otp);
        }

        public async Task<bool> SendSms(string languange, string mobile, string message)
        {
            bool response;
            var result = await APIService.PostAsync<SMSMisrDto>(
                $@"/api/sms/?environment={_environment}&username={_username}&password={_password}&language=2&sender={_sender}&mobile=2{mobile}&message={message}", null);
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

        public async Task<bool> SendOTP(string mobile, string otp)
        {
            bool response;
            var result = await APIService.PostAsync<SMSMisrDto>(
                $@"/api/otp/?environment={_environment}&username={_username}&password={_password}&language=2&sender={_sender}&mobile=2{mobile}&template=5f0b0e60ee65179573bdad2f7e9da5d4f89547a44cb15ff5a134a5d595cffc47&otp={otp}", null);
            switch (result.Code)
            {
                case "4901":
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
