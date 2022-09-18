using App.Global.ExtensionMethods;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IClientService clientService;
        private readonly IAccountService accountService;
        private readonly IGeneralSetting generalSetting;

        public RegisterService(IClientService clientService, IAccountService accountService, IGeneralSetting generalSetting)
        {
            this.clientService = clientService;
            this.accountService = accountService;
            this.generalSetting = generalSetting;
        }
        public async Task<ClientRegisterResponseDto> RegisterClient(ClientRegisterDto clientRegisterDto)
        {
            ClientT client = new ClientT
            {
                ClientName = clientRegisterDto.Name,
                ClientEmail = clientRegisterDto.Email,
                CurrentPhone = clientRegisterDto.Phone,
                SystemUserId = GeneralSetting.CustomerAppSystemUserId,
                ClientRegDate = DateTime.Now,
                ClientPhonesT = new List<ClientPhonesT>
                {
                    new ClientPhonesT
                    {
                        ClientPhone = clientRegisterDto.Phone,
                        IsDefault = true
                    }
                }
            };
            var result = await clientService.Add(client);
            if (result <= 0)
            {
                return null;
            }
            string passwordSlat = Guid.NewGuid().ToString().Replace("-", "");
            AccountT account = new AccountT
            {
                AccountHashSlat = passwordSlat,
                AccountPassword = App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(passwordSlat, clientRegisterDto.Password),
                AccountUsername = clientRegisterDto.Phone,
                CreationDate = DateTime.Now,
                AccountReferenceId = client.ClientId.ToString(),
                IsActive = false,
                SystemUserId = GeneralSetting.CustomerAppSystemUserId,
                AccountSecurityCode = Guid.NewGuid().ToString().Replace("-", ""),
                AccountTypeId = GeneralSetting.CustomerAccountTypeId,
                MobileOtpCode = App.Global.Generator.GenerateOTPCode(4),
                IsMobileVerfied = false,
                IsEmailVerfied = false,
                LastOtpCreationTime = DateTime.Now,
                AccountRoleT = new List<AccountRoleT>
                {
                    new AccountRoleT
                    {
                        RoleId = GeneralSetting.CustomerRoleId
                    }
                }
            };
            var addAccountResult = await accountService.Add(account);
            if(addAccountResult <= 0)
            {
                return null;
            }
            return new ClientRegisterResponseDto
            {
                Client = client,
                OtpCode = account.MobileOtpCode,
                OTPExpireTime = account.LastOtpCreationTime.Value.AddMinutes(GeneralSetting.OTPExpireMinutes),
                SecurityCode = account.AccountSecurityCode
            };
        }

        public async Task<AccountT> RegisterClientAccount(ClientT client, ClientRegisterDto clientRegisterDto)
        {
            string password = null;
            bool resetPassword = false;
            if (string.IsNullOrEmpty(clientRegisterDto.Password))
            {
                resetPassword = true;
            }
            else
            {
                password = clientRegisterDto.Password;
            }
            string passwordSlat = Guid.NewGuid().ToString().Replace("-", "");
            AccountT account = new AccountT
            {
                AccountHashSlat = passwordSlat,
                AccountPassword = password.IsNull() ? "" : App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(passwordSlat, clientRegisterDto.Password),
                AccountUsername = clientRegisterDto.Phone,
                CreationDate = DateTime.Now,
                AccountReferenceId = client.ClientId.ToString(),
                IsActive = false,
                SystemUserId = GeneralSetting.CustomerAppSystemUserId,
                AccountSecurityCode = Guid.NewGuid().ToString().Replace("-", ""),
                AccountTypeId = GeneralSetting.CustomerAccountTypeId,
                MobileOtpCode = App.Global.Generator.GenerateOTPCode(4),
                IsMobileVerfied = false,
                IsEmailVerfied = false,
                LastOtpCreationTime = DateTime.Now,
                AccountRoleT = new List<AccountRoleT>
                {
                    new AccountRoleT
                    {
                        RoleId = GeneralSetting.CustomerRoleId
                    }
                },
                IsPasswordReseted = resetPassword,
            };
            var addAccountResult = await accountService.Add(account);
            if (addAccountResult <= 0)
            {
                return null;
            }
            return account;
        }
    }
}
