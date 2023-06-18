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
using App.Global.DateTimeHelper;
using SanyaaDelivery.Domain.DTOs;
using App.Global.DTOs;
using Microsoft.EntityFrameworkCore;

namespace SanyaaDelivery.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IRepository<TokenT> tokenRepository;
        private readonly IRepository<AccountT> accountRepository;

        public TokenService(IConfiguration config, IRepository<TokenT> tokenRepository, IRepository<AccountT> accountRepository)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            this.tokenRepository = tokenRepository;
            this.accountRepository = accountRepository;
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
            else if(account.AccountTypeId == GeneralSetting.EmployeeAccountTypeId)
            {
                systemUserId = GeneralSetting.EmployeeAppSystemUserId;
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
                var r = role.Role;
                if (r.IsNull())
                {
                    r = GeneralSetting.RoleList.Find(d => d.RoleId == role.RoleId);
                }
                claims.Add(new Claim(ClaimTypes.Role, r.RoleName));
            }
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDiscreptor = new SecurityTokenDescriptor
            {
                SigningCredentials = creds,
                Subject = new ClaimsIdentity(claims),
                //Expires = DateTime.Now.EgyptTimeNow().AddHours(1)
                Expires = DateTime.Now.EgyptTimeNow().AddDays(GeneralSetting.TokenExpireInDays)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDiscreptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<Result<string>> RenewTokenAsync(RenewTokenDto model, bool skipCheckSignature = false)
        {
            var account = await accountRepository.Where(d => d.AccountId == model.AccountId)
                .Include(d => d.AccountRoleT).ThenInclude(d => d.Role)
               .FirstOrDefaultAsync();
            if (account.IsNull())
            {
                return ResultFactory<string>.CreateErrorResponseMessage("Account not found", App.Global.Enums.ResultStatusCode.NotFound);
            }
            var signature = App.Global.Encreption.Hashing.ComputeSha256Hash(model.AccountId + account.AccountSecurityCode);
            if(skipCheckSignature == false && signature != model.Signature)
            {
                return ResultFactory<string>.CreateErrorResponseMessage("Invalid data siganture, please contact us", App.Global.Enums.ResultStatusCode.InvalidSignature);
            }
            var token = CreateToken(account);
            return ResultFactory<string>.CreateSuccessResponse(token);
        }
    }
}
