using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Application.DTO;
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

        public ClientService(IRepository<ClientT> clientRepository)
        {
            this.clientRepository = clientRepository;
        }
        public ClientDto GetAllClients()
        {
            //return new ClientDto{ Clients = clientRepository.GetAll() };
            return null;
        }

        public Task<ClientT> GetById(int id)
        {
            return clientRepository.Get(id);
        }

        public Task<List<ClientT>> GetByName(string name)
        {
            return clientRepository.Where(c => c.ClientName.Contains(name)).ToListAsync();
        }

        public Task<ClientT> GetByPhone(string phone)
        {
            return clientRepository.Where(c => c.CurrentPhone == phone).FirstOrDefaultAsync();
        }
    }
}
