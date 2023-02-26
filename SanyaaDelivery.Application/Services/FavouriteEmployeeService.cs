using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class FavouriteEmployeeService : IFavouriteEmployeeService
    {
        private readonly IRepository<FavouriteEmployeeT> repo;

        public FavouriteEmployeeService(IRepository<FavouriteEmployeeT> repo)
        {
            this.repo = repo;
        }

        public async Task<int> AddAsync(FavouriteEmployeeT favouriteEmployee)
        {
            await repo.AddAsync(favouriteEmployee);
            return await repo.SaveAsync();
        }

        public async Task<int> DeletetAsync(int id)
        {
            await repo.DeleteAsync(id);
            return await repo.SaveAsync();
        }

        public async Task<int> FavouriteSwitch(int clientId, string employeeId)
        {
            var employeeFavouriteList = await GetListAsync(clientId);
            if (employeeFavouriteList.HasItem())
            {
                var selectedFavouriteEmployee = employeeFavouriteList.FirstOrDefault(d => d.EmployeeId == employeeId);
                if (selectedFavouriteEmployee.IsNotNull())
                {
                    return await DeletetAsync(selectedFavouriteEmployee.FavouriteEmployeeId);
                }
            }
            var newFavouriteEmployee = new FavouriteEmployeeT
            {
                ClientId = clientId,
                EmployeeId = employeeId
            };
            return await AddAsync(newFavouriteEmployee);
        }

        public Task<FavouriteEmployeeT> GetAsync(int id, bool includeEmployee = false)
        {
            var query = repo.DbSet.AsQueryable();
            query = query.Where(d => d.FavouriteEmployeeId == id);
            if (includeEmployee)
            {
                query = query.Include(d => d.Employee);
            }
            return query.FirstOrDefaultAsync();
        }

        public Task<List<FavouriteEmployeeT>> GetListAsync(int? clientId, bool includeEmployee = false)
        {
            var query = repo.DbSet.AsQueryable();
            if (includeEmployee)
            {
                query = query.Include(d => d.Employee);
            }
            if (clientId.HasValue)
            {
                query = query.Where(d => d.ClientId == clientId);
            }
            return query.ToListAsync();
        }

        public Task<int> UpdateAsync(FavouriteEmployeeT favouriteEmployee)
        {
            repo.Update(favouriteEmployee.FavouriteEmployeeId, favouriteEmployee);
            return repo.SaveAsync();
        }
    }
}
