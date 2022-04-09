using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<CountryT> repo;

        public CountryService(IRepository<CountryT> repo)
        {
            this.repo = repo;
        }
        public Task<List<CountryT>> GetList()
        {
            return repo.GetListAsync();
        }
    }
}
