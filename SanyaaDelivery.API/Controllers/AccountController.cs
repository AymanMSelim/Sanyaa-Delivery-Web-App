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

        public AccountController(ISystemUserService systemUserService, ITokenService tokenService)
        {
            this.systemUserService = systemUserService;
            this.tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<SystemUserDto>> Login(UserLoginDto user)
        {
            if (ModelState.IsValid)
            {
                var hash = new HMACSHA512();
                var password = hash.ComputeHash(Encoding.UTF8.GetBytes(user.Password));

                var systemUser =  await systemUserService.Get(user.Username);
                if(systemUser == null)
                {
                    return NotFound(new { Message = $"This user {user.Username} not found" });
                }
                if(systemUser.SystemUserPass != "2642264")
                {
                    return 
                        NotFound(new { Message = $"This password for user {user.Username} not matched"});
                }
                return Ok(
                    new SystemUserDto 
                    { 
                        Username = user.Username,
                        Token = tokenService.CreateToken(systemUser)
                    });
            }
            else
            {
                return BadRequest(new { Message = "Empty username or password" });
            }
        }
    }
}
