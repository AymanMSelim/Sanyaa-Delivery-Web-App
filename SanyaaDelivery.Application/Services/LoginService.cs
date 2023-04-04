using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.DTOs;
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
        private readonly IRepository<AccountT> accountRepository;
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly ITokenService tokenService;
        private readonly IRegisterService registerService;

        public LoginService(ISystemUserService systemUserService, IRepository<AccountT> accountRepository,
            IRepository<EmployeeT> employeeRepository, ITokenService tokenService, IRegisterService registerService)
        {
            this.systemUserService = systemUserService;
            this.accountRepository = accountRepository;
            this.employeeRepository = employeeRepository;
            this.tokenService = tokenService;
            this.registerService = registerService;
        }

        public async Task<Result<EmployeeLoginResponseDto>> LoginEmployee(LoginEmployeeDto model)
        {
            EmployeeLoginResponseDto response = new EmployeeLoginResponseDto();
            if (model.IsNull())
            {
                return ResultFactory<EmployeeLoginResponseDto>.CreateErrorResponseMessageFD("Empty data");
            }
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                return ResultFactory<EmployeeLoginResponseDto>.CreateErrorResponseMessageFD("Empty username or password");
            }
            var account = await accountRepository.DbSet.FirstOrDefaultAsync(d => d.AccountUsername == model.UserName);
            var validationCredentialResult = ValidateAccountCredential(account, model.Password);
            if (validationCredentialResult.IsFail)
            {
                return validationCredentialResult.Convert(response);
            }
            var employee = await employeeRepository.GetAsync(account.AccountReferenceId);
            if (employee.IsNull())
            {
                return ResultFactory<EmployeeLoginResponseDto>.CreateErrorResponseMessageFD("This employee not found");
            }
            var registerStep = await registerService.GetEmployeeRegisterStep(account.AccountReferenceId);
            response.NextRegisterStep = registerStep.Data.NextStep;
            response.NextRegisterStepDescription = registerStep.Data.NextStepDescription;
            if (string.IsNullOrEmpty(model.FCMToken) == false && account.FcmToken != model.FCMToken)
            {
                account.FcmToken = model.FCMToken;
                accountRepository.Update(account.AccountId, account);
                await accountRepository.SaveAsync();
            }
            if (response.NextRegisterStep == (int)Domain.Enum.EmployeeRegisterStep.Done)
            {
                var accountValidation = ValidateAccount(account, model.Password);
                if (accountValidation.IsFail)
                {
                    return accountValidation.Convert(response);
                }
                if (employee.IsApproved == false)
                {
                    return ResultFactory<EmployeeLoginResponseDto>.CreateErrorResponseMessageFD("This employee not approved yet");
                }
            }
            response.IsDataComplete = employee.IsDataComplete;
            //if (employee.IsDataComplete == false)
            //{
            //    var registerStep = await registerService.GetEmployeeRegisterStep(employee.EmployeeId);
            //    response.NextRegisterStep = registerStep.Data.NextStep;
            //    response.NextRegisterStepDescription = registerStep.Data.NextStepDescription;
            //}
            response.Employee = employee;
            response.AccountId = account.AccountId;
            response.FCMToken = account.FcmToken;
            response.SecurityCode = account.AccountSecurityCode;
            response.Token = tokenService.CreateToken(account);
            return ResultFactory<EmployeeLoginResponseDto>.CreateSuccessResponse(response);
        }

        private Result<AccountT> ValidateAccountCredential(AccountT account, string password = null)
        {
            if (account.IsNull() || account.IsDeleted)
            {
                return ResultFactory<AccountT>.CreateErrorResponseMessageFD(message: "Invalid username or password", resultStatusCode: App.Global.Enums.ResultStatusCode.InvalidUserOrPassword);
            }
            if (password.IsNotNull())
            {
                var hashedPassword = App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(account.AccountHashSlat, password);
                if (account.AccountPassword != hashedPassword)
                {
                    return ResultFactory<AccountT>.CreateErrorResponseMessageFD(message: "Invalid username or password", resultStatusCode: App.Global.Enums.ResultStatusCode.InvalidUserOrPassword);
                }
            }
            return ResultFactory<AccountT>.CreateSuccessResponse();
        }
        private Result<AccountT> ValidateAccount(AccountT account, string password = null)
        {
            var vaildateCredentialResult = ValidateAccountCredential(account, password);
            if (vaildateCredentialResult.IsFail) { return vaildateCredentialResult; }
            if (account.IsMobileVerfied == false)
            {
                return ResultFactory<AccountT>.CreateErrorResponseMessageFD(message: "This phone number not verfied", resultStatusCode: App.Global.Enums.ResultStatusCode.MobileVerificationRequired);
            }
            if (account.IsActive == false)
            {
                return ResultFactory<AccountT>.CreateErrorResponseMessageFD(message: "This account is suspended", resultStatusCode: App.Global.Enums.ResultStatusCode.AccountSuspended);
            }
            return ResultFactory<AccountT>.CreateSuccessResponse();
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
