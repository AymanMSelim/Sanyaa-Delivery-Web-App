using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.Models
{
    public class CleaningSubscribersT
    {
        public int Id { get; set; }

        public int Package { get; set; }

        public DateTime SubscribeDate { get; set; }

        public int ClientId { get; set; }

        public int SystemUserId { get; set; }

        public ClientT Client { get; set; }

        public SystemUserT SystemUser { get; set; }
    }
}
