using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class SendFirebaseNotificationDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImageURL { get; set; }
    }
}
