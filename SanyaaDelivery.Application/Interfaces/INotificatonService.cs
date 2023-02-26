using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface INotificatonService
    {
        void Send(string token, string title, string body);
    }
}
