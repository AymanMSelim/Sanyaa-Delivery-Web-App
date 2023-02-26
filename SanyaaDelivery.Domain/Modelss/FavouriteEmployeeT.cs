using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class FavouriteEmployeeT
    {
        public int FavouriteEmployeeId { get; set; }
        public string EmployeeId { get; set; }
        public int ClientId { get; set; }

        public ClientT Client { get; set; }
        public EmployeeT Employee { get; set; }
    }
}
