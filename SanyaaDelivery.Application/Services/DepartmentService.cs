using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<DepartmentT> departmentRepository;

        public DepartmentService(IRepository<DepartmentT> departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task<int> AddAsync(DepartmentT department)
        {
            await departmentRepository.AddAsync(department);
            return await departmentRepository.SaveAsync();
        }

        public async Task<int> DeleteAsync(int departmentId)
        {
            var department = await departmentRepository.Where(d => d.DepartmentId == departmentId).FirstOrDefaultAsync();
            departmentRepository.DbContext.Entry(department).State = EntityState.Deleted;
            return await departmentRepository.SaveAsync();
        }

        public async Task<int> DeleteAsync(string departmentName)
        {
            var department = await departmentRepository.Where(d => d.DepartmentName == departmentName).FirstOrDefaultAsync();
            departmentRepository.DbContext.Entry(department).State = EntityState.Deleted;
            return await departmentRepository.SaveAsync();
        }

        public Task<DepartmentT> GetAsync(int departmentId)
        {
            return departmentRepository.Where(d => d.DepartmentId == departmentId).FirstOrDefaultAsync();
        }

        public Task<DepartmentT> GetAsync(string departmentName)
        {
            return departmentRepository.Where(d => d.DepartmentName == departmentName).FirstOrDefaultAsync();
        }

        public Task<List<DepartmentT>> GetListAsync()
        {
            return departmentRepository.GetListAsync();
        }

        public async Task<int> UpdateAsync(DepartmentT department)
        {
            var departmentNew = await departmentRepository.Where(d => d.DepartmentId == department.DepartmentId).FirstOrDefaultAsync();
            departmentRepository.DbContext.Entry(departmentNew).State = EntityState.Modified;
            return await departmentRepository.SaveAsync();
        }
    }
}
