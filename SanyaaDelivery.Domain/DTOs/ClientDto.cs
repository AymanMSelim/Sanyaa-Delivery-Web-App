using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class ClientDto
    {
        public IEnumerable<ClientT> Clients { get; set; }
    }
}
