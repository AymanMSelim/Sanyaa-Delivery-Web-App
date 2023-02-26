using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Enum;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class ClientPointService : IClientPointService 
    { 
        private readonly IRepository<ClientPointT> repo;
        private readonly IClientService clientService;

        public ClientPointService(IRepository<ClientPointT> repo, IClientService clientService)
        {
            this.repo = repo;
            this.clientService = clientService;
        }

        public async Task<int> AddAsync(ClientPointT clientPoint)
        {
            clientPoint.CreationDate = DateTime.Now;
            clientPoint.PointType = ((sbyte)ClientPointType.Add); //Add
            await repo.AddAsync(clientPoint);
            await clientService.AddPointAsync(clientPoint.ClientId, clientPoint.Points.Value);
            return await repo.SaveAsync();
        }

        public async Task<int> WithdrawAsync(ClientPointT clientPoint)
        {
            clientPoint.CreationDate = DateTime.Now;
            clientPoint.PointType = ((sbyte)ClientPointType.Withdraw); //Withdraw
            await repo.AddAsync(clientPoint);
            await clientService.WidthrawPointAsync(clientPoint.ClientId, clientPoint.Points.Value);
            return await repo.SaveAsync();
        }


        public async Task<int> DeletetAsync(int id)
        {
            await repo.DeleteAsync(id);
            return await repo.SaveAsync();
        }


        public Task<ClientPointT> GetAsync(int id)
        {
            return repo.GetAsync(id);
        }

        public Task<List<ClientPointT>> GetListAsync(int clientId, ClientPointType? type = null)
        {
            var query = repo.Where(d => d.ClientId == clientId);
            if(type != null)
            {
                query = query.Where(d => d.PointType == ((sbyte)type));
            }
            return query.ToListAsync();  
        }

        public Task<int> UpdateAsync(ClientPointT clientPoint)
        {
            repo.Update(clientPoint.ClientPointId, clientPoint);
            return repo.SaveAsync();
        }

        public async Task<int> GetClientPointAsync(int clientId)
        {
            var addedPoints = await repo.DbSet.Where(d => d.ClientId == clientId && d.PointType == ((sbyte)ClientPointType.Add))
                .SumAsync(d => d.Points.Value);
            var withdrawPoints = await repo.DbSet.Where(d => d.ClientId == clientId && d.PointType == ((sbyte)ClientPointType.Withdraw))
                .SumAsync(d => d.Points.Value);
            return addedPoints - withdrawPoints;
        }
    }
}
