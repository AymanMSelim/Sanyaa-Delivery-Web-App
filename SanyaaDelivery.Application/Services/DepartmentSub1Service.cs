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
    public class DepartmentSub1Service : IDeparmentSub1Service
    {
        private readonly IRepository<DepartmentSub1T> departmentSub1Repository;

        public DepartmentSub1Service(IRepository<DepartmentSub1T> departmentSub1Repository)
        {
            this.departmentSub1Repository = departmentSub1Repository;
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
                .Where(d => d.Department.DepartmentSub0Id == departmentSub0Id)
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
    }
}
