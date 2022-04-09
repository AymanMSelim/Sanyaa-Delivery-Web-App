using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class EmpDeptService : IEmpDeptService
    {
        private readonly IRepository<DepartmentEmployeeT> departmentRepository;

        public EmpDeptService(IRepository<DepartmentEmployeeT> empDeptRepository)
        {
            this.departmentRepository = empDeptRepository;
        }

        public async Task<int> AddAsync(DepartmentEmployeeT departmentEmployee)
        {
            await departmentRepository.AddAsync(departmentEmployee);
            return await departmentRepository.SaveAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await departmentRepository.DeleteAsync(id);
            return await departmentRepository.SaveAsync();
        }

        public List<EmployeeT> GetEmployees(string department)
        {
            return departmentRepository.Where(d => d.DepartmentName == department).Select(d => d.Employee).ToList();
        }

        public List<EmployeeT> GetEmployees(int departmentId)
        {
            return departmentRepository.Where(d => d.DepartmentId == departmentId).Select(d => d.Employee).ToList();
        }
    }
}
