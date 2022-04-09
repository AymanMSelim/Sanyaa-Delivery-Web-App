using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class CleaningSubscriberService : ICleaningSubscriberService
    {

        private readonly IRepository<CleaningSubscribersT> subscribeRepository;

        public CleaningSubscriberService(IRepository<CleaningSubscribersT> subscribeRepository)
        {
            this.subscribeRepository = subscribeRepository;
        }

        public async Task<int> AddSubscribe(int clientId, int package, int userId)
        {
            await subscribeRepository.AddAsync(new CleaningSubscribersT
            {
                SubscribeDate = DateTime.Now,
                Package = package,
                ClientId = clientId,
                SystemUserId = userId
            }) ;
            return await subscribeRepository.SaveAsync();
        }

        public Task<int> EditPackage(int clientId, int newPackage)
        {
            var oldPackage = GetInfo(clientId).Result;
            oldPackage.Package = newPackage;
            subscribeRepository.Update(oldPackage.Id, oldPackage);
            return subscribeRepository.SaveAsync();
        }

        public Task<CleaningSubscribersT> GetInfo(int clientId)
        {
            return subscribeRepository.Where(s => s.ClientId == clientId).FirstOrDefaultAsync();
        }

        public Task<List<ClientT>> GetSubscribers()
        {
            return subscribeRepository.DbSet.Select(d => d.Client).ToListAsync();
        }
    }
}
