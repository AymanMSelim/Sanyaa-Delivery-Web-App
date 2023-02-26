using System;
using System.Collections.Generic;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;


namespace App.Global.WhatsApp
{
    public class WhatsAppService
    {
        string accountSid;
        string authToken;
        public WhatsAppService()
        {
            accountSid = "AC654dee8f4dff432f26bce20bcc3276be";//Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            authToken = "c3f3161a5655ea7eb5ced88509fb43bf";//Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
        }

        public string SendOTP(string number, string otpmsg)
        {

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: otpmsg,
                from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
                to: new Twilio.Types.PhoneNumber($"whatsapp:+2{number}")
            );

            return message.Sid;
        }

        public void SendRequestDetails(string number, string requestDetails)
        {

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: requestDetails,
                from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
                to: new Twilio.Types.PhoneNumber($"whatsapp:+2{number}")
            );

            Console.WriteLine(message.Sid);
        }
    }
}
