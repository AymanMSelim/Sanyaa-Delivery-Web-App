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
        Task<Result<SystemUserT>> SystemUserLoginAsync(string userName, string password);
        Task<Result<EmployeeLoginResponseDto>> LoginEmployeeAsync(LoginEmployeeDto model);
        Task<Result<SystemUserDto>> LoginClientAsync(string phone, string password = null, string otp = null);
        Task<Result<OTPCodeDto>> RequestOTPForLoginAsync(string clientPhone);
    }
}
