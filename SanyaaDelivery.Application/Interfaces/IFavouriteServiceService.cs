using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IFavouriteServiceService
    {
        Task<int> AddAsync(FavouriteServiceT favouriteService);
        Task<List<FavouriteServiceT>> GetListAsync(int? clientId = null, bool includeService = false);
        Task<FavouriteServiceT> GetAsync(int id, bool includeService = false);
        Task<int> DeletetAsync(int id);
        Task<int> UpdateAsync(FavouriteServiceT favouriteService);
        Task<int> FavouriteSwitch(int clientId, int serviceId);
    }
}
