using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Application.ModelViews;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<ClientT> clientRepository;

        public ClientService(IRepository<ClientT> clientRepository)
        {
            this.clientRepository = clientRepository;
        }
        public ClientModelView GetAllClients()
        {
            return new ClientModelView{ Clients = clientRepository.GetAll() };
        }
    }
}
