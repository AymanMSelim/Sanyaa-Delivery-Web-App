using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Application.ModelViews;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanyaaDelivery.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<EmployeeT> employeeRepository;

        public EmployeeService(IRepository<EmployeeT> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public EmployeeT Get(string id)
        {
            return employeeRepository.Get(id);
        }

        public List<EmployeeT> GetByDepartment(string departmentName)
        {
            return null;
            //return employeeRepository.Where(d => d.DepartmentEmployeeT == null).Select(d => d.DepartmentEmployeeT.Select(d => d.Employee));
        }

        public EmployeeModelView GetCustomInfo(string id)
        {
            throw new NotImplementedException();
        }
    }
}
