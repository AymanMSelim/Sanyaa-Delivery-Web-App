using System;
using System.Collections.Generic;
using System.Text;

namespace App.Global.DTOs.SMS
{
    
    public class SMSMisrDto
    {
        public string Code { get; set; }
        public string SmsId { get; set; }
        public int Vodafone { get; set; }
        public int Etisalat { get; set; }
        public int Orange { get; set; }
        public int We { get; set; }
        public string Language { get; set; }
        public int Vodafone_Cost { get; set; }
        public int Etisalat_Cost { get; set; }
        public int Orange_Cost { get; set; }
        public int We_Cost { get; set; }
    }
}
