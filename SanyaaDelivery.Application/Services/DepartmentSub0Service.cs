using App.Global.DTOs;
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
    public class DepartmentSub0Service : IDeparmentSub0Service
    {
        private readonly IRepository<DepartmentSub0T> departmentSub0Repository;
        private readonly IRepository<DepartmentSub1T> departmentSub1Repository;

        public DepartmentSub0Service(IRepository<DepartmentSub0T> departmentSub0Repository, IRepository<DepartmentSub1T> departmentSub1Repository)
        {
            this.departmentSub0Repository = departmentSub0Repository;
            this.departmentSub1Repository = departmentSub1Repository;
        }

        public async Task<int> AddAsync(DepartmentSub0T departmentSub0)
        {
            await departmentSub0Repository.AddAsync(departmentSub0);
            return await departmentSub0Repository.SaveAsync();
        }

        public Task<List<ValueWithIdDto>> FilerAsync(int departmentId)
        {
            return departmentSub0Repository.Where(d => d.DepartmentId == departmentId)
                .Select(d => new ValueWithIdDto
                {
                    Id = d.DepartmentSub0Id.ToString(),
                    Value = d.DepartmentSub0
                }).ToListAsync();
        }

        public Task<DepartmentSub0T> GetAsync(int departmentId)
        {
            return departmentSub0Repository.Where(d => d.DepartmentSub0Id == departmentId)
                .FirstOrDefaultAsync();
        }

        public  async Task<List<DepartmentSub0T>> GetListAsync(int? departmentId = null, string departmentSub0Name = null)
        {
            var query = departmentSub1Repository.DbSet.AsQueryable();
            if (departmentId.HasValue)
            {
                query = query.Where(d => d.DepartmentSub0Navigation.DepartmentId == departmentId.Value);
            }
            if(string.IsNullOrEmpty(departmentSub0Name) == false)
            {
                query = query.Where(d => d.DepartmentSub0.Contains(departmentSub0Name));
            }
            query = query.OrderByDescending(d => d.ServiceT.Count);
            var list = await query.Select(d => d.DepartmentSub0Navigation).ToListAsync();
            return list.Distinct().ToList();
        }

        public Task<List<DepartmentSub0T>> GetListAsync(string departmentName)
        {
            return departmentSub0Repository.Where(d => d.DepartmentSub0.Contains(departmentName))
                .ToListAsync();
        }

        public Task<bool> IsExistAsync(string departmentName)
        {
            return departmentSub0Repository.Where(d => d.DepartmentSub0.ToLower() == departmentName.ToLower()).AnyAsync();
        }

        public Task<int> UpdateAsync(DepartmentSub0T departmentSub0)
        {
            departmentSub0Repository.Update(departmentSub0.DepartmentSub0Id, departmentSub0);
            return departmentSub0Repository.SaveAsync();
        }

        Task<List<ValueWithIdDto>> IDeparmentSub0Service.FilerAsync(string departmentName)
        {
            return departmentSub0Repository.Where(d => d.DepartmentSub0.Contains(departmentName))
                .Select(d => new ValueWithIdDto
                {
                    Id = d.DepartmentSub0Id.ToString(),
                    Value = d.DepartmentSub0
                }).ToListAsync();
        }
    }
}
