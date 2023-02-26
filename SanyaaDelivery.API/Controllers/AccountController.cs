using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Application;
using App.Global.DTOs;
using SanyaaDelivery.Domain;
using App.Global.ExtensionMethods;
using System.Security.Claims;

namespace SanyaaDelivery.API.Controllers
{
    public class AccountController : API.Controllers.APIBaseAuthorizeController
    {
        private readonly ISystemUserService systemUserService;
        private readonly ITokenService tokenService;
        private readonly IAccountService accountService;
        private readonly IAccountRoleService accountRoleService;
        private readonly IClientService clientService;
        private readonly IEmployeeService employeeService;
        private readonly IGeneralSetting generalSetting;
        private readonly IRegisterService registerService;
        private readonly ILoginService loginService;
        private readonly CommonService commonService;

        public AccountController(ISystemUserService systemUserService, ITokenService tokenService,
            IAccountService accountService, IAccountRoleService accountRoleService,
            IClientService clientService, IEmployeeService employeeService, IGeneralSetting generalSetting,
            IRegisterService registerService, ILoginService loginService, CommonService commonService)
        {
            this.systemUserService = systemUserService;
            this.tokenService = tokenService;
            this.accountService = accountService;
            this.accountRoleService = accountRoleService;
            this.clientService = clientService;
            this.employeeService = employeeService;
            this.generalSetting = generalSetting;
            this.registerService = registerService;
            this.loginService = loginService;
            this.commonService = commonService;
        }

        [AllowAnonymous]
        [HttpGet("GetToken")]
        public object GetToken()
        {
            return Ok(
                   new
                   {
                       Username = "AymanSelim",
                       Token = tokenService.CreateToken(new AccountT { AccountId = 52, AccountTypeId = 3, AccountReferenceId = "10516", AccountUsername = "01090043513", AccountRoleT = new List<AccountRoleT> { new AccountRoleT {  Role = new RoleT { RoleName = "CustomerApp" } } } }),
                       TokenExpireDate = DateTime.Now.AddDays(30),
                   });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<Result<object>>> Login(UserLoginDto user)
        {
            if (!ModelState.IsValid)
            {
                var response = ResultFactory<string>.CreateModelNotValidResponse("Empty username or password");
                return Ok(response);
            }

            string userId = null;
            object userData = null;
            if (user.AccountType == ((int)Domain.Enum.AccountType.User))
            {
                var userModel = await systemUserService.Get(user.Username);
                if (userModel != null)
                {
                    userId = userModel.SystemUserId.ToString();
                    userData = userModel;
                }
            }
            else if (user.AccountType == ((int)Domain.Enum.AccountType.Employee))
            {
                var employeeModel = await employeeService.GetAsync(user.Username);
                if (employeeModel != null)
                {
                    userId = employeeModel.EmployeeId;
                    userData = employeeModel;
                }
            }
            else if (user.AccountType == ((int)Domain.Enum.AccountType.Client))
            {
                var clientModel = await clientService.GetByPhone(user.Username);
                if (clientModel != null)
                {
                    userId = clientModel.ClientId.ToString();
                    userData = clientModel;
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                var account = await accountService.Get(user.AccountType.Value, userId);
                if (account == null)
                {
                    var response = new Result<object>
                    {
                        StatusCode = 0,
                        Message = "No account for this user",
                        StatusDescreption = "Failed"
                    };
                    return Ok(response);

                }

                if (account.IsActive == false)
                {
                    var response = new Result<object>
                    {
                        StatusCode = 0,
                        Message = "this account is suspend",
                        StatusDescreption = "Failed"
                    };
                    return Ok(response);
                }

                var accountRoles = await accountRoleService.GetList(account.AccountId, true);
                if (accountRoles == null)
                {
                    var response = new Result<object>
                    {
                        StatusCode = 0,
                        Message = "No roles for this user",
                        StatusDescreption = "Failed"
                    };
                    return Ok(response);
                }

                var password = App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(account.AccountHashSlat, user.Password);
                if (account.AccountPassword != password)
                {
                    var response = new Result<object>
                    {
                        StatusCode = 0,
                        Message = "Invalid username or password",
                        StatusDescreption = "Failed"
                    };
                    return Ok(response);
                }
                return Ok(
                    new Result<SystemUserDto>
                    {
                        StatusCode = 1,
                        Message = "Token Generated Sussessfuly",
                        StatusDescreption = "Success",
                        Data = new SystemUserDto
                        {
                            Username = user.Username,
                            Token = tokenService.CreateToken(account),
                            TokenExpireDate = DateTime.Now.AddDays(30),
                            UserData = userData,
                            FCMToken = account.FcmToken,
                            AccountId = account.AccountId
                        }
                    });
            }
            else
            {
                return Ok(new Result<object> { StatusCode = 0, Message = "Invalid username or password", StatusDescreption = "Failed" });
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginSystemUser")]
        public async Task<ActionResult<Result<SystemUserT>>> LoginSystemUser(UserLoginDto user)
        {
            if (!ModelState.IsValid)
            {
                var response = ResultFactory<SystemUserT>.CreateModelNotValidResponse("Empty username or password");
                return Ok(response);
            }
            var result = await loginService.SystemUserLogin(user.Username, user.Password);
            if (result.IsFail)
            {
                return result;
            }
            var systemUser = result.Data;
            var account = await accountService.Get(GeneralSetting.SystemUserAccountTypeId, systemUser.SystemUserId.ToString());
            if (account.IsNull())
            {
                account = await registerService.RegisterAccount(systemUser.SystemUserId.ToString(), systemUser.SystemUserUsername, systemUser.SystemUserPass, GeneralSetting.SystemUserAccountTypeId, systemUser.SystemUserId, GeneralSetting.SystemUserDefaultRoleId);
            }

            var accountRoles = await accountRoleService.GetList(account.AccountId, true);
            //var password = App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(account.AccountHashSlat, user.Password);
            //if (account.AccountPassword != password)
            //{
            //    var response = new Result<object>
            //    {
            //        StatusCode = 0,
            //        Message = "Invalid username or password",
            //        StatusDescreption = "Failed"
            //    };
            //    return Ok(response);
            //}
            systemUser.AccountT = null;
            return Ok(
                new Result<SystemUserDto>
                {
                    StatusCode = 1,
                    Message = "Token Generated Sussessfuly",
                    StatusDescreption = "Success",
                    Data = new SystemUserDto
                    {
                        Username = user.Username,
                        Token = tokenService.CreateToken(account),
                        TokenExpireDate = DateTime.Now.AddDays(30),
                        UserData = systemUser

                    }
                });
        }

        [AllowAnonymous]
        [HttpPost("LoginClient")]
        public async Task<ActionResult<Result<SystemUserDto>>> LoginClient(ClientLoginDto clientLoginDto)
        {
            try
            {
                if (clientLoginDto.IsNull() || string.IsNullOrEmpty(clientLoginDto.Username) || string.IsNullOrEmpty(clientLoginDto.Password))
                {
                    return Ok(ResultFactory<SystemUserDto>.CreateErrorResponseMessageFD("Empty username or password", App.Global.Enums.ResultStatusCode.EmptyData));
                }

                var client = await clientService.GetByPhone(clientLoginDto.Username);
                if (client == null)
                {
                    return Ok(ResultFactory<SystemUserDto>.CreateErrorResponseMessageFD("This client not found, please contact us", App.Global.Enums.ResultStatusCode.NotFound));
                }

                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, client.ClientId.ToString());
                if (account == null)
                    return Ok(ResultFactory<SystemUserDto>.CreateErrorResponseMessageFD("No account for this client, please contact us", App.Global.Enums.ResultStatusCode.NotFound));

                if (account.IsDeleted)
                    return Ok(ResultFactory<SystemUserDto>.CreateErrorResponseMessageFD("Invalid username or password", App.Global.Enums.ResultStatusCode.AccountSuspended));

                if (account.IsMobileVerfied == false)
                    return Ok(ResultFactory<object>.CreateErrorResponse(
                        new ClientRegisterResponseDto 
                        { 
                            Client = client, 
                            OtpCode = account.MobileOtpCode, 
                            SecurityCode = account.AccountSecurityCode,
                            OTPExpireTime = account.LastOtpCreationTime.Value 
                        }, App.Global.Enums.ResultStatusCode.MobileVerificationRequired, "Your phone not verifed, please login using OTP code", App.Global.Enums.ResultAleartType.FailedDialog));

                if (account.IsActive == false)
                    return Ok(ResultFactory<SystemUserDto>.CreateErrorResponseMessageFD("Account is suspended, please contact us", App.Global.Enums.ResultStatusCode.AccountSuspended));

                if (account.IsPasswordReseted.HasValue && account.IsPasswordReseted.Value)
                    return Ok(ResultFactory<SystemUserDto>.CreateErrorResponseMessageFD("Reset password required, you must reset your password", App.Global.Enums.ResultStatusCode.ResetPasswordRequired));


                if (GeneralSetting.IsEmailVerificationRequired)
                {
                    if (account.IsEmailVerfied == false)
                        return Ok(ResultFactory<SystemUserDto>.CreateErrorResponseMessage("Email verification required", App.Global.Enums.ResultStatusCode.EmailVerificationRequired));
                }
                var accountRoles = await accountRoleService.GetList(account.AccountId, true);
                if (accountRoles == null)
                    return Ok(ResultFactory<SystemUserDto>.CreateErrorResponseMessage("No roles for this user", App.Global.Enums.ResultStatusCode.NoRoleFound));

                var password = App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(account.AccountHashSlat, clientLoginDto.Password);
                if (account.AccountPassword != password)
                {
                    return Ok(ResultFactory<SystemUserDto>.CreateErrorResponseMessageFD("Invalid username or password", App.Global.Enums.ResultStatusCode.InvalidUserOrPassword));
                }
                account.AccountRoleT = accountRoles;
                string token = tokenService.CreateToken(account);
                _ = tokenService.AddAsync(new TokenT { AccountId = account.AccountId, CreationTime = DateTime.Now, Token = token });
                return Ok(
                    ResultFactory<SystemUserDto>.CreateSuccessResponse(
                         new SystemUserDto
                         {
                             Username = clientLoginDto.Username,
                             Token = token,
                             TokenExpireDate = DateTime.Now.AddDays(30),
                             UserData = client,
                             AccountId = account.AccountId,
                             FCMToken = account.FcmToken
                         }
                        ));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginClientWithOtp")]
        public async Task<ActionResult<Result<ClientT>>> LoginClientWithOtp(ClientLoginWithOtpDto clientLoginWithOtpDto)
        {
            try
            {
                if (clientLoginWithOtpDto.IsNull() || string.IsNullOrEmpty(clientLoginWithOtpDto.Phone) || string.IsNullOrEmpty(clientLoginWithOtpDto.OtpCode))
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponseMessageFD("Please fill phone number and otp first", App.Global.Enums.ResultStatusCode.EmptyData));
                }

                var client = await clientService.GetByPhone(clientLoginWithOtpDto.Phone);
                if (client == null)
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponseMessageFD("This client not found", App.Global.Enums.ResultStatusCode.NotFound));
                }

                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, client.ClientId.ToString());
                if (account == null)
                    return Ok(ResultFactory<object>.CreateErrorResponseMessageFD("No account for this client", App.Global.Enums.ResultStatusCode.NotFound));

                if (account.IsDeleted)
                    return Ok(ResultFactory<object>.CreateErrorResponseMessageFD("Invalid phone or otp code", App.Global.Enums.ResultStatusCode.AccountSuspended));


                if (account.IsActive == false)
                    return Ok(ResultFactory<object>.CreateErrorResponseMessageFD("Your account is suspended, please contact us on phone", App.Global.Enums.ResultStatusCode.AccountSuspended));

                //if (account.IsPasswordReseted.HasValue && account.IsPasswordReseted.Value)
                //    return Ok(ResultFactory<object>.CreateErrorResponseMessageFD("Please reset your password from reset password button", App.Global.Enums.ResultStatusCode.ResetPasswordRequired, App.Global.Enums.ResultAleartType.FailedDialog));

                if (GeneralSetting.IsEmailVerificationRequired)
                {
                    if (account.IsEmailVerfied == false)
                        return Ok(ResultFactory<object>.CreateErrorResponseMessageFD("Please verify your email first", App.Global.Enums.ResultStatusCode.EmailVerificationRequired));
                }
                var accountRoles = await accountRoleService.GetList(account.AccountId, true);
                if (accountRoles == null)
                    return Ok(ResultFactory<object>.CreateErrorResponseMessageFD("No roles found for this user, please contact us", App.Global.Enums.ResultStatusCode.NoRoleFound));

                //var password = App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(account.AccountHashSlat, clientLoginDto.Password);
                if (account.MobileOtpCode != clientLoginWithOtpDto.OtpCode)
                {
                    return Ok(ResultFactory<object>.CreateErrorResponseMessageFD("Invalid phone or otp code", App.Global.Enums.ResultStatusCode.InvalidUserOrPassword));
                }
                if(account.IsMobileVerfied == false)
                {
                    account.IsMobileVerfied = true;
                }
                await accountService.Update(account);
                account.AccountRoleT = accountRoles;
                string token = tokenService.CreateToken(account);
                _ = tokenService.AddAsync(new TokenT { AccountId = account.AccountId, CreationTime = DateTime.Now, Token = token });
                return Ok(
                    ResultFactory<SystemUserDto>.CreateSuccessResponse(
                         new SystemUserDto
                         {
                             Username = clientLoginWithOtpDto.Phone,
                             Token = token,
                             TokenExpireDate = DateTime.Now.AddDays(30),
                             UserData = client,
                             AccountId = account.AccountId,
                             FCMToken = account.FcmToken
                         }
                        ));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("RegisterClient")]
        public async Task<ActionResult<Result<ClientRegisterResponseDto>>> RegisterClient(ClientRegisterDto clientRegisterDto)
        {
            Result<ClientRegisterResponseDto> response;
            try
            {
                if (!ModelState.IsValid)
                {
                    response = ResultFactory<ClientRegisterResponseDto>
                        .CreateErrorResponseMessageFD("Empty username or password", App.Global.Enums.ResultStatusCode.EmptyData);
                    return Ok(response);
                }
                response = await registerService.CRegisterClient(clientRegisterDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("ConfirmOTPCode")]
        public async Task<ActionResult<Result<ClientT>>> ConfirmRegisterOTPCode(int? clientId, string phone, string otpCode, string signature)
        {
            Result<ClientT> response;
            try
            {
                if (!clientId.HasValue || string.IsNullOrEmpty(otpCode) || string.IsNullOrEmpty(signature))
                {
                    response = ResultFactory<ClientT>.CreateErrorResponseMessageFD("Please enter otp code sent to your number", App.Global.Enums.ResultStatusCode.EmptyData);
                    return Ok(response);
                }

                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, clientId.ToString());
                if (account == null)
                {
                    response = ResultFactory<ClientT>.CreateErrorResponseMessageFD("This client not found, please try again, if this problem appeat again please contact us", App.Global.Enums.ResultStatusCode.NotFound);
                    return Ok(response);
                }

                var passwordHash = App.Global.Encreption.Hashing.ComputeSha256Hash(clientId + phone + otpCode + account.AccountSecurityCode);
                if (!HttpContext.User.Identity.IsAuthenticated && passwordHash.ToLower() != signature.ToLower())
                {
                    response = ResultFactory<ClientT>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Enums.ResultStatusCode.InvalidSignature);
                    return Ok(response);
                }

                if (account.MobileOtpCode != otpCode)
                {
                    response = ResultFactory<ClientT>.CreateErrorResponseMessageFD("Invalid OTP code, please enter it correctly", App.Global.Enums.ResultStatusCode.InvalidOTP, App.Global.Enums.ResultAleartType.FailedDialog);
                    return Ok(response);
                }

                if (accountService.IsOtpExpired(account))
                {
                    response = ResultFactory<ClientT>.CreateErrorResponseMessageFD("This OTP code is expired, please request a new one", App.Global.Enums.ResultStatusCode.OTPExpired, App.Global.Enums.ResultAleartType.FailedDialog);
                    return Ok(response);
                }

                int affectedRecord = await accountService.ConfirmRegisterOtp(account);
                if (affectedRecord < 0)
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponse());
                }
                var client = await clientService.GetByPhone(phone);
                var accountRoles = await accountRoleService.GetList(account.AccountId, true);
                account.AccountRoleT = accountRoles;
                string token = tokenService.CreateToken(account);
                _ = tokenService.AddAsync(new TokenT { AccountId = account.AccountId, CreationTime = DateTime.Now, Token = token });
                return Ok(
                  ResultFactory<SystemUserDto>.CreateSuccessResponse(
                       new SystemUserDto
                       {
                           Username = phone,
                           Token = token,
                           TokenExpireDate = DateTime.Now.AddDays(30),
                           UserData = client,
                           AccountId = account.AccountId
                       }
                      ));

                //return Ok(HttpResponseDtoFactory<ClientT>.CreateSuccessResponse());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("ResendOTPCode")]
        public async Task<ActionResult<Result<OTPCodeDto>>> ResendOTPCode(int? clientId, string phone, string signature)
        {
            Result<OTPCodeDto> response;
            try
            {
                if (!clientId.HasValue || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(signature))
                {
                    response = ResultFactory<OTPCodeDto>.CreateErrorResponseMessage("Empty data", App.Global.Enums.ResultStatusCode.EmptyData);
                    return Ok(response);
                }

                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, clientId.ToString());
                if (account == null)
                {
                    response = ResultFactory<OTPCodeDto>.CreateErrorResponseMessage("This client not found", App.Global.Enums.ResultStatusCode.NotFound);
                    return Ok(response);
                }

                if (!HttpContext.User.Identity.IsAuthenticated && App.Global.Encreption.Hashing.ComputeSha256Hash(clientId + phone + account.AccountSecurityCode).ToLower() != signature.ToLower())
                {
                    response = ResultFactory<OTPCodeDto>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Enums.ResultStatusCode.InvalidSignature);
                    return Ok(response);
                }

                if (accountService.IsMaxOtpPerDayReached(account))
                {
                    response = ResultFactory<OTPCodeDto>.CreateErrorResponseMessage("Maximum otp has been reached", App.Global.Enums.ResultStatusCode.MaximumCountReached);
                    return Ok(response);
                }
                account.MobileOtpCode = App.Global.Generator.GenerateOTPCode(4);
                account.LastOtpCreationTime = DateTime.Now;
                int affectedRecord = await accountService.Update(account);
                _ = App.Global.SMS.SMSMisrService.SendSmsAsync("2", phone, $"Your SanyaaDelivery code is {account.MobileOtpCode}");
                if (affectedRecord < 0)
                {
                    return Ok(ResultFactory<OTPCodeDto>.CreateErrorResponse());
                }
                return Ok(ResultFactory<OTPCodeDto>.CreateSuccessResponse(new OTPCodeDto
                {
                    OTPCode = account.MobileOtpCode,
                    OTPExpireTime = account.LastOtpCreationTime.Value.AddMinutes(GeneralSetting.OTPExpireMinutes)
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }

        }

        [AllowAnonymous]
        [HttpGet("RequestOTPForLogin/{clientPhone}")]
        public async Task<ActionResult<Result<OTPCodeDto>>> RequestOTPForLogin(string clientPhone)
        {
            Result<OTPCodeDto> response;
            try
            {
                if (string.IsNullOrEmpty(clientPhone))
                {
                    response = ResultFactory<OTPCodeDto>.CreateErrorResponseMessage("Empty data", App.Global.Enums.ResultStatusCode.EmptyData);
                    return Ok(response);
                }

                var client = await clientService.GetByPhone(clientPhone);
                if (client.IsNull())
                {
                    response = ResultFactory<OTPCodeDto>.CreateNotFoundResponse("This client not found");
                    return Ok(response);
                }
                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, client.ClientId.ToString());
                if (account == null)
                {
                    account = await registerService.RegisterClientAccount(client, new ClientRegisterDto { Phone = clientPhone });
                }
                if (account.IsDeleted)
                    return Ok(ResultFactory<object>.CreateErrorResponseMessageFD("This client not found", App.Global.Enums.ResultStatusCode.NotFound));

                if (accountService.IsMaxOtpPerDayReached(account))
                {
                    response = ResultFactory<OTPCodeDto>.CreateErrorResponseMessage("Maximum otp has been reached", App.Global.Enums.ResultStatusCode.MaximumCountReached);
                    return Ok(response);
                }
                account.MobileOtpCode = App.Global.Generator.GenerateOTPCode(4);
                account.LastOtpCreationTime = DateTime.Now;
                int affectedRecord = await accountService.Update(account);
                _ = App.Global.SMS.SMSMisrService.SendSmsAsync("2", clientPhone, $"Your SanyaaDelivery code is {account.MobileOtpCode}");
                if (affectedRecord < 0)
                {
                    return Ok(ResultFactory<OTPCodeDto>.CreateErrorResponse());
                }
                return Ok(ResultFactory<OTPCodeDto>.CreateSuccessResponse(new OTPCodeDto
                {
                    OTPCode = account.MobileOtpCode,
                    OTPExpireTime = account.LastOtpCreationTime.Value.AddMinutes(GeneralSetting.OTPExpireMinutes)
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }

        }

        [AllowAnonymous]
        [HttpGet("IsClientExist/{phoneNumber}")]
        public async Task<ActionResult<Result<object>>> IsClientExist(string phoneNumber)
        {
            try
            {
                var client = await clientService.GetByPhone(phoneNumber);
                if (client.IsNull())
                {
                    return Ok(ResultFactory<object>.CreateNotFoundResponse("No client found match this phone number, please sign up first", App.Global.Enums.ResultAleartType.None));
                }
                var account = await accountService.Get((int)Domain.Enum.AccountType.Client, client.ClientId.ToString());
                if (account.IsNull())
                {
                    account = await registerService.RegisterClientAccount(client, new ClientRegisterDto { Phone = phoneNumber });
                }
                if (account.IsDeleted)
                    return Ok(ResultFactory<object>.CreateErrorResponseMessageFD("No client found match this phone number, please sign up first", App.Global.Enums.ResultStatusCode.NotFound));

                return Ok(ResultFactory<object>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.AlreadyExist));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("ResetClientPassword/{phoneNumber}")]
        public async Task<ActionResult<Result<object>>> ResetClientPassword(string phoneNumber)
        {
            try
            {
                var client = await clientService.GetByPhone(phoneNumber);
                if (client.IsNull())
                {
                    return ResultFactory<object>.CreateNotFoundResponse("This phone number is not registered");
                }

                var account = await accountService.Get((int)Domain.Enum.AccountType.Client, client.ClientId.ToString());
                if (account.IsNull())
                {
                    account = await registerService.RegisterClientAccount(client, new ClientRegisterDto { Phone = phoneNumber });
                }

                if (account.IsNull())
                {
                    return ResultFactory<object>.CreateErrorResponse();
                }

                if (account.AccountUsername != phoneNumber)
                {
                    return ResultFactory<object>.CreateNotFoundResponse("No account find for this phone number");
                }

                if (accountService.IsMaxResetPasswordPerDayReached(account))
                {
                    return ResultFactory<object>.CreateErrorResponseMessage("Maximum password reset has been reached", App.Global.Enums.ResultStatusCode.MaximumCountReached);
                }

                var resetPasswordResult = await accountService.ResetPassword(account);
                if (resetPasswordResult <= 0)
                {
                    return ResultFactory<object>.CreateErrorResponse();
                }
                _ = App.Global.SMS.SMSMisrService.SendSmsAsync("2", phoneNumber, $"Your SanyaaDelivery code is {account.MobileOtpCode} expired after {GeneralSetting.OTPExpireMinutes} minutes");
                return ResultFactory<object>.CreateSuccessResponse(new { OtpCode = account.MobileOtpCode, AccountId = account.AccountId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("ConfirmResetPasswordOTP")]
        public async Task<ActionResult<Result<object>>> ConfirmResetPasswordOTP(int accountId, string otpCode)
        {
            try
            {
                var account = await accountService.Get(accountId);
                if (account.IsNull())
                {
                    return Ok(ResultFactory<object>.CreateNotFoundResponse("This account not found"));
                }

                if (account.IsActive == false)
                {
                    return Ok(ResultFactory<object>.CreateErrorResponseMessage("Your account is suspended please contact us", App.Global.Enums.ResultStatusCode.AccountSuspended));
                }

                if (account.IsPasswordReseted == false)
                {
                    return Ok(ResultFactory<object>.CreateErrorResponseMessage("Password reset has not been requested", App.Global.Enums.ResultStatusCode.ResetPasswordNotRequested));
                }


                //var calculatedSignature = App.Global.Encreption.Hashing.ComputeSha256Hash(accountId + account.AccountUsername + otpCode + account.ResetPasswordToken);
                //if(!HttpContext.User.Identity.IsAuthenticated && calculatedSignature.ToLower() != signature.ToLower())
                //{
                //    return Ok(HttpResponseDtoFactory<object>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Eumns.ResponseStatusCode.InvalidSignature));
                //}

                if (account.MobileOtpCode != otpCode)
                {
                    return Ok(ResultFactory<object>.CreateErrorResponseMessage("OTP code is invalid", App.Global.Enums.ResultStatusCode.InvalidOTP));
                }

                if (accountService.IsOtpExpired(account))
                {
                    return Ok(ResultFactory<object>.CreateErrorResponseMessage("OTP code is expired", App.Global.Enums.ResultStatusCode.OTPExpired));
                }

                int affectedRecord = await accountService.ConfirmResetPasswordOtp(account);
                if (affectedRecord < 0)
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponse());
                }
                return Ok(ResultFactory<object>.CreateSuccessResponse(new { ResetToken = account.ResetPasswordToken }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("UpdatePassword")]
        public async Task<ActionResult<Result<object>>> UpdatePassword(UpdatePasswordDto updatePasswordDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(ResultFactory<object>.CreateModelNotValidResponse("Model not valid"));
                }
                var account = await accountService.Get(updatePasswordDto.AccountId);
                if (account.IsNull())
                {
                    return Ok(ResultFactory<object>.CreateNotFoundResponse("This account not found"));
                }

                if (account.IsActive == false)
                {
                    return Ok(ResultFactory<object>.CreateErrorResponseMessage("Your account is suspended please contact us", App.Global.Enums.ResultStatusCode.AccountSuspended));
                }

                var calculatedSignature = App.Global.Encreption.Hashing.ComputeSha256Hash(updatePasswordDto.AccountId + account.AccountUsername + account.ResetPasswordToken);
                if (!HttpContext.User.Identity.IsAuthenticated && (string.IsNullOrEmpty(updatePasswordDto.Signature) || calculatedSignature.ToLower() != updatePasswordDto.Signature.ToLower()))
                {
                    return Ok(ResultFactory<object>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Enums.ResultStatusCode.InvalidSignature));
                }

                int affectedRecord = await accountService.UpdatePassword(account, updatePasswordDto.Password);
                if (affectedRecord < 0)
                {
                    return Ok(ResultFactory<object>.CreateErrorResponse());
                }
                return Ok(ResultFactory<object>.CreateSuccessResponse());
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpGet("RenewToken")]
        public async Task<ActionResult<Result<object>>> RenewToken(int? accountId = null, string signature = null)
        {
            try
            {
                if (accountId.IsNull() && !HttpContext.User.Identity.IsAuthenticated)
                {
                    return Ok(ResultFactory<object>.CreateErrorResponseMessage("Account id is null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                if(HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    var nameIdentifier = App.Global.JWT.TokenHelper.GetAccountId(identity);
                    if (nameIdentifier != null)
                    {
                        accountId =nameIdentifier;
                    }
                }
                var account = await accountService.Get(accountId.Value);
                if (account.IsNull())
                {
                    return Ok(ResultFactory<object>.CreateNotFoundResponse("This account not found"));
                }

                if (account.IsActive == false)
                {
                    return Ok(ResultFactory<object>.CreateErrorResponseMessage("Your account is suspended please contact us", App.Global.Enums.ResultStatusCode.AccountSuspended));
                }

                var calculatedSignature = App.Global.Encreption.Hashing.ComputeSha256Hash(accountId + account.AccountUsername + account.AccountSecurityCode);
                if (!HttpContext.User.Identity.IsAuthenticated && (string.IsNullOrEmpty(signature) || calculatedSignature.ToLower() != signature.ToLower()))
                {
                    return Ok(ResultFactory<object>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Enums.ResultStatusCode.InvalidSignature));
                }
                account.AccountRoleT = await accountRoleService.GetList(accountId.Value, true);
                return Ok(
                       new
                       {
                           AccountId = accountId,
                           Token = tokenService.CreateToken(account),
                           TokenExpireDate = DateTime.Now.AddDays(30),
                       });
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginAsGuest")]
        public async Task<ActionResult<Result<ClientRegisterResponseDto>>> LoginAsGuest()
        {
            GuestClientRegisterResponseDto clientRegisterResponseDto;
            try
            {
                var random = new Random(DateTime.Now.Millisecond).Next(0, 99999999);
                var clientRegisterDto = new ClientRegisterDto
                {
                    Email = $"guest-{random.ToString("D6")}@guest.com",
                    Name = $"guest-{random.ToString("D6")}",
                    Password = random.ToString("D6"),
                    Phone = random.ToString("D6")
                };
                clientRegisterResponseDto = await registerService.RegisterGuest(clientRegisterDto);
                if (clientRegisterResponseDto == null)
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponse());
                }
                var account = clientRegisterResponseDto.Account;
                string token = tokenService.CreateToken(account);
                _ = tokenService.AddAsync(new TokenT { AccountId = account.AccountId, CreationTime = DateTime.Now, Token = token });
                return Ok(
                    ResultFactory<SystemUserDto>.CreateSuccessResponse(
                         new SystemUserDto
                         {
                             Username = clientRegisterDto.Phone,
                             Token = token,
                             TokenExpireDate = DateTime.Now.AddDays(30),
                             UserData = clientRegisterResponseDto.Client,
                             AccountId = account.AccountId,
                             FCMToken = account.FcmToken
                         }
                        ));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [Authorize]
        [HttpPost("Delete")]
        public async Task<ActionResult<Result<AccountT>>> Delete()
        {
            try
            {
                if(HttpContext.User.Identity.IsAuthenticated is false)
                {
                    return Ok(ResultFactory<AccountT>.CreateErrorResponseMessageFD("Not Authorize"));
                }
                var accountId = commonService.GetAccountId();
                var affectedRows = await accountService.DeleteSoft(accountId.Value);
                return Ok(ResultFactory<AccountT>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AccountT>.CreateExceptionResponse(ex));
            }
        }


    }
}
