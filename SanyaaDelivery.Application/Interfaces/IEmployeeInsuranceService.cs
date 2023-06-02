using SanyaaDelivery.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IEmployeeInsuranceService
    {
        Task<EmployeeInsuranceIndexDto> GetIndexAsync(string employeeId);

    }
}
