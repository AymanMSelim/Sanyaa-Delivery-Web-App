using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.DTO;
using SanyaaDelivery.Application.DTOs;
using SanyaaDelivery.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class AccountController : APIBaseController
    {
        private readonly ISystemUserService systemUserService;
        private readonly ITokenService tokenService;
        private readonly IAccountService accountService;
        private readonly IAccountRoleService accountRoleService;
        private readonly IClientService clientService;

        public AccountController(ISystemUserService systemUserService, ITokenService tokenService, IAccountService accountService, IAccountRoleService accountRoleService, IClientService clientService)
        {
            this.systemUserService = systemUserService;
            this.tokenService = tokenService;
            this.accountService = accountService;
            this.accountRoleService = accountRoleService;
            this.clientService = clientService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<SystemUserDto>> Login(UserLoginDto user)
        {
            if (ModelState.IsValid)
            {
                int id = 0;
                if(user.AccountType == 1)
                {
                    var userModel = await systemUserService.Get(user.Username);
                    id = userModel.SystemUserId;
                    
                }
                else if(user.AccountType == 3)
                {
                    var clientModel = await clientService.GetByPhone(user.Username);
                    id = clientModel.ClientId;
                }

                var account = await accountService.Get(user.AccountType.Value, id);
                var acountRoles = await accountRoleService.GetList(account.AccountId, true);


                //var hash = new HMACSHA512();
                //var password = hash.ComputeHash(Encoding.UTF8.GetBytes(user.Password));

                //var systemUser =  await systemUserService.Get(user.Username);
                //if(systemUser == null)
                //{
                //    return NotFound(new { Message = $"This user {user.Username} not found" });
                //}
                //if(systemUser.SystemUserPass != "2642264")
                //{
                //    return 
                //        NotFound(new { Message = $"This password for user {user.Username} not matched"});
                //}
                return Ok(
                    new SystemUserDto 
                    { 
                        Username = user.Username,
                        Token = tokenService.CreateToken(id.ToString(), user.Username, acountRoles)
                    });
            }
            else
            {
                return BadRequest(new { Message = "Empty username or password" });
            }
        }
    }
}
