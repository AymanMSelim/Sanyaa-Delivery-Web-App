using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SanyaaDelivery.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(SystemUserT systemUser)
        {
            var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.NameId, systemUser.SystemUserId.ToString()),
               new Claim(JwtRegisteredClaimNames.Name, systemUser.SystemUserUsername)
           };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            
            var tokenDiscreptor = new SecurityTokenDescriptor();
            tokenDiscreptor.SigningCredentials = creds;
            tokenDiscreptor.Subject = new ClaimsIdentity(claims);
            tokenDiscreptor.Expires = DateTime.Now.AddDays(1);
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDiscreptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}
