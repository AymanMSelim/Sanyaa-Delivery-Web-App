using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanyaaDelivery.Application.Services
{
    public class EmpDeptService : IEmpDeptService
    {
        private readonly IRepository<DepartmentEmployeeT> departmentRepository;

        public EmpDeptService(IRepository<DepartmentEmployeeT> empDeptRepository)
        {
            this.departmentRepository = empDeptRepository;
        }

        public List<EmployeeT> GetEmployees(string department)
        {
            return departmentRepository.Where(d => d.DepartmentName == department).Select(d => d.Employee).ToList();
        }
    }
}
