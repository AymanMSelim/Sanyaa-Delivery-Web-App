using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTO
{
    public class ClientDto
    {
        public IEnumerable<ClientT> Clients { get; set; }
    }
}
