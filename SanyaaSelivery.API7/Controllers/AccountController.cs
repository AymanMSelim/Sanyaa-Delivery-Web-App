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
using App.Global.DateTimeHelper;
using System.Security.Claims;
using SanyaaDelivery.API.Dto;
using App.Global.SMS;

namespace SanyaaDelivery.API.Controllers
{
    public class AccountController : API.Controllers.APIBaseAuthorizeController
    {
        private readonly ISystemUserService systemUserService;
        private readonly ITokenService tokenService;
        private readonly IAccountService accountService;
        private readonly IAccountRoleService accountRoleService;
        private readonly ISMSService smsService;
        private readonly IClientService clientService;
        private readonly IEmployeeService employeeService;
        private readonly IGeneralSetting generalSetting;
        private readonly IRegisterService registerService;
        private readonly ILoginService loginService;
        private readonly CommonService commonService;

        public AccountController(ISystemUserService systemUserService, ITokenService tokenService,
            IAccountService accountService, IAccountRoleService accountRoleService, ISMSService smsService,
            IClientService clientService, IEmployeeService employeeService, IGeneralSetting generalSetting,
            IRegisterService registerService, ILoginService loginService, CommonService commonService) : base(commonService)
        {
            this.systemUserService = systemUserService;
            this.tokenService = tokenService;
            this.accountService = accountService;
            this.accountRoleService = accountRoleService;
            this.smsService = smsService;
            this.clientService = clientService;
            this.employeeService = employeeService;
            this.generalSetting = generalSetting;
            this.registerService = registerService;
            this.loginService = loginService;
            this.commonService = commonService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("GetByReference")]
        public async Task<ActionResult<Result<AccountT>>> GetByReference(int referenceType, string id)
        {
            try
            {
                var account = await accountService.Get(referenceType, id);
                return Ok(ResultFactory<AccountT>.CreateSuccessResponse(account));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AccountT>.CreateExceptionResponse(ex));
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("Update")]
        public async Task<ActionResult<Result<AccountT>>> Update(AccountT account)
        {
            try
            {
                var affectedRows = await accountService.Update(account);
                return Ok(ResultFactory<AccountT>.CreateAffectedRowsResult(affectedRows, data:account));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AccountT>.CreateExceptionResponse(ex));
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
            var result = await loginService.SystemUserLoginAsync(user.Username, user.Password);
            if (result.IsFail)
            {
                return result;
            }
            var systemUser = result.Data;
            var account = await accountService.Get(GeneralSetting.SystemUserAccountTypeId, systemUser.SystemUserId.ToString());
            if (account.IsNull())
            {
                account = await registerService.RegisterAccountAsync(systemUser.SystemUserId.ToString(), systemUser.SystemUserUsername, systemUser.SystemUserPass, GeneralSetting.SystemUserAccountTypeId, systemUser.SystemUserId, GeneralSetting.SystemUserDefaultRoleId);
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
        public async Task<ActionResult<Result<SystemUserDto>>> LoginClient(ClientLoginDto model)
        {
            try
            {
                model.Username = commonService.RepairPhoneNumber(model.Username);
                if (commonService.IsPhoneNotValid(model.Username))
                {
                    return Ok(ResultFactory<ClientRegisterResponseDto>.CreateErrorResponseMessageFD(commonService.GetPhoneNotValidMessage(model.Username)));
                }
                var result = await loginService.LoginClientAsync(model.Username, password: model.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginClientWithOtp")]
        public async Task<ActionResult<Result<SystemUserDto>>> LoginClientWithOtp(ClientLoginWithOtpDto model)
        {
            try
            {
                model.Phone = commonService.RepairPhoneNumber(model.Phone);
                if (commonService.IsPhoneNotValid(model.Phone))
                {
                    return Ok(ResultFactory<ClientRegisterResponseDto>.CreateErrorResponseMessageFD(commonService.GetPhoneNotValidMessage(model.Phone)));
                }
                var result = await loginService.LoginClientAsync(model.Phone, otp: model.OtpCode);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<SystemUserDto>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("RegisterClient")]
        public async Task<ActionResult<Result<ClientRegisterResponseDto>>> RegisterClient(ClientRegisterDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(ResultFactory<ClientRegisterResponseDto>
                        .CreateErrorResponseMessageFD("Please enter all data first", App.Global.Enums.ResultStatusCode.EmptyData));
                }
                model.Phone = commonService.RepairPhoneNumber(model.Phone);
                if (commonService.IsPhoneNotValid(model.Phone))
                {
                    return Ok(ResultFactory<ClientRegisterResponseDto>.CreateErrorResponseMessageFD(commonService.GetPhoneNotValidMessage(model.Phone)));
                }
                var result = await registerService.RegisterClientCompleteAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("RegisterEmployee")]
        public async Task<ActionResult<Result<EmployeeRegisterResponseDto>>> RegisterEmployee(EmployeeRegisterDto employeeRegisterDto)
        {
            try
            {
                var result = await registerService.RegisterEmployeeAsync(employeeRegisterDto, commonService.GetSystemUserId());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<EmployeeRegisterResponseDto>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginEmployee")]
        public async Task<ActionResult<Result<EmployeeLoginResponseDto>>> LoginEmployee(LoginEmployeeDto model)
        {
            try
            {
                //var s = App.Global.Serialization.Json.Serialize(model);
                //System.IO.File.AppendAllText($@"wwwroot\timer\login.txt", s + @"\n");
                var result = await loginService.LoginEmployeeAsync(model);
                //var r = App.Global.Serialization.Json.Serialize(result);
                //System.IO.File.AppendAllText($@"wwwroot\timer\login.txt", r);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<EmployeeLoginResponseDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("ConfirmOtp")]
        public async Task<ActionResult<Result<bool>>> ConfirmOtp(ConfirmOtpDto model)
        {
            try
            {
                var result = await accountService.ConfirmOTPAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<bool>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("CompleteEmployeePersonalData")]
        public async Task<ActionResult<Result<string>>> CompleteEmployeePersonalData([FromForm] CompleteEmployeePersonalDataDto model)
        {
            try
            {
                var isValid = commonService.IsFileValid(model.ProfilePic);
                if (isValid == false)
                {
                    return Ok(ResultFactory<string>.FileValidationError("ProfilePicture"));
                }
                isValid = commonService.IsFileValid(model.NationalIdBack);
                if (isValid == false)
                {
                    return Ok(ResultFactory<string>.FileValidationError("NationalIdBack"));
                }
                isValid = commonService.IsFileValid(model.NationalIdFront);
                if (isValid == false)
                {
                    return Ok(ResultFactory<string>.FileValidationError("NationalIdFront"));
                }
                if (string.IsNullOrEmpty(model.PhoneNumber) || string.IsNullOrEmpty(model.NationalId) || string.IsNullOrEmpty(model.RelativeName) || string.IsNullOrEmpty(model.RelativePhone))
                {
                    return Ok(ResultFactory<string>.CreateErrorResponseMessage("Data not complete"));
                }
                var result = await registerService.CompleteEmployeePersonalDataAsync(model.PhoneNumber, model.NationalId, model.RelativeName, model.RelativePhone,
                    commonService.ConvertFileToByteArray(model.ProfilePic), commonService.GetFileExtention(model.ProfilePic),
                    commonService.ConvertFileToByteArray(model.NationalIdFront), commonService.GetFileExtention(model.NationalIdFront),
                    commonService.ConvertFileToByteArray(model.NationalIdBack), commonService.GetFileExtention(model.NationalIdBack));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<string>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("CompleteEmployeeAddress")]
        public async Task<ActionResult<Result<string>>> CompleteEmployeeAddress(EmployeeAddressDto model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.City) || string.IsNullOrEmpty(model.Governate) || string.IsNullOrEmpty(model.Region) || string.IsNullOrEmpty(model.Street)
                    || string.IsNullOrEmpty(model.Lang) || string.IsNullOrEmpty(model.Lat) || string.IsNullOrEmpty(model.Description)) 
                {
                    return Ok(ResultFactory<string>.CreateErrorResponseMessage("Data not complete"));
                }
                var affectedRows = await registerService.CompleteEmployeeAddressAsync(model);
                return Ok(ResultFactory<string>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<string>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("CompleteEmployeeWorkingData")]
        public async Task<ActionResult<Result<EmployeeT>>> CompleteEmployeeWorkingData(EmployeeWorkingDataDto model)
        {
            try
            {
                if(model.Departments.IsEmpty() || model.CityId == 0 || string.IsNullOrEmpty(model.NationalId))
                {
                    return Ok(ResultFactory<string>.CreateErrorResponseMessage("Data not complete"));
                }
                var result = await registerService.CompleteEmployeeWorkingDataAsync(model);
                result.Data = null;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<EmployeeT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetEmployeeRegisterStep/{nationalId}")]
        public async Task<ActionResult<Result<EmployeeRegisterStepDto>>> GetEmployeeRegisterStep(string nationalId)
        {
            try
            {
                var result = await registerService.GetEmployeeRegisterStepAsync(nationalId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<EmployeeRegisterStepDto>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("ConfirmOTPCode")]
        public async Task<ActionResult<Result<SystemUserDto>>> ConfirmRegisterOTPCode(int? clientId, string phone, string otpCode, string signature)
        {
            try
            {
                phone = commonService.RepairPhoneNumber(phone);
                var result = await registerService.ConfirmClientRegisterOTPAsync(clientId, phone, otpCode, signature);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<SystemUserDto>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("ResendOTPCode")]
        public async Task<ActionResult<Result<OTPCodeDto>>> ResendOTPCode(int? clientId, string phone, string signature)
        {
            try
            {
                phone = commonService.RepairPhoneNumber(phone);
                var result = await loginService.RequestOTPForLoginAsync(phone);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("ResendOTP")]
        public async Task<ActionResult<Result<OTPCodeDto>>> ResendOTP()
        {
            try
            {
                var accountId = commonService.GetAccountId();
                var result = await registerService.ResendOTP(accountId.Value);
                return Ok(ResultFactory<object>.CreateSuccessResponse());
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
            try
            {
                clientPhone = commonService.RepairPhoneNumber(clientPhone);
                if (commonService.IsPhoneNotValid(clientPhone))
                {
                    return Ok(ResultFactory<OTPCodeDto>.CreateErrorResponseMessageFD(commonService.GetPhoneNotValidMessage(clientPhone)));
                }
                var result = await loginService.RequestOTPForLoginAsync(clientPhone);
                return Ok(result);
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
                phoneNumber = commonService.RepairPhoneNumber(phoneNumber);
                //if (commonService.IsPhoneNotValid(phoneNumber))
                //{
                //    return Ok(ResultFactory<object>.CreateErrorResponseMessageFD(commonService.GetPhoneNotValidMessage(phoneNumber)));
                //}
                var client = await clientService.GetByPhone(phoneNumber);
                if (client.IsNull())
                {
                    return Ok(ResultFactory<object>.CreateNotFoundResponse("No client found match this phone number, please sign up first", App.Global.Enums.ResultAleartType.None));
                }
                var account = await accountService.Get((int)Domain.Enum.AccountType.Client, client.ClientId.ToString());
                if (account.IsNull())
                {
                    account = await registerService.RegisterClientAccountAsync(client, new ClientRegisterDto { Phone = phoneNumber });
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
                    account = await registerService.RegisterClientAccountAsync(client, new ClientRegisterDto { Phone = phoneNumber });
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
                _ = smsService.SendOTPAsync(phoneNumber, account.MobileOtpCode);
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
        public async Task<ActionResult<Result<object>>> RenewToken(int? accountId = null, string? signature = null)
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
        [HttpPost("RenewAppToken")]
        public async Task<ActionResult<Result<string>>> RenewAppToken(RenewTokenDto model)
        {
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    var accountId = App.Global.JWT.TokenHelper.GetAccountId(identity);
                    if (accountId.IsNotNull())
                    {
                        var resultA = await tokenService.RenewTokenAsync(new RenewTokenDto { AccountId = accountId.Value }, true);
                        return Ok(resultA);
                    }
                }
                var result = await tokenService.RenewTokenAsync(model);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse(ex));
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


        [Authorize]
        [HttpPost("Logout")]
        public async Task<ActionResult<Result<object>>> Logout()
        {
            try
            {
                return Ok(ResultFactory<object>.CreateSuccessResponse());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

    }
}
