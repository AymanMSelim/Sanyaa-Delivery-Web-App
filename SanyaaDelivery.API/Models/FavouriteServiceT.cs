using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class FavouriteServiceT
    {
        public int FavouriteServiceId { get; set; }
        public int ServiceId { get; set; }
        public int ClientId { get; set; }
        public DateTime? CreationTime { get; set; }

        public ClientT Client { get; set; }
        public ServiceT Service { get; set; }
    }
}
