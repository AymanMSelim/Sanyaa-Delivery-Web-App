using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.ModelViews
{
    public class ClientModelView
    {
        public IEnumerable<ClientT> Clients { get; set; }
    }
}
