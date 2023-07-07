using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class SendMulticastNotificationDto
    {
        public int AccountType { get; set; } 
        public List<string> IdList { get; set; } 
        public string Title { get; set; } 
        public string Body { get; set; } 
        public string Image { get; set; } 
        public string Link { get; set; } 
    }
}
