using SanyaaDelivery.Domain.Enum;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IClientPointService
    {
        Task<int> AddAsync(ClientPointT clientPoint);
        Task<int> WithdrawAsync(ClientPointT clientPoint);
        Task<List<ClientPointT>> GetListAsync(int clientId, ClientPointType type);
        Task<ClientPointT> GetAsync(int id);
        Task<int> DeletetAsync(int id);
        Task<int> UpdateAsync(ClientPointT clientPoint);
        Task<int> GetClientPointAsync(int clientId);
    }
}
