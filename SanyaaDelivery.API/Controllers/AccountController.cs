using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.DTO;
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
        public ActionResult Login(UserLoginDto user)
        {
            if (ModelState.IsValid)
            {
                var hash = new HMACSHA512();
                var password = hash.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                return Ok();
            }
            else
            {
                return BadRequest(new { Message = "Empty username or password" });
            }
        }
    }
}
