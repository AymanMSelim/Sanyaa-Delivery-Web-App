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

        public async Task<bool> SendSmsAsync(string mobile, string message, string lang = null)
        {
            var request = new SMSMisrSMSRequestDto
            {
                Environment = 1,
                Mobile = mobile,
                Password = _password,
                Sender = _sender,
                Message = message,
                Username = _username
            };
            bool response;
            var result = await APIService.PostAsync<SMSMisrDto>(
                $@"/api/sms", request);
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

        public async Task<bool> SendOTPAsync(string mobile, string otp)
        {
            var request = new SMSMisrOTPRequestDto
            {
                Environment = 1,
                Mobile = mobile,
                Otp = otp,
                Password = _password,
                Sender = _sender,
                Template = "0f9217c9d760c1c0ed47b8afb5425708da7d98729016a8accfc14f9cc8d1ba83",
                Username = _username
            };
            bool response;
            var result = await APIService.PostAsync<SMSMisrDto>($@"/api/otp", request);
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
