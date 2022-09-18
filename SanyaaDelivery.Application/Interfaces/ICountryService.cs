using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryT>> GetListAsync(string countryName = null);
        Task<CountryT> GetAsync(int countryId);
        Task<int> DeletetAsync(int countryId);
        Task<int> AddAsync(CountryT country);
        Task<int> UpdateAsync(CountryT country);
    }
}
