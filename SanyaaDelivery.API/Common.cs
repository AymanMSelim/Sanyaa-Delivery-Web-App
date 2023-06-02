using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using SanyaaDelivery.Application;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Application.Services;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Global.DateTimeHelper;
using Microsoft.EntityFrameworkCore;

namespace SanyaaDelivery.API
{
    public class CommonService
    {
        private readonly IHttpContextAccessor context;
        private readonly ICartService cartService;
        private readonly IClientService clientService;
        private readonly ICityService cityService;
        private readonly IRepository<CartT> cartRepository;
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly IRepository<AccountT> accountRepository;
        private readonly IRepository<OpreationT> operationRepository;

        public CommonService(IHttpContextAccessor context, ICartService cartService, 
            IClientService clientService, ICityService cityService, IGeneralSetting generalSetting, IRepository<CartT> cartRepository,
            IRepository<EmployeeT> employeeRepository, IRepository<AccountT> accountRepository, IRepository<OpreationT> operationRepository)
        {
            this.context = context;
            this.cartService = cartService;
            this.clientService = clientService;
            this.cityService = cityService;
            this.cartRepository = cartRepository;
            this.employeeRepository = employeeRepository;
            this.accountRepository = accountRepository;
            this.operationRepository = operationRepository;
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                generalSetting.CurrentIsViaApp = IsViaApp();
            }
        }

        public int? GetClientId(int? clientId = null)
        {
            if (IsViaApp())
            {
                var identity = context.HttpContext.User.Identity as ClaimsIdentity;
                clientId = App.Global.JWT.TokenHelper.GetReferenceId(identity);
                return clientId;
            }
            else
            {
                return clientId;
            }
        }

        public string GetEmployeeId(string employeeId = null)
        {
            if (IsViaApp())
            {
                var identity = context.HttpContext.User.Identity as ClaimsIdentity;
                employeeId = App.Global.JWT.TokenHelper.GetReferenceIdString(identity);
                if(string.IsNullOrEmpty(employeeId) is false)
                {
                    var operation = operationRepository.Where(d => d.EmployeeId == employeeId)
                        .FirstOrDefault();
                    if (operation.IsNotNull())
                    {
                        operation.LastActiveTime = DateTime.Now.EgyptTimeNow();
                        operationRepository.DbSet.Update(operation);
                        operationRepository.DbContext.SaveChanges();
                    }
                //    var accountState = accountRepository.Where(d => d.AccountReferenceId == employeeId)
                //        .Select(d => new { d.IsActive, d.IsDeleted }).FirstOrDefault();
                //    if(accountState.IsActive is false)
                //    {
                //        return null;
                //    }
                //    if (accountState.IsDeleted)
                //    {
                //        return null;
                //    }
                //    var isApproved = employeeRepository.Where(d => d.EmployeeId == employeeId)
                //        .Select(d => d.IsApproved).FirstOrDefault();
                //    if(isApproved is false)
                //    {
                //        return null;
                //    }
                }
            }
            return employeeId;
        }

        public int? GetAccountId()
        {
            int? accountId = null;
            if (IsViaApp())
            {
                var identity = context.HttpContext.User.Identity as ClaimsIdentity;
                accountId = App.Global.JWT.TokenHelper.GetAccountId(identity);
                return accountId;
            }
            else
            {
                return accountId;
            }
        }

        public async Task<ClientT> GetClient(int? clientId = null, bool includeAddress = false, bool includePhone = false, bool includePoints = false)
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (IsViaApp())
            {
                clientId = App.Global.JWT.TokenHelper.GetReferenceId(identity);
            }
            if (clientId.HasValue)
            {
                return await clientService.GetAsync(clientId.Value, includePhone, includeAddress);
            }
            return null;
        }

        public async Task<AddressT> GetDefaultAddress(int? clientId = null)
        {
            clientId = GetClientId(clientId);
            if (clientId.HasValue)
            {
                return await clientService.GetDefaultAddressAsync(clientId.Value);
            }
            return null;
        }

        public async Task<ClientPhonesT> GetDefaultPhone(int? clientId = null)
        {
            clientId = GetClientId(clientId);
            if (clientId.HasValue)
            {
                return await clientService.GetDefaultPhoneAsync(clientId.Value);
            }
            return null;
        }

        public async Task<BranchT> GetCurrentAddressBranch(int? clientId = null)
        {
            clientId = GetClientId(clientId);
            if (clientId.HasValue)
            {
                var defaultAddress = await clientService.GetDefaultAddressAsync(clientId.Value);
                var branch = await cityService.GetCityBranchAsync(defaultAddress.CityId.Value);
                return branch;
            }
            return null;
        }

        public Task<Domain.Models.CartT> GetCurrentClientCartAsync(int? clientId = null, bool includeDetails = false)
        {
            bool isViaApp = IsViaApp();
            if (isViaApp)
            {
                clientId = GetClientId();
            }
            if (clientId.HasValue)
            {
                return cartService.GetCurrentByClientIdAsync(clientId.Value, isViaApp, includeDetails);
            }
            else
            {
                return null;
            }
        }

        public async Task<int?> GetCurrentClientCartIdAsync(int? clientId = null)
        {
            bool isViaApp = IsViaApp();
            if (isViaApp)
            {
                clientId = GetClientId();
            }
            if (clientId.HasValue)
            {
                return await cartRepository.Where(d => d.ClientId == clientId.Value && d.IsViaApp == isViaApp && d.HaveRequest == false)
                    .Select(d => d.CartId).FirstOrDefaultAsync();
            }
            else
            {
                return null;
            }
        }

        public int GetSystemUserId()
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            var claimValue = App.Global.JWT.TokenHelper.GetClaimValue(identity, "SystemUserId");
            if (string.IsNullOrEmpty(claimValue))
            {
                return 1;
            }
            return int.Parse(claimValue);
        }

        public bool IsViaApp()
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            int? accountType = App.Global.JWT.TokenHelper.GetAccountType(identity);
            if (accountType.HasValue && (GeneralSetting.CustomerAccountTypeId == accountType.Value || GeneralSetting.EmployeeAccountTypeId == accountType.Value))
            {
                return true;
            }
            return false;
        }

        public bool IsViaEmpApp()
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            int? accountType = App.Global.JWT.TokenHelper.GetAccountType(identity);
            if (accountType.HasValue && GeneralSetting.EmployeeAccountTypeId == accountType.Value)
            {
                return true;
            }
            return false;
        }

        public bool IsViaClientApp()
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            int? accountType = App.Global.JWT.TokenHelper.GetAccountType(identity);
            if (accountType.HasValue && GeneralSetting.CustomerAccountTypeId == accountType.Value )
            {
                return true;
            }
            return false;
        }


        public bool IsFileValid(IFormFile formFile)
        {
            if (formFile.IsNull())
            {
                return false;
            }
            if(formFile.Length <= 0)
            {
                return false;
            }
            return true;
        }

        public byte[] ConvertFileToByteArray(IFormFile formFile)
        {
            var result = IsFileValid(formFile);
            if (result == false)
            {
                return null;
            }
            using (var ms = new MemoryStream())
            {
                formFile.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return fileBytes;
            }
        }

        public string GetFileExtention(IFormFile file)
        {
            var fileExtention = System.IO.Path.GetExtension(file.FileName);
            fileExtention = fileExtention.Replace(".", "");
            return fileExtention;
        }

        public string GetHost()
        {
            return "https://" + context.HttpContext.Request.Host.Host;
        }

        public bool IsPhoneValid(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return false;
            }
            if(phone.Length != 11)
            {
                return false;
            }
            if(!long.TryParse(phone, out _))
            {
                return false;
            }
            if(phone.StartsWith("010") || phone.StartsWith("011") || phone.StartsWith("015") || phone.StartsWith("012"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPhoneNotValid(string phone)
        {
            return !IsPhoneValid(phone);
        }


        public string GetPhoneNotValidMessage(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return "Please enter the mobile number first";
            }
            if (phone.Length != 11)
            {
                return "The number you entered is incorrect";
            }
            if (!long.TryParse(phone, out _))
            {
                return "The number you entered is incorrect";
            }
            if (phone.StartsWith("010") || phone.StartsWith("011") || phone.StartsWith("015") || phone.StartsWith("012"))
            {
                return "";
            }
            else
            {
                return "The mobile number must start with 011, 012, 015 or 010";
            }
        }

        public string RepairPhoneNumber(string phone)
        {
            if(phone.Length == 10 && !phone.StartsWith("0"))
            {
                phone = "0" + phone;
            }
            return phone;
        }


    }
}
