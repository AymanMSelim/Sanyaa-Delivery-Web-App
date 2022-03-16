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

        public EmployeeService(IRepository<EmployeeT> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
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

        public List<EmployeeT> GetByDepartment(string departmentName)
        {
            return null;
            //return employeeRepository.Where(d => d.DepartmentEmployeeT == null).Select(d => d.DepartmentEmployeeT.Select(d => d.Employee));
        }

        public Task<int> Add(EmployeeT employee)
        {
            employeeRepository.AddAsync(employee);
            return employeeRepository.SaveAsync();
        }
        public EmployeeDto GetCustomInfo(string id)
        {
            throw new NotImplementedException();
        }
    }
}
