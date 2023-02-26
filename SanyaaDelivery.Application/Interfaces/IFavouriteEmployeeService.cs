using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IFavouriteEmployeeService
    {
        Task<int> AddAsync(FavouriteEmployeeT favouriteEmployee);
        Task<List<FavouriteEmployeeT>> GetListAsync(int? clientId = null, bool includeEmployee = false);
        Task<FavouriteEmployeeT> GetAsync(int id, bool includeEmployee = false);
        Task<int> DeletetAsync(int id);
        Task<int> UpdateAsync(FavouriteEmployeeT favouriteEmployee);
        Task<int> FavouriteSwitch(int clientId, string employeeId);
    }
}
