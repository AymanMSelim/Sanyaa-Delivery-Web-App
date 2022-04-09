using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SanyaaDelivery.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<ClientT> clientRepository;
        private readonly IRepository<AddressT> addressRepository;
        private readonly IRepository<ClientPhonesT> phoneRepository;

        public ClientService(IRepository<ClientT> clientRepository, IRepository<AddressT> addressRepository, IRepository<ClientPhonesT> phoneRepository)
        {
            this.clientRepository = clientRepository;
            this.addressRepository = addressRepository;
            this.phoneRepository = phoneRepository;
        }
        public ClientDto GetAllClients()
        {
            //return new ClientDto{ Clients = clientRepository.GetAll() };
            return null;
        }

        public Task<ClientT> GetById(int id)
        {
            return clientRepository.GetAsync(id);
        }

        public Task<List<ClientT>> GetByName(string name)
        {
            return clientRepository.Where(c => c.ClientName.Contains(name)).ToListAsync();
        }

        public Task<ClientT> GetByPhone(string phone)
        {
            return clientRepository.Where(c => c.ClientPhonesT.Where(p => p.ClientPhone == phone).Any()).FirstOrDefaultAsync();
        }

        public async Task<int> Add(ClientT client)
        {
            await clientRepository.AddAsync(client);
            return await clientRepository.SaveAsync();
        }

        public Task<int> Update(ClientT client)
        {
            clientRepository.Update(client.ClientId, client);
            return clientRepository.SaveAsync();
        }

        public async Task<int> AddAddress(AddressT address)
        {
            await addressRepository.AddAsync(address);
            return await addressRepository.SaveAsync();
        }

        public Task<int> UpdateAddress(AddressT address)
        {
            addressRepository.Update(address.AddressId, address);
            return addressRepository.SaveAsync();
        }

        public async Task<int> AddPhone(ClientPhonesT phone)
        {
            await phoneRepository.AddAsync(phone);
            return await phoneRepository.SaveAsync();
        }

        public Task<int> UpdatePhone(ClientPhonesT phone)
        {
            phoneRepository.Update(phone.ClientPhoneId, phone);
            return phoneRepository.SaveAsync();
        }

        public Task<List<AddressT>> GetAddressList(int clientId)
        {
            return addressRepository.Where(d => d.ClientId == clientId).ToListAsync();
        }

        public Task<List<ClientPhonesT>> GetPhoneList(int clientId)
        {
            return phoneRepository.Where(d => d.ClientId == clientId).ToListAsync();
        }
    }
}
