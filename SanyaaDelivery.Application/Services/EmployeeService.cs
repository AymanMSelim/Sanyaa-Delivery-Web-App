using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SanyaaDelivery.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly IRepository<DepartmentEmployeeT> employeeDepartmentRepository;
        private readonly IRepository<EmployeeWorkplacesT> employeeWorkplaceRepository;

        public EmployeeService(IRepository<EmployeeT> employeeRepository, IRepository<DepartmentEmployeeT> employeeDepartmentRepository, IRepository<EmployeeWorkplacesT> employeeWorkplaceRepository)
        {
            this.employeeRepository = employeeRepository;
            this.employeeDepartmentRepository = employeeDepartmentRepository;
            this.employeeWorkplaceRepository = employeeWorkplaceRepository;
        }

        public Task<EmployeeT> Get(string id)
        {
            return employeeRepository.GetAsync(id);
        }

        public Task<EmployeeT> GetWithBeancesAndTimetable(string id)
        {
            return employeeRepository
                .Where(d=> d.EmployeeId == id)
                .Include("EmployeeWorkplacesT")
                .Include("TimetableT")
                .Include("FiredStaffT")
                .FirstOrDefaultAsync();
        }

        public async Task<List<EmployeeT>> GetByDepartment(string departmentName)
        {
            return await employeeDepartmentRepository.Where(d => d.DepartmentName == departmentName).Select(d => d.Employee).ToListAsync();
        }

        public async Task<List<EmployeeT>> GetByDepartment(int departmentId)
        {
            return await employeeDepartmentRepository.Where(d => d.DepartmentId == departmentId).Select(d => d.Employee).ToListAsync();
        }

        public async Task<int> AddAsync(EmployeeT employee)
        {
            await employeeRepository.AddAsync(employee);
            return await employeeRepository.SaveAsync();
        }
        public EmployeeDto GetCustomInfo(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(EmployeeT employee)
        {
            employeeRepository.Update(employee.EmployeeId, employee);
            return employeeRepository.SaveAsync();
        }

        public async Task<int> AddDepartment(DepartmentEmployeeT departmentEmployee)
        {
            await employeeDepartmentRepository.AddAsync(departmentEmployee);
            return await employeeDepartmentRepository.SaveAsync();
        }

        public async Task<int> DeleteDepartment(int id)
        {
            await employeeDepartmentRepository.DeleteAsync(id);
            return await employeeDepartmentRepository.SaveAsync();
        }

        public async Task<int> AddBranch(EmployeeWorkplacesT employeeWorkplace)
        {
            await employeeWorkplaceRepository.AddAsync(employeeWorkplace);
            return await employeeWorkplaceRepository.SaveAsync();
        }

        public async Task<int> DeleteBranch(int id)
        {
            await employeeWorkplaceRepository.DeleteAsync(id);
            return await employeeWorkplaceRepository.SaveAsync();
        }

        
    }
}
