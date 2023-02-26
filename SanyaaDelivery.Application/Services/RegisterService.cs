using App.Global.DTOs;
using App.Global.ExtensionMethods;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data;
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
        private readonly IUnitOfWork unitOfWork;

        public RegisterService(IClientService clientService, IAccountService accountService, IGeneralSetting generalSetting, IUnitOfWork unitOfWork)
        {
            this.clientService = clientService;
            this.accountService = accountService;
            this.generalSetting = generalSetting;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ClientRegisterResponseDto> RegisterClient(ClientRegisterDto clientRegisterDto)
        {
            unitOfWork.StartTransaction();
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
            await clientService.Add(client);
            var affectedRows = await unitOfWork.SaveAsync();
            if (affectedRows <= 0)
            {
                return null;
            }
            var account = await RegisterClientAccount(client, clientRegisterDto);
            if(account.IsNull())
            {
                return null;
            }
            await unitOfWork.CommitAsync();
            return new ClientRegisterResponseDto
            {
                Client = client,
                OtpCode = account.MobileOtpCode,
                OTPExpireTime = account.LastOtpCreationTime.Value.AddMinutes(GeneralSetting.OTPExpireMinutes),
                SecurityCode = account.AccountSecurityCode
            };
        }

        public async Task<GuestClientRegisterResponseDto> RegisterGuest(ClientRegisterDto clientRegisterDto)
        {
            unitOfWork.StartTransaction();
            ClientT client = new ClientT
            {
                ClientName = clientRegisterDto.Name,
                ClientEmail = clientRegisterDto.Email,
                CurrentPhone = clientRegisterDto.Phone,
                SystemUserId = GeneralSetting.CustomerAppSystemUserId,
                ClientRegDate = DateTime.Now,
                IsGuest = true,
                BranchId = 60,
                ClientPhonesT = new List<ClientPhonesT>
                {
                    new ClientPhonesT
                    {
                        ClientPhone = clientRegisterDto.Phone,
                        IsDefault = true
                    }
                },
                AddressT = new List<AddressT>{ 
                    new AddressT
                    {
                        CityId = 11,
                        GovernorateId = 1,
                        RegionId = 3,
                        AddressGov = "مصر",
                        AddressCity = "العبور",
                        AddressRegion = "الحى الاول",
                        IsDefault = true
                    }
                }
            };
            var affectedRows = await clientService.Add(client);
            if (affectedRows <= 0)
            {
                return null;
            }
            var account = await RegisterAccount(client.ClientId.ToString(), clientRegisterDto.Phone, clientRegisterDto.Password, GeneralSetting.CustomerAccountTypeId,
                GeneralSetting.CustomerAppSystemUserId, GeneralSetting.GuestRoleId, clientRegisterDto.FCMToken, true);
            if (account.IsNull())
            {
                return null;
            }
            await unitOfWork.CommitAsync();
            return new GuestClientRegisterResponseDto
            {
                Client = client,
                Account = account
            };
        }


        public Task<AccountT> RegisterClientAccount(ClientT client, ClientRegisterDto clientRegisterDto)
        {
            return RegisterAccount(client.ClientId.ToString(), clientRegisterDto.Phone, clientRegisterDto.Password, GeneralSetting.CustomerAccountTypeId, 
                GeneralSetting.CustomerAppSystemUserId, GeneralSetting.CustomerRoleId, clientRegisterDto.FCMToken);
        }

        public async Task<AccountT> RegisterAccount(string id, string userName, string password, int accountTypeId, int systemUserId, int roleId, string fcmToken = null, bool isGuest = false)
        {
            bool resetPassword = false;
            //if (string.IsNullOrEmpty(password))
            //{
            //    resetPassword = true;
            //}
            string passwordSlat = Guid.NewGuid().ToString().Replace("-", "");
            AccountT account = new AccountT
            {
                AccountHashSlat = passwordSlat,
                AccountPassword = password.IsNull() ? "" : App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(passwordSlat, password),
                AccountUsername = userName,
                CreationDate = DateTime.Now,
                AccountReferenceId = id,
                IsActive = true,
                SystemUserId = systemUserId,
                AccountSecurityCode = Guid.NewGuid().ToString().Replace("-", ""),
                AccountTypeId = accountTypeId,
                MobileOtpCode = App.Global.Generator.GenerateOTPCode(4),
                IsMobileVerfied = isGuest ? true : false,
                IsEmailVerfied = isGuest ? true : false,
                LastOtpCreationTime = DateTime.Now,
                AccountRoleT = new List<AccountRoleT>
                {
                    new AccountRoleT
                    {
                        RoleId = roleId
                    }
                },
                IsPasswordReseted = resetPassword,
                FcmToken = fcmToken
            };
            var affectedRows = await accountService.Add(account);
            if (affectedRows <= 0)
            {
                return null;
            }
            return account;
        }

        public async Task<Result<ClientRegisterResponseDto>> CRegisterClient(ClientRegisterDto clientRegisterDto)
        {
            ClientRegisterResponseDto clientRegisterResponseDto = null;
            var client = await clientService.GetByPhone(clientRegisterDto.Phone);
            if (client != null)
            {
                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, client.ClientId.ToString());
                if (account == null)
                {
                    account = await RegisterClientAccount(client, clientRegisterDto);
                }
                if (account.IsDeleted)
                {
                    account.IsDeleted = false;
                    await accountService.Update(account);
                }

                clientRegisterResponseDto = new ClientRegisterResponseDto
                {
                    Client = client,
                    OtpCode = account.MobileOtpCode,
                    OTPExpireTime = account.LastOtpCreationTime.Value,
                    SecurityCode = account.AccountSecurityCode
                };
                return ResultFactory<ClientRegisterResponseDto>
                    .CreateSuccessResponseSD(clientRegisterResponseDto, App.Global.Enums.ResultStatusCode.AlreadyExist, "This phone is already token!, please login");
            }
            clientRegisterResponseDto = await RegisterClient(clientRegisterDto);
            if (clientRegisterResponseDto == null)
            {
                return ResultFactory<ClientRegisterResponseDto>.CreateErrorResponse();
            }
            _ = App.Global.SMS.SMSMisrService.SendSmsAsync("2", clientRegisterDto.Phone, $"Your SanyaaDelivery code is {clientRegisterResponseDto.OtpCode}");
            return ResultFactory<ClientRegisterResponseDto>.CreateSuccessResponse(clientRegisterResponseDto, App.Global.Enums.ResultStatusCode.ClientRegisterdSuccessfully);
        }

        public async Task<Result<ClientRegisterResponseDto>> CRegisterGuestClient(int guestClientId, ClientRegisterDto clientRegisterDto)
        {
            ClientRegisterResponseDto clientRegisterResponseDto = null;
            var client = await clientService.GetByPhone(clientRegisterDto.Phone);
            if (client.IsNotNull())
            {
                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, client.ClientId.ToString());
                if (account == null)
                {
                    account = await RegisterClientAccount(client, clientRegisterDto);
                }
                clientRegisterResponseDto = new ClientRegisterResponseDto
                {
                    Client = client,
                    OtpCode = account.MobileOtpCode,
                    OTPExpireTime = account.LastOtpCreationTime.Value,
                    SecurityCode = account.AccountSecurityCode
                };
                return ResultFactory<ClientRegisterResponseDto>
                    .CreateSuccessResponseSD(clientRegisterResponseDto, App.Global.Enums.ResultStatusCode.AlreadyExist, "This phone is already token!, please login");
            }
            clientRegisterResponseDto = await RegisterClient(clientRegisterDto);
            if (clientRegisterResponseDto == null)
            {
                return ResultFactory<ClientRegisterResponseDto>.CreateErrorResponse();
            }
            _ = App.Global.SMS.SMSMisrService.SendSmsAsync("2", clientRegisterDto.Phone, $"Your SanyaaDelivery code is {clientRegisterResponseDto.OtpCode}");
            return ResultFactory<ClientRegisterResponseDto>.CreateSuccessResponse(clientRegisterResponseDto, App.Global.Enums.ResultStatusCode.ClientRegisterdSuccessfully);
        }
    }
}
