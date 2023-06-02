using System;
using System.Collections.Generic;
using System.Text;

namespace App.Global.DTOs.SMS
{
    public class SMSMisrOTPRequestDto
    {
        public int Environment { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Sender { get; set; }

        public string Mobile { get; set; }

        public string Template { get; set; }

        public string Otp { get; set; }
    }

    public class SMSMisrSMSRequestDto
    {
        public int Environment { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Sender { get; set; }

        public string Mobile { get; set; }

        public string Message { get; set; }

    }
}
