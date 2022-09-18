using App.Global.DTOs;
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
    public class DepartmentSub1Service : IDeparmentSub1Service
    {
        private readonly IRepository<DepartmentSub1T> departmentSub1Repository;

        public DepartmentSub1Service(IRepository<DepartmentSub1T> departmentSub1Repository)
        {
            this.departmentSub1Repository = departmentSub1Repository;
        }

        public async Task<int> AddAsync(DepartmentSub1T departmentSub1)
        {
            await departmentSub1Repository.AddAsync(departmentSub1);
            return await departmentSub1Repository.SaveAsync();
        }

        public Task<List<ValueWithIdDto>> FilerAsync(string departmentName, string departmentSub0)
        {
            return departmentSub1Repository
                .Where(d => d.DepartmentName.Contains(departmentName) && d.DepartmentSub0.Contains(departmentSub0))
                .Select(d => new ValueWithIdDto
                {
                    Id = d.DepartmentId.ToString(),
                    Value = d.DepartmentSub1
                }).ToListAsync();
        }

        public Task<List<ValueWithIdDto>> FilerAsync(int departmentSub0Id)
        {
            return departmentSub1Repository
                .Where(d => d.DepartmentSub0Id == departmentSub0Id)
                .Select(d => new ValueWithIdDto
                {
                    Id = d.DepartmentId.ToString(),
                    Value = d.DepartmentSub1
                }).ToListAsync();
        }

        public Task<DepartmentSub1T> GetAsync(int departmenSub1tId)
        {
            return departmentSub1Repository.GetAsync(departmenSub1tId);
        }

        public Task<List<DepartmentSub1T>> GetListAsync()
        {
            return departmentSub1Repository.GetListAsync();
        }

        public Task<List<DepartmentSub1T>> GetListAsync(string departmentName, string departmentSub0)
        {
            return departmentSub1Repository
                .Where(d => d.DepartmentName.Contains(departmentName) && d.DepartmentSub0.Contains(departmentSub0))
                .ToListAsync();
        }

        public Task<List<DepartmentSub1T>> GetListAsync(int? mainDepartmentId, int? departmentSub0Id, string departmentSub1Name)
        {
            var query = departmentSub1Repository.DbSet.AsQueryable();
            if (mainDepartmentId.HasValue)
            {
                query = query.Where(d => d.DepartmentSub0Navigation.DepartmentId == mainDepartmentId);
            }
            if (departmentSub0Id.HasValue)
            {
                query = query.Where(d => d.DepartmentSub0Id == departmentSub0Id);
            }
            if (departmentSub1Name.IsNotNull())
            {
                query = query.Where(d => d.DepartmentSub1.Contains(departmentSub1Name));
            }
            return query.ToListAsync();
        }

        public Task<bool> IsExistAsync(string departmentName)
        {
            return departmentSub1Repository.Where(d => d.DepartmentName.ToLower() == departmentName.ToLower()).AnyAsync();
        }

        public Task<int> UpdateAsync(DepartmentSub1T departmentSub1)
        {
             departmentSub1Repository.Update(departmentSub1.DepartmentId, departmentSub1);
            return  departmentSub1Repository.SaveAsync();
        }
    }
}
