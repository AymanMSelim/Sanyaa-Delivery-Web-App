using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.DTOs
{
    public class SubscribeDto
    {
        public int ClientId { get; set; }
        public int Package { get; set; }
        public int SystemUserId { get; set; }
    }
}
