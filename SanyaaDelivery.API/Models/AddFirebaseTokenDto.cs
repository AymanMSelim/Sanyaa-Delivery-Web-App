using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Models
{
    public class AddFirebaseTokenDto
    {
        public int AccountId { get; set; }
        public string Token { get; set; }
    }
}
