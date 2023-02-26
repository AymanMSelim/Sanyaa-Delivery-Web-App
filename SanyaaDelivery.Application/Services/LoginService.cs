using App.Global.DTOs;
using App.Global.ExtensionMethods;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly ISystemUserService systemUserService;

        public LoginService(ISystemUserService systemUserService)
        {
            this.systemUserService = systemUserService;
        }
        public async Task<Result<SystemUserT>> SystemUserLogin(string userName, string password)
        {
            var systemUser = await systemUserService.Get(userName);
            if (systemUser.IsNull())
            {
                return ResultFactory<SystemUserT>.CreateNotFoundResponse("This user not found");
            }
            if(systemUser.SystemUserPass != password)
            {
                return ResultFactory<SystemUserT>.CreateErrorResponseMessage("User or password incorrect");
            }
            return ResultFactory<SystemUserT>.CreateSuccessResponse(systemUser);
        }
    }
}
