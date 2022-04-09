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

        Task<int> Update(ClientT client);

        Task<List<AddressT>> GetAddressList(int clientId);

        Task<int> AddAddress(AddressT address);

        Task<int> UpdateAddress(AddressT address);

        Task<List<ClientPhonesT>> GetPhoneList(int clientId);

        Task<int> AddPhone(ClientPhonesT phone);

        Task<int> UpdatePhone(ClientPhonesT phone);

    }
}
