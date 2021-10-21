using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ICleaningSubscriberService
    {
        Task<List<ClientT>> GetSubscribers();

        Task<CleaningSubscribersT> GetInfo(int clientId);

        Task<int> AddSubscribe(int clientId, int package, int userId);

        Task<int> EditPackage(int clientId, int newPackage);

    }
}
