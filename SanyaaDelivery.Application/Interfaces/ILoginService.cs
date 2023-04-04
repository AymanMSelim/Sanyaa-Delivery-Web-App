using App.Global.DTOs;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ILoginService
    {
        Task<Result<SystemUserT>> SystemUserLogin(string userName, string password);
        Task<Result<EmployeeLoginResponseDto>> LoginEmployee(LoginEmployeeDto model);
    }
}
