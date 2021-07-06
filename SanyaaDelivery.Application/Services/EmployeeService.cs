﻿using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Application.DTO;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return employeeRepository.Get(id);
        }

        public List<EmployeeT> GetByDepartment(string departmentName)
        {
            return null;
            //return employeeRepository.Where(d => d.DepartmentEmployeeT == null).Select(d => d.DepartmentEmployeeT.Select(d => d.Employee));
        }

        public EmployeeDto GetCustomInfo(string id)
        {
            throw new NotImplementedException();
        }
    }
}