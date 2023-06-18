using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Global.DateTimeHelper;
using SanyaaDelivery.Infra.Data;
using App.Global.SMS;
using SanyaaDelivery.Domain.Enum;

namespace SanyaaDelivery.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly ISystemUserService systemUserService;
        private readonly IRepository<AccountT> accountRepository;
        private readonly IRepository<ClientT> clientRepository;
        private readonly IRepository<ClientPhonesT> clientPhoneRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ISMSService smsService;
        private readonly INotificatonService notificatonService;
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly ITokenService tokenService;
        private readonly IRegisterService registerService;

        public LoginService(ISystemUserService systemUserService, IRepository<AccountT> accountRepository, IRepository<ClientT> clientRepository,
            IRepository<ClientPhonesT> clientPhoneRepository, IUnitOfWork unitOfWork, ISMSService smsService, INotificatonService notificatonService,
            IRepository<EmployeeT> employeeRepository, ITokenService tokenService, IRegisterService registerService)
        {
            this.systemUserService = systemUserService;
            this.accountRepository = accountRepository;
            this.clientRepository = clientRepository;
            this.clientPhoneRepository = clientPhoneRepository;
            this.unitOfWork = unitOfWork;
            this.smsService = smsService;
            this.notificatonService = notificatonService;
            this.employeeRepository = employeeRepository;
            this.tokenService = tokenService;
            this.registerService = registerService;
        }

        private Result<EmployeeLoginResponseDto> ValidateEmployeeLogin(EmployeeT employee)
        {
            if (employee.IsFired)
            {
                return ResultFactory<EmployeeLoginResponseDto>.CreateErrorResponseMessageFD("This employee if fired!");
            }
            return ResultFactory<EmployeeLoginResponseDto>.CreateSuccessResponse();
        }

        public async Task<Result<EmployeeLoginResponseDto>> LoginEmployeeAsync(LoginEmployeeDto model)
        {
            EmployeeLoginResponseDto response = new EmployeeLoginResponseDto();
            if (model.IsNull() || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                return ResultFactory<EmployeeLoginResponseDto>.CreateEmptyDataErrorMessageFD();
            }
            var employee = await employeeRepository.Where(d => d.EmployeePhone == model.UserName)
                .FirstOrDefaultAsync();
            if (employee.IsNull())
            {
                return ResultFactory<EmployeeLoginResponseDto>.CreateErrorResponseMessageFD(message: "Invalid username or password", resultStatusCode: App.Global.Enums.ResultStatusCode.InvalidUserOrPassword);
            }
            var account = await accountRepository.DbSet.FirstOrDefaultAsync(d => d.AccountUsername == model.UserName && d.AccountTypeId == (int)AccountType.Employee);
            if (account.IsNull())
            {
                account = await registerService.RegisterAccountAsync(employee.EmployeeId, employee.EmployeePhone, employee.EmployeeId, (int)AccountType.Employee, 1, GeneralSetting.EmployeeAppDefaultRoleId, model.FCMToken);
            }
            var validationCredentialResult = ValidateAccountCredential(account, model.Password);
            if (validationCredentialResult.IsFail)
            {
                return validationCredentialResult.Convert(response);
            }
            var employeeValidation = ValidateEmployeeLogin(employee);
            if (employeeValidation.IsFail)
            {
                return employeeValidation;
            }
            var registerStep = await registerService.GetEmployeeRegisterStepAsync(account.AccountReferenceId);
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
                    return ResultFactory<EmployeeLoginResponseDto>
                        .CreateErrorResponseMessageFD("This employee's data has not yet been reviewed and approved, please try again after a while or contact the administration");
                }
            }
            response.IsDataComplete = employee.IsDataComplete;
            //if (employee.IsDataComplete == false)
            //{
            //    var registerStep = await registerService.GetEmployeeRegisterStep(employee.EmployeeId);
            //    response.NextRegisterStep = registerStep.Data.NextStep;
            //    response.NextRegisterStepDescription = registerStep.Data.NextStepDescription;
            //}
            if(response.NextRegisterStep == (int)Domain.Enum.EmployeeRegisterStep.ConfirmMobile)
            {
                try
                {
                    await notificatonService.SendFirebaseNotificationAsync(Domain.Enum.AccountType.Employee, account.AccountReferenceId, "OTP Code", $"Your OTP is {account.MobileOtpCode}");
                }
                catch { }
            }
            response.Employee = employee;
            response.AccountId = account.AccountId;
            response.FCMToken = account.FcmToken;
            response.SecurityCode = account.AccountSecurityCode;
            response.Token = tokenService.CreateToken(account);
            return ResultFactory<EmployeeLoginResponseDto>.CreateSuccessResponse(response);
        }

        private Result<AccountT> ValidateAccountCredential(AccountT account, string password = null, string otp = null)
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
            if (otp.IsNotNull())
            {
                if (account.MobileOtpCode != otp)
                {
                    return ResultFactory<AccountT>.CreateErrorResponseMessageFD(message: "Invalid OTP Code", resultStatusCode: App.Global.Enums.ResultStatusCode.InvalidOTP);
                }
            }
            return ResultFactory<AccountT>.CreateSuccessResponse();
        }

        private Result<AccountT> ValidateAccount(AccountT account, string password = null, string otp = null)
        {
            var vaildateCredentialResult = ValidateAccountCredential(account, password, otp);
            if (vaildateCredentialResult.IsFail) { return vaildateCredentialResult; }
            if (account.IsMobileVerfied == false)
            {
                return ResultFactory<AccountT>.CreateErrorResponseMessageFD(message: "This mobile number has not been confirmed, please log in by sending a code to your number", resultStatusCode: App.Global.Enums.ResultStatusCode.MobileVerificationRequired);
            }
            if (account.IsActive == false)
            {
                return ResultFactory<AccountT>.CreateErrorResponseMessageFD(message: "Your account has been suspended, please contact us to recover it", resultStatusCode: App.Global.Enums.ResultStatusCode.AccountSuspended);
            }
            return ResultFactory<AccountT>.CreateSuccessResponse();
        }

        public async Task<Result<SystemUserT>> SystemUserLoginAsync(string userName, string password, int? version = null)
        {
            var systemUser = await systemUserService.Get(userName);
            if (systemUser.IsNull())
            {
                return ResultFactory<SystemUserT>.CreateNotFoundResponse("This user not found");
            }
            if (systemUser.SystemUserPass != password)
            {
                return ResultFactory<SystemUserT>.CreateErrorResponseMessage("User or password incorrect");
            }
            return ResultFactory<SystemUserT>.CreateSuccessResponse(systemUser);
        }

        public async Task<Result<SystemUserDto>> LoginClientAsync(string username, string password = null, string otp = null)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                if (string.IsNullOrEmpty(username) || (string.IsNullOrEmpty(otp) && string.IsNullOrEmpty(password)))
                {
                    return ResultFactory<SystemUserDto>.CreateErrorResponseMessageFD("Please enter the mobile number and OTP/password first", App.Global.Enums.ResultStatusCode.EmptyData);
                }

                var client = await clientPhoneRepository.Where(d => d.ClientPhone == username)
                    .Select(d => d.Client)
                    .FirstOrDefaultAsync();

                if (client.IsNull())
                {
                    return ResultFactory<SystemUserDto>.CreateErrorResponseMessageFD("This number is not registered, please register first", App.Global.Enums.ResultStatusCode.NotFound);
                }

                var account = await accountRepository
                    .Where(d => d.AccountTypeId == GeneralSetting.CustomerAccountTypeId && d.AccountReferenceId == client.ClientId.ToString())
                    .Include(d => d.AccountRoleT).ThenInclude(d => d.Role)
                    .FirstOrDefaultAsync();

                if (account.IsNull())
                {
                    account = await registerService.RegisterAccountAsync(client.ClientId.ToString(), username, null, GeneralSetting.CustomerAccountTypeId, GeneralSetting.CustomerAppSystemUserId,
                        GeneralSetting.CustomerAppDefaultRoleId);
                    await unitOfWork.SaveAsync();
                }
                if (string.IsNullOrEmpty(otp) is false)
                {
                    account.IsMobileVerfied = true;
                    accountRepository.Update(account.AccountId, account);
                }
                var accountValidationResult = ValidateAccount(account, password, otp);
                if (accountValidationResult.IsFail)
                {
                    return accountValidationResult.Convert(new SystemUserDto(), true);
                }
                string token = tokenService.CreateToken(account);
                await tokenService.AddAsync(new TokenT { AccountId = account.AccountId, CreationTime = DateTime.Now.EgyptTimeNow(), Token = token });
                var systemUserDto = new SystemUserDto
                {
                    Username = username,
                    Token = token,
                    TokenExpireDate = DateTime.Now.EgyptTimeNow().AddDays(GeneralSetting.TokenExpireInDays),
                    UserData = client,
                    AccountId = account.AccountId,
                    FCMToken = account.FcmToken
                };
                if (isRootTransaction)
                {
                    var affectedRows = await unitOfWork.CommitAsync(false);
                    return ResultFactory<SystemUserDto>.CreateAffectedRowsResult(affectedRows, data: systemUserDto);
                }
                return ResultFactory<SystemUserDto>.CreateSuccessResponse(systemUserDto);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return ResultFactory<SystemUserDto>.CreateExceptionResponse(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }

        }

        public async Task<Result<OTPCodeDto>> RequestOTPForLoginAsync(string clientPhone)
        {
            var client = await clientPhoneRepository.Where(d => d.ClientPhone == clientPhone)
                .Select(d => d.Client)
                .FirstOrDefaultAsync();
            if (client.IsNull())
            {
                return ResultFactory<OTPCodeDto>.CreateNotFoundResponse("This number is not registered, please register first");
            }
            var account = await accountRepository
                .Where(d => d.AccountTypeId == GeneralSetting.CustomerAccountTypeId && d.AccountReferenceId == client.ClientId.ToString())
                .Include(d => d.AccountRoleT).ThenInclude(d => d.Role)
                .FirstOrDefaultAsync();
            if (account.IsNull())
            {
                account = await registerService.RegisterClientAccountAsync(client, new ClientRegisterDto { Phone = clientPhone, Email = client.ClientEmail, Name = client.ClientName, Password = clientPhone });
            }

            if (account.IsDeleted)
            {
                return ResultFactory<OTPCodeDto>.CreateErrorResponseMessageFD("This number is not registered, please register first", App.Global.Enums.ResultStatusCode.NotFound);
            }
           
            if (account.LastOtpCreationTime.HasValue && DateTime.Now.EgyptTimeNow() < account.LastOtpCreationTime.Value.AddSeconds(60))
            {
                return ResultFactory<OTPCodeDto>.CreateErrorResponseMessageFD("You can't request 2 otp code at the same time, please wait 1 minute at least", App.Global.Enums.ResultStatusCode.MaximumCountReached);
            }

            if (DateTime.Now.EgyptTimeNow().Date == account.LastOtpCreationTime.Value.Date)
            {
                account.OtpCountWithinDay++;
            }
            account.MobileOtpCode = App.Global.Generator.GenerateOTPCode(4);
            account.LastOtpCreationTime = DateTime.Now.EgyptTimeNow();
            accountRepository.Update(account.AccountId, account);
            _ = smsService.SendOTPAsync(clientPhone, account.MobileOtpCode);
            var affectedRows = await accountRepository.SaveAsync();
            return ResultFactory<OTPCodeDto>.CreateAffectedRowsResult(affectedRows, data: new OTPCodeDto
            {
                OTPCode = account.MobileOtpCode,
                OTPExpireTime = account.LastOtpCreationTime.Value.AddMinutes(GeneralSetting.OTPExpireMinutes)
            }, aleartType: App.Global.Enums.ResultAleartType.SuccessToast);
        }
    }
}
