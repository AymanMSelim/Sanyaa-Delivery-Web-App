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

namespace SanyaaDelivery.Application.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IClientService clientService;
        private readonly IAccountService accountService;
        private readonly IGeneralSetting generalSetting;
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly ITokenService tokenService;
        private readonly IAttachmentService attachmentService;
        private readonly IRepository<DepartmentT> departmentRepository;
        private readonly IRepository<CityT> cityRepository;
        private readonly IUnitOfWork unitOfWork;

        public RegisterService(IClientService clientService, IAccountService accountService, IGeneralSetting generalSetting,
            IRepository<EmployeeT> employeeRepository, ITokenService tokenService, IAttachmentService attachmentService,
            IRepository<DepartmentT> departmentRepository, IRepository<CityT> cityRepository, IUnitOfWork unitOfWork)
        {
            this.clientService = clientService;
            this.accountService = accountService;
            this.generalSetting = generalSetting;
            this.employeeRepository = employeeRepository;
            this.tokenService = tokenService;
            this.attachmentService = attachmentService;
            this.departmentRepository = departmentRepository;
            this.cityRepository = cityRepository;
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
            if (account.IsNull())
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

        public async Task<AccountT> RegisterAccount(string id, string userName, string password, int accountTypeId, int systemUserId, int roleId, string fcmToken = null, bool isGuest = false, bool isActive = true)
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
                CreationDate = DateTime.Now.EgyptTimeNow(),
                AccountReferenceId = id,
                IsActive = isActive,
                SystemUserId = systemUserId,
                AccountSecurityCode = Guid.NewGuid().ToString().Replace("-", ""),
                AccountTypeId = accountTypeId,
                MobileOtpCode = App.Global.Generator.GenerateOTPCode(4),
                IsMobileVerfied = isGuest ? true : false,
                IsEmailVerfied = isGuest ? true : false,
                LastOtpCreationTime = DateTime.Now.EgyptTimeNow(),
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
            _ = App.Global.SMS.SMSMisrService.SendOTPAsync(clientRegisterDto.Phone, clientRegisterResponseDto.OtpCode);
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
            _ = App.Global.SMS.SMSMisrService.SendOTPAsync(clientRegisterDto.Phone, clientRegisterResponseDto.OtpCode);
            return ResultFactory<ClientRegisterResponseDto>.CreateSuccessResponse(clientRegisterResponseDto, App.Global.Enums.ResultStatusCode.ClientRegisterdSuccessfully);
        }

        public async Task<Result<EmployeeRegisterResponseDto>> RegisterEmployee(EmployeeRegisterDto model, int systemUserId)
        {
            unitOfWork.StartTransaction();
            var isExist = employeeRepository
                .Where(d => d.EmployeeId == model.NationalId)
                .Any();
            if (isExist)
            {
                return ResultFactory<EmployeeRegisterResponseDto>.CreateErrorResponseMessageFD("This national number is already register, please login");
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
            var account = await RegisterAccount(employee.EmployeeId, employee.EmployeePhone,
                model.Password, GeneralSetting.EmployeeAccountTypeId, systemUserId, GeneralSetting.EmployeeAppDefaultRoleId, model.FCMToken, isActive: true);
            var affectedRows = await unitOfWork.CommitAsync();
            var response = new EmployeeRegisterResponseDto
            {
                Employee = employee,
                OtpCode = account.MobileOtpCode,
                OTPExpireTime = DateTime.Now.ToEgyptTime().AddMinutes(GeneralSetting.OTPExpireMinutes),
                SecurityCode = account.AccountSecurityCode,
                NextRegisterStep = (int)Domain.Enum.EmployeeRegisterStep.ConfirmMobile,
                NextRegisterStepDescription = Domain.Enum.EmployeeRegisterStep.ConfirmMobile.ToString(),
                IsDataComplete = false,
                Token = tokenService.CreateToken(account),
                AccountId = account.AccountId
            };
            return ResultFactory<EmployeeRegisterResponseDto>.CreateAffectedRowsResult(affectedRows, data: response);
        }

        public async Task<Result<string>> CompleteEmployeePersonalData(string phoneNumber, string nationalId, string relativeName, string realtivePhone,
            byte[] profilePic, byte[] nationalIdFront, byte[] nationalIdBack)
        {
            unitOfWork.StartTransaction();
            var employee = await employeeRepository
                .Where(d => d.EmployeeId == nationalId)
                .FirstOrDefaultAsync();
            if (employee.IsNull())
            {
                return ResultFactory<string>.CreateNotFoundResponse("Employee Not Found");
            }
            employee.EmployeeRelativeName = relativeName;
            employee.EmployeeRelativePhone = realtivePhone;
            await attachmentService.SaveFileAsync(profilePic, (int)Domain.Enum.AttachmentType.ProfilePicture, nationalId, ".jpg");
            await attachmentService.SaveFileAsync(nationalIdBack, (int)Domain.Enum.AttachmentType.NationalIdBack, nationalId, ".jpg");
            await attachmentService.SaveFileAsync(nationalIdFront, (int)Domain.Enum.AttachmentType.NationalIdFront, nationalId, ".jpg");
            var affectedRows = await unitOfWork.CommitAsync();
            return ResultFactory<string>.CreateAffectedRowsResult(affectedRows);
        }

        public async Task<int> CompleteEmployeeAddress(EmployeeAddressDto model)
        {
            var employee = await employeeRepository
                  .Where(d => d.EmployeeId == model.NationalId)
                  .FirstOrDefaultAsync();
            if (employee.IsNull())
            {
                return (int)App.Global.Enums.ResultStatusCode.NotFound;
            }
            employee.EmployeeGov = model.Governate;
            employee.EmployeeCity = model.City;
            employee.EmployeeRegion = model.Region;
            employee.EmployeeFlatNum = model.FlatNumber;
            employee.EmployeeBlockNum = model.BlockNumber;
            employee.EmployeeDes = model.Description;
            employee.EmployeeStreet = model.Street;
            employee.EmployeeLocation = new EmployeeLocation
            {
                EmployeeId = model.NationalId,
                Latitude = model.Lat,
                Longitude = model.Lang,
                Location = model.Location
            };
            return await employeeRepository.SaveAsync();
        }

        public async Task<int> CompleteEmployeeWorkingData(EmployeeWorkingDataDto model)
        {
            var employee = await employeeRepository
                  .Where(d => d.EmployeeId == model.NationalId)
                  .FirstOrDefaultAsync();
            if (employee.IsNull())
            {
                return (int)App.Global.Enums.ResultStatusCode.NotFound;
            }
            foreach (var item in model.Departments)
            {
                var department = await departmentRepository.GetAsync(item);
                employee.DepartmentEmployeeT.Add(new DepartmentEmployeeT
                {
                    DepartmentId = item,
                    EmployeeId = model.NationalId,
                    DepartmentName = department.DepartmentName,
                    Percentage = department.DepartmentPercentage
                });
            }
            var city = await cityRepository.GetAsync(model.CityId);
            employee.EmployeeWorkplacesT.Add(new EmployeeWorkplacesT
            {
                BranchId = city.BranchId.GetValueOrDefault(1),
                EmployeeId = model.NationalId
            });
            employee.OpreationT = new OpreationT
            {
                IsActive = false,
                LastActiveTime = DateTime.Now.EgyptTimeNow(),
            };
            employee.IsDataComplete = true;
            return await employeeRepository.SaveAsync();
        }

        public async Task<Result<EmployeeRegisterStepDto>> GetEmployeeRegisterStep(string nationalId)
        {
            var employee = await employeeRepository
               .Where(d => d.EmployeeId == nationalId)
               .Include(d => d.EmployeeWorkplacesT)
               .Include(d => d.DepartmentEmployeeT)
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
            var account = await accountService.Get(GeneralSetting.EmployeeAccountTypeId, nationalId);
            if (account.IsNull())
            {
                account = await RegisterAccount(nationalId, employee.EmployeePhone, nationalId, GeneralSetting.EmployeeAccountTypeId, 600, GeneralSetting.EmployeeAppDefaultRoleId, isActive: false);
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
            if (string.IsNullOrEmpty(employee.EmployeeRelativeName) || string.IsNullOrEmpty(employee.EmployeeRelativePhone))
            {
                return ResultFactory<EmployeeRegisterStepDto>.CreateSuccessResponse(new EmployeeRegisterStepDto
                {
                    NationalId = nationalId,
                    AccountId = account.AccountId,
                    NextStep = (int)Domain.Enum.EmployeeRegisterStep.CompleteNationalAndRelative,
                    NextStepDescription = Domain.Enum.EmployeeRegisterStep.CompleteNationalAndRelative.ToString()
                });
            }
            if(string.IsNullOrEmpty(employee.EmployeeGov) || string.IsNullOrEmpty(employee.EmployeeCity) || string.IsNullOrEmpty(employee.EmployeeRegion)
                || string.IsNullOrEmpty(employee.EmployeeStreet) || employee.EmployeeBlockNum.IsNull() || employee.EmployeeFlatNum.IsNull())
            {
                return ResultFactory<EmployeeRegisterStepDto>.CreateSuccessResponse(new EmployeeRegisterStepDto
                {
                    NationalId = nationalId,
                    AccountId = account.AccountId,
                    NextStep = (int)Domain.Enum.EmployeeRegisterStep.CompleteAddress,
                    NextStepDescription = Domain.Enum.EmployeeRegisterStep.CompleteAddress.ToString()
                });
            }
            if (employee.DepartmentEmployeeT.IsEmpty())
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
    }
}
