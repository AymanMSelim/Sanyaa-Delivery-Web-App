using SanyaaDelivery.Application.ModelViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IClientService
    {
        ClientModelView GetAllClients();
    }
}
