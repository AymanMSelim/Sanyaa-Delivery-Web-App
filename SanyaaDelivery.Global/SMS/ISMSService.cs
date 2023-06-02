using App.Global.DTOs.SMS;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Global.SMS
{
    public interface ISMSService
    {
        Task<bool> SendSmsAsync(string mobile, string message, string languange = null);
        Task<bool> SendOTPAsync(string mobile, string otp);
    }
}
