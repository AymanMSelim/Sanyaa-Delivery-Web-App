using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IClientService
    {
        ClientDto GetAllClients();

        Task<ClientT> GetById(int id);

        Task<ClientT> GetByPhone(string phone);

        Task<List<ClientT>> GetByName(string name);

        Task<int> Add(ClientT client);
    }
}
