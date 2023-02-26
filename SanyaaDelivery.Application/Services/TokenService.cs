using App.Global.ExtensionMethods;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IRepository<TokenT> tokenRepository;
        private readonly IRoleService roleService;

        public TokenService(IConfiguration config, IRepository<TokenT> tokenRepository, IRoleService roleService)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            this.tokenRepository = tokenRepository;
            this.roleService = roleService;
        }

        public async Task<int> AddAsync(TokenT token)
        {
            await tokenRepository.AddAsync(token);
            return await tokenRepository.SaveAsync();
        }

        public string CreateToken(AccountT account)
        {
            int systemUserId = 1;
            if(account.AccountTypeId == GeneralSetting.CustomerAccountTypeId)
            {
                systemUserId = GeneralSetting.CustomerAppSystemUserId;
            }
            else if(account.AccountTypeId == GeneralSetting.SystemUserAccountTypeId)
            {
                systemUserId = int.Parse(account.AccountReferenceId);
            }
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.AccountId.ToString()),
                new Claim("AccountType", account.AccountTypeId.ToString()),
                new Claim("ReferenceId", account.AccountReferenceId),
                new Claim("SystemUserId", systemUserId.ToString()),
                new Claim(ClaimTypes.Name, account.AccountUsername)
            };
            foreach (var role in account.AccountRoleT)
            {
                if (role.Role.IsNull())
                {
                    role.Role = GeneralSetting.RoleList.Find(d => d.RoleId == role.RoleId);
                }
                claims.Add(new Claim(ClaimTypes.Role, role.Role.RoleName));
            }
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDiscreptor = new SecurityTokenDescriptor
            {
                SigningCredentials = creds,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(GeneralSetting.TokenExpireInDays)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDiscreptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
