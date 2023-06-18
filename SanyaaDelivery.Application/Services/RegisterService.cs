using App.Global.DTOs;
using App.Global.ExtensionMethods;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Global.DateTimeHelper;
using Microsoft.EntityFrameworkCore;
using System.IO;
using App.Global.SMS;

namespace SanyaaDelivery.Application.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IClientService clientService;
        private readonly IGeneralSetting generalSetting;
        private readonly IRepository<AccountT> accountRepository;
        private readonly IRepository<ClientT> clientRepository;
        private readonly INotificatonService notificatonService;
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly ITokenService tokenService;
        private readonly IAttachmentService attachmentService;
        private readonly ISMSService smsService;
        private readonly IRepository<DepartmentT> departmentRepository;
        private readonly IRepository<CityT> cityRepository;
        private readonly IUnitOfWork unitOfWork;

        public RegisterService(IClientService clientService, IGeneralSetting generalSetting, IRepository<AccountT> accountRepository,
            IRepository<ClientT> clientRepository, INotificatonService notificatonService,
            IRepository<EmployeeT> employeeRepository, ITokenService tokenService, IAttachmentService attachmentService, ISMSService smsService,
            IRepository<DepartmentT> departmentRepository, IRepository<CityT> cityRepository, IUnitOfWork unitOfWork)
        {
            this.clientService = clientService;
            this.generalSetting = generalSetting;
            this.accountRepository = accountRepository;
            this.clientRepository = clientRepository;
            this.notificatonService = notificatonService;
            this.employeeRepository = employeeRepository;
            this.tokenService = tokenService;
            this.attachmentService = attachmentService;
            this.smsService = smsService;
            this.departmentRepository = departmentRepository;
            this.cityRepository = cityRepository;
            this.unitOfWork = unitOfWork;
        }

        private string GetNewIfEmpty(string value, string newValue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return newValue;
            }
            return value;
        }

        private int? GetNewIfEmpty(int? value, int? newValue)
        {
            if (value.HasValue && value != 0)
            {
                return value;
            }
            return newValue;
        }

        public async Task<Result<ClientRegisterResponseDto>> RegisterClientAsync(ClientRegisterDto clientRegisterDto)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                ClientT client = new ClientT
                {
                    ClientName = clientRegisterDto.Name,
                    ClientEmail = clientRegisterDto.Email,
                    CurrentPhone = clientRegisterDto.Phone,
                    SystemUserId = GeneralSetting.CustomerAppSystemUserId,
                    ClientRegDate = DateTime.Now.EgyptTimeNow(),
                    ClientPhonesT = new List<ClientPhonesT>
                    {
                        new ClientPhonesT
                        {
                            ClientPhone = clientRegisterDto.Phone,
                            IsDefault = true
                        }
                    }
                };
                await clientRepository.AddAsync(client);
                await unitOfWork.SaveAsync();
                var account = await RegisterClientAccountAsync(client, clientRegisterDto);
                if (account.IsNull())
                {
                    return ResultFactory<ClientRegisterResponseDto>.CreateErrorResponse();
                }
                await unitOfWork.SaveAsync();
                if (isRootTransaction)
                {
                    await unitOfWork.CommitAsync(false);
                }
                var res = new ClientRegisterResponseDto
                {
                    Client = client,
                    OtpCode = account.MobileOtpCode,
                    OTPExpireTime = account.LastOtpCreationTime.Value.AddMinutes(GeneralSetting.OTPExpireMinutes),
                    SecurityCode = account.AccountSecurityCode
                };
                return ResultFactory<ClientRegisterResponseDto>.CreateSuccessResponse(res, App.Global.Enums.ResultStatusCode.ClientRegisterdSuccessfully);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return ResultFactory<ClientRegisterResponseDto>.CreateExceptionResponse(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }

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
            var account = await RegisterAccountAsync(client.ClientId.ToString(), clientRegisterDto.Phone, clientRegisterDto.Password, GeneralSetting.CustomerAccountTypeId,
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
        public Task<AccountT> RegisterClientAccountAsync(ClientT client, ClientRegisterDto clientRegisterDto)
        {
            return RegisterAccountAsync(client.ClientId.ToString(), clientRegisterDto.Phone, clientRegisterDto.Password, GeneralSetting.CustomerAccountTypeId,
                GeneralSetting.CustomerAppSystemUserId, GeneralSetting.CustomerRoleId, clientRegisterDto.FCMToken);
        }
        public async Task<AccountT> RegisterAccountAsync(string id, string userName, string password, int accountTypeId, int systemUserId, int roleId,
            string fcmToken = null, bool isGuest = false, bool isActive = true, bool requireConfirmMobile = true)
        {
            AccountT account = await accountRepository
                .Where(d => d.AccountReferenceId == id && d.AccountTypeId == accountTypeId)
                .Include(d => d.AccountRoleT).ThenInclude(d => d.Role)
                .FirstOrDefaultAsync();

            if (account.IsNotNull())
            {
                return account;
            }

            bool resetPassword = false;
            string passwordSlat = Guid.NewGuid().ToString().Replace("-", "");
            account = new AccountT
            {
                AccountHashSlat = passwordSlat,
                AccountPassword = password.IsNull() ? "" : App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(passwordSlat, password),
                AccountUsername = userName,
                CreationDate = DateTime.Now.EgyptTimeNow(),
                AccountReferenceId = id,
                IsActive = isActive,
                SystemUserId = systemUserId,
                AccountSecurityCode = Guid.NewGuid().ToString().Replace("-", ""),
                AccountTypeId = accountTypeId,
                MobileOtpCode = App.Global.Generator.GenerateOTPCode(4),
                IsMobileVerfied = isGuest ? true : false,
                IsEmailVerfied = isGuest ? true : false,
                LastOtpCreationTime = DateTime.Now.EgyptTimeNow().AddMinutes(-2),
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
            if(requireConfirmMobile == false)
            {
                account.IsMobileVerfied = true;
            }
            await accountRepository.AddAsync(account);
            await accountRepository.SaveAsync();
            return account;
        }

        public async Task<Result<ClientRegisterResponseDto>> RegisterClientCompleteAsync(ClientRegisterDto model)
        {
            Result<ClientRegisterResponseDto> result;
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var client = await clientService.GetByPhone(model.Phone);
                if (client.IsNull())
                {
                    result = await RegisterClientAsync(model);
                }
                else
                {
                    var account = await RegisterClientAccountAsync(client, model);
                    if (account.IsDeleted)
                    {
                        account.IsDeleted = false;
                        accountRepository.Update(account.AccountId, account);
                    }
                    //var res  = new ClientRegisterResponseDto
                    //{
                    //    Client = client,
                    //    OtpCode = account.MobileOtpCode,
                    //    OTPExpireTime = account.LastOtpCreationTime.Value,
                    //    SecurityCode = account.AccountSecurityCode
                    //};
                    result = ResultFactory<ClientRegisterResponseDto>.CreateErrorResponseMessageFD("This number is already registered with us, please login", App.Global.Enums.ResultStatusCode.AlreadyExist);
                }
                if (result.IsSuccess)
                {
                    _ = smsService.SendOTPAsync(model.Phone, result.Data.OtpCode);
                }
                if (isRootTransaction)
                {
                    await unitOfWork.CommitAsync(false);
                }
                return result;
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return ResultFactory<ClientRegisterResponseDto>.CreateExceptionResponse(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }

        }
        public async Task<Result<EmployeeRegisterResponseDto>> RegisterEmployeeAsync(EmployeeRegisterDto model, int systemUserId)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var isExist = employeeRepository
                    .Where(d => d.EmployeeId == model.NationalId)
                    .Any();
                if (isExist)
                {
                    return ResultFactory<EmployeeRegisterResponseDto>.CreateErrorResponseMessageFD("This national number is already register, please login");
                }
                bool accountExist = await accountRepository.Where(d => d.AccountTypeId == GeneralSetting.EmployeeAccountTypeId && d.AccountUsername == model.PhoneNumber)
                    .AnyAsync();
                if (accountExist)
                {
                    return ResultFactory<EmployeeRegisterResponseDto>.CreateErrorResponseMessageFD("This phone number is already register, please login");
                }
                EmployeeT employee = new EmployeeT
                {
                    IsNewEmployee = true,
                    EmployeePhone = model.PhoneNumber,
                    EmployeeName = model.Name,
                    SystemId = systemUserId,
                    EmployeeId = model.NationalId,
                    EmployeeFlatNum = 0,
                    EmployeeBlockNum = 0,
                    EmployeeFileNum = model.NationalId.Substring(0, 10)
                };
                await employeeRepository.AddAsync(employee);
                var account = await RegisterAccountAsync(employee.EmployeeId, employee.EmployeePhone,
                    model.Password, GeneralSetting.EmployeeAccountTypeId, systemUserId, GeneralSetting.EmployeeAppDefaultRoleId,
                    model.FCMToken, isActive: true, requireConfirmMobile: false);
                int affectedRows = 0;
                if (isRootTransaction)
                {
                     affectedRows = await unitOfWork.CommitAsync(false);
                }
                var response = new EmployeeRegisterResponseDto
                {
                    Employee = employee,
                    OtpCode = account.MobileOtpCode,
                    OTPExpireTime = DateTime.Now.ToEgyptTime().AddMinutes(GeneralSetting.OTPExpireMinutes),
                    SecurityCode = account.AccountSecurityCode,
                    NextRegisterStep = (int)Domain.Enum.EmployeeRegisterStep.CompleteNationalAndRelative,
                    NextRegisterStepDescription = Domain.Enum.EmployeeRegisterStep.ConfirmMobile.ToString(),
                    IsDataComplete = false,
                    Token = tokenService.CreateToken(account),
                    AccountId = account.AccountId
                };
                try
                {
                    await notificatonService.SendFirebaseNotificationAsync(Domain.Enum.AccountType.Employee, account.AccountReferenceId, "OTP Code", $"Your OTP is {account.MobileOtpCode}");
                }
                catch { }
                return ResultFactory<EmployeeRegisterResponseDto>.CreateAffectedRowsResult(affectedRows, data: response);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return ResultFactory<EmployeeRegisterResponseDto>.CreateExceptionResponse(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
            
        }
        public async Task<Result<string>> CompleteEmployeePersonalDataAsync(string phoneNumber, string nationalId, string relativeName, string realtivePhone,
            byte[] profilePic, string profileExtention,
            byte[] nationalIdFront, string nationalFrontExtention,
            byte[] nationalIdBack, string nationalBackExtention)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var employee = await employeeRepository
                    .Where(d => d.EmployeeId == nationalId)
                    .FirstOrDefaultAsync();
                if (employee.IsNull())
                {
                    return ResultFactory<string>.CreateNotFoundResponse("Employee not found");
                }
                employee.EmployeePhone1 = GetNewIfEmpty(employee.EmployeePhone, phoneNumber);
                employee.EmployeeRelativeName = GetNewIfEmpty(employee.EmployeeRelativeName, relativeName);
                employee.EmployeeRelativePhone = GetNewIfEmpty(employee.EmployeeRelativePhone, realtivePhone);
                var profilePicAttachment = await attachmentService.SaveFileAsync(profilePic, (int)Domain.Enum.AttachmentType.ProfilePicture, nationalId, profileExtention);
                await attachmentService.SaveFileAsync(nationalIdBack, (int)Domain.Enum.AttachmentType.NationalIdBack, nationalId, nationalFrontExtention);
                await attachmentService.SaveFileAsync(nationalIdFront, (int)Domain.Enum.AttachmentType.NationalIdFront, nationalId, nationalBackExtention);
                employee.EmployeeImageUrl = profilePicAttachment.FilePath;
                employeeRepository.Update(employee.EmployeeId, employee);
                var affectedRows = 0;
                if (isRootTransaction)
                {
                    affectedRows = await unitOfWork.CommitAsync(false);
                }
                return ResultFactory<string>.CreateAffectedRowsResult(affectedRows);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return ResultFactory<string>.CreateExceptionResponse(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
        }

        public async Task<int> CompleteEmployeeAddressAsync(EmployeeAddressDto model)
        {
            var employee = await employeeRepository
                  .Where(d => d.EmployeeId == model.NationalId)
                  .Include(d => d.EmployeeLocation)
                  .FirstOrDefaultAsync();
            if (employee.IsNull())
            {
                return (int)App.Global.Enums.ResultStatusCode.NotFound;
            }
            employee.EmployeeGov = GetNewIfEmpty(employee.EmployeeGov, model.Governate);
            employee.EmployeeCity = GetNewIfEmpty(employee.EmployeeCity, model.City);
            employee.EmployeeRegion = GetNewIfEmpty(employee.EmployeeRegion, model.Region);
            employee.EmployeeStreet = GetNewIfEmpty(employee.EmployeeStreet, model.Street);
            employee.EmployeeDes = GetNewIfEmpty(employee.EmployeeDes, model.Description);
            employee.EmployeeFlatNum = GetNewIfEmpty(employee.EmployeeFlatNum, model.FlatNumber);
            employee.EmployeeBlockNum = GetNewIfEmpty(employee.EmployeeBlockNum, model.BlockNumber);
            if (employee.EmployeeLocation.IsNull())
            {
                employee.EmployeeLocation = new EmployeeLocation
                {
                    EmployeeId = model.NationalId,
                };
            }
            employee.EmployeeLocation.Latitude = GetNewIfEmpty(employee.EmployeeLocation.Latitude, model.Lat);
            employee.EmployeeLocation.Location = GetNewIfEmpty(employee.EmployeeLocation.Location, model.Location);
            employee.EmployeeLocation.Longitude = GetNewIfEmpty(employee.EmployeeLocation.Longitude, model.Lang);
            employeeRepository.Update(employee.EmployeeId, employee);
            return await employeeRepository.SaveAsync();
        }
        public async Task<Result<EmployeeT>> CompleteEmployeeWorkingDataAsync(EmployeeWorkingDataDto model)
        {
            var employee = await employeeRepository
                  .Where(d => d.EmployeeId == model.NationalId)
                  .Include(d => d.EmployeeWorkplacesT)
                  .Include(d => d.DepartmentEmployeeT)
                  .Include(d => d.OpreationT)
                  .FirstOrDefaultAsync();
            if (employee.IsNull())
            {
                return ResultFactory<EmployeeT>.CreateErrorResponseMessageFD("Employee not found");
            }

            foreach (var item in model.Departments)
            {
                if(employee.DepartmentEmployeeT.Any(d => d.DepartmentId == item))
                {
                    continue;
                }
                var department = await departmentRepository.GetAsync(item);
                employee.DepartmentEmployeeT.Add(new DepartmentEmployeeT
                {
                    DepartmentId = item,
                    EmployeeId = model.NationalId,
                    DepartmentName = department.DepartmentName,
                    Percentage = department.DepartmentPercentage
                });
            }

            if (employee.EmployeeWorkplacesT.IsEmpty())
            {
                var city = await cityRepository.GetAsync(model.CityId);
                employee.EmployeeWorkplacesT.Add(new EmployeeWorkplacesT
                {
                    BranchId = city.BranchId.GetValueOrDefault(1),
                    EmployeeId = model.NationalId
                });
            }
            if (employee.OpreationT.IsNull())
            {
                employee.OpreationT = new OpreationT
                {
                    IsActive = true,
                    LastActiveTime = DateTime.Now.EgyptTimeNow(),
                };
            }
            if (employee.SubscriptionId.IsNull())
            {
                employee.SubscriptionId = GeneralSetting.DefaultEmployeeSubacriptionId;
            }
            employeeRepository.Update(employee.EmployeeId, employee);
            var affectedRows = await employeeRepository.SaveAsync();
            return ResultFactory<EmployeeT>.CreateAffectedRowsResult(affectedRows, data: employee);
        }
        public async Task<Result<EmployeeRegisterStepDto>> GetEmployeeRegisterStepAsync(string nationalId)
        {
            var employee = await employeeRepository
               .Where(d => d.EmployeeId == nationalId)
               .Include(d => d.EmployeeWorkplacesT)
               .Include(d => d.DepartmentEmployeeT)
               .Include(d => d.EmployeeLocation)
               .Include(d => d.OpreationT)
               .FirstOrDefaultAsync();
            if (employee.IsNull())
            {
                return ResultFactory<EmployeeRegisterStepDto>.CreateSuccessResponse(new EmployeeRegisterStepDto
                {
                    NationalId = nationalId,
                    AccountId = null,
                    NextStep = (int)Domain.Enum.EmployeeRegisterStep.Register,
                    NextStepDescription = Domain.Enum.EmployeeRegisterStep.Register.ToString()
                });
            }
            var account = await accountRepository.Where(d => d.AccountTypeId == GeneralSetting.EmployeeAccountTypeId && d.AccountReferenceId == employee.EmployeeId)
                .FirstOrDefaultAsync();
            if (account.IsNull())
            {
                account = await RegisterAccountAsync(nationalId, employee.EmployeePhone, nationalId, GeneralSetting.EmployeeAccountTypeId, GeneralSetting.EmployeeAppSystemUserId, GeneralSetting.EmployeeAppDefaultRoleId, isActive: true);
                return ResultFactory<EmployeeRegisterStepDto>.CreateSuccessResponse(new EmployeeRegisterStepDto
                {
                    NationalId = nationalId,
                    AccountId = account.AccountId,
                    NextStep = (int)Domain.Enum.EmployeeRegisterStep.ConfirmMobile,
                    NextStepDescription = Domain.Enum.EmployeeRegisterStep.ConfirmMobile.ToString()
                });
            }
            if(account.IsMobileVerfied == false)
            {
                return ResultFactory<EmployeeRegisterStepDto>.CreateSuccessResponse(new EmployeeRegisterStepDto
                {
                    NationalId = nationalId,
                    AccountId = account.AccountId,
                    NextStep = (int)Domain.Enum.EmployeeRegisterStep.ConfirmMobile,
                    NextStepDescription = Domain.Enum.EmployeeRegisterStep.ConfirmMobile.ToString()
                });
            }
            if (string.IsNullOrEmpty(employee.EmployeeRelativeName) || string.IsNullOrEmpty(employee.EmployeeRelativePhone) || string.IsNullOrEmpty(employee.EmployeeImageUrl))
            {
                return ResultFactory<EmployeeRegisterStepDto>.CreateSuccessResponse(new EmployeeRegisterStepDto
                {
                    NationalId = nationalId,
                    AccountId = account.AccountId,
                    NextStep = (int)Domain.Enum.EmployeeRegisterStep.CompleteNationalAndRelative,
                    NextStepDescription = Domain.Enum.EmployeeRegisterStep.CompleteNationalAndRelative.ToString()
                });
            }
            if(employee.EmployeeLocation.IsNull() || string.IsNullOrEmpty(employee.EmployeeGov) || string.IsNullOrEmpty(employee.EmployeeCity) || string.IsNullOrEmpty(employee.EmployeeRegion)
                || string.IsNullOrEmpty(employee.EmployeeStreet) || employee.EmployeeBlockNum.IsNull() || employee.EmployeeFlatNum.IsNull() 
                || string.IsNullOrEmpty(employee.EmployeeLocation.Latitude) || string.IsNullOrEmpty(employee.EmployeeLocation.Longitude))
            {
                return ResultFactory<EmployeeRegisterStepDto>.CreateSuccessResponse(new EmployeeRegisterStepDto
                {
                    NationalId = nationalId,
                    AccountId = account.AccountId,
                    NextStep = (int)Domain.Enum.EmployeeRegisterStep.CompleteAddress,
                    NextStepDescription = Domain.Enum.EmployeeRegisterStep.CompleteAddress.ToString()
                });
            }
            if (employee.DepartmentEmployeeT.IsEmpty() || employee.EmployeeWorkplacesT.IsEmpty())
            {
                return ResultFactory<EmployeeRegisterStepDto>.CreateSuccessResponse(new EmployeeRegisterStepDto
                {
                    NationalId = nationalId,
                    AccountId = account.AccountId,
                    NextStep = (int)Domain.Enum.EmployeeRegisterStep.CompleteDepartmentAndWorkplace,
                    NextStepDescription = Domain.Enum.EmployeeRegisterStep.CompleteDepartmentAndWorkplace.ToString()
                });
            }
            return ResultFactory<EmployeeRegisterStepDto>.CreateSuccessResponse(new EmployeeRegisterStepDto
            {
                NationalId = nationalId,
                AccountId = account.AccountId,
                NextStep = (int)Domain.Enum.EmployeeRegisterStep.Done,
                NextStepDescription = Domain.Enum.EmployeeRegisterStep.Done.ToString()
            });
        }

        public async Task<Result<SystemUserDto>> ConfirmClientRegisterOTPAsync(int? clientId, string phone, string otpCode, string signature)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                if (!clientId.HasValue || string.IsNullOrEmpty(otpCode) || string.IsNullOrEmpty(signature))
                {
                    return ResultFactory<SystemUserDto>.CreateErrorResponseMessageFD("Please enter all data first", App.Global.Enums.ResultStatusCode.EmptyData);
                }
                var account = await accountRepository.
                    Where(d => d.AccountTypeId == GeneralSetting.CustomerAccountTypeId && d.AccountReferenceId == clientId.ToString())
                    .Include(d => d.AccountRoleT)
                    .FirstOrDefaultAsync();
                if (account.IsNull())
                {
                    return ResultFactory<SystemUserDto>.CreateErrorResponse();
                }

                var passwordHash = App.Global.Encreption.Hashing.ComputeSha256Hash(clientId + phone + otpCode + account.AccountSecurityCode);
                if (passwordHash.ToLower() != signature.ToLower())
                {
                    return ResultFactory<SystemUserDto>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Enums.ResultStatusCode.InvalidSignature);
                }

                if (account.MobileOtpCode != otpCode)
                {
                    return ResultFactory<SystemUserDto>.CreateErrorResponseMessageFD("Invalid OTP code", App.Global.Enums.ResultStatusCode.InvalidOTP, App.Global.Enums.ResultAleartType.FailedDialog);
                }
                account.IsMobileVerfied = true;
                account.IsActive = true;
                accountRepository.Update(account.AccountId, account);
                var client = await clientService.GetByPhone(phone);
                string token = tokenService.CreateToken(account);
                await tokenService.AddAsync(new TokenT { AccountId = account.AccountId, CreationTime = DateTime.Now.EgyptTimeNow(), Token = token });
                if (isRootTransaction)
                {
                    await unitOfWork.CommitAsync(false);
                }
                var res = new SystemUserDto
                {
                    Username = phone,
                    Token = token,
                    TokenExpireDate = DateTime.Now.EgyptTimeNow().AddDays(GeneralSetting.TokenExpireInDays),
                    UserData = client,
                    AccountId = account.AccountId
                };
                return ResultFactory<SystemUserDto>.CreateSuccessResponse(res);
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


        public async Task<Result<OTPCodeDto>> ResendOTP(int accountId)
        {
            var account = await accountRepository.GetAsync(accountId);
            account.LastOtpCreationTime = DateTime.Now.EgyptTimeNow();
            account.MobileOtpCode = App.Global.Generator.GenerateOTPCode(4);
            accountRepository.Update(accountId, account);
            await accountRepository.SaveAsync();
            try
            {
                await notificatonService.SendFirebaseNotificationAsync(Domain.Enum.AccountType.Employee, account.AccountReferenceId, "OTP Code", $"Your OTP is {account.MobileOtpCode}");
            }
            catch { }
            return ResultFactory<OTPCodeDto>.CreateSuccessResponse(new OTPCodeDto
            {
                OTPCode = account.MobileOtpCode,
                OTPExpireTime = account.LastOtpCreationTime.Value.AddMinutes(GeneralSetting.OTPExpireMinutes)
            });
        }
    }
}
