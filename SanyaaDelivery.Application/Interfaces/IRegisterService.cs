using App.Global.DTOs;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IRegisterService
    {
        Task<Result<ClientRegisterResponseDto>> RegisterClientCompleteAsync(ClientRegisterDto clientRegisterDto);
        Task<Result<ClientRegisterResponseDto>> RegisterClientAsync(ClientRegisterDto clientRegisterDto);
        Task<Result<SystemUserDto>> ConfirmClientRegisterOTPAsync(int? clientId, string phone, string otpCode, string signature);
        Task<Result<EmployeeRegisterResponseDto>> RegisterEmployeeAsync(EmployeeRegisterDto employeeRegisterDto, int systemUserId);
        Task<Result<string>> CompleteEmployeePersonalDataAsync(string phoneNumber, string nationalId, string relativeName, string realtivePhone,
             byte[] profilePic, string profileExtention,
            byte[] nationalIdFront, string nationalFrontExtention,
            byte[] nationalIdBack, string nationalBackExtention);

        Task<int> CompleteEmployeeAddressAsync(EmployeeAddressDto model);
        Task<Result<EmployeeT>> CompleteEmployeeWorkingDataAsync(EmployeeWorkingDataDto model);
        Task<Result<EmployeeRegisterStepDto>> GetEmployeeRegisterStepAsync(string nationalId);
        Task<GuestClientRegisterResponseDto> RegisterGuest(ClientRegisterDto clientRegisterDto);
        Task<AccountT> RegisterClientAccountAsync(ClientT client, Domain.DTOs.ClientRegisterDto clientRegisterDto);
        Task<AccountT> RegisterAccountAsync(string id, string userName, string password, int accountTypeId, 
            int systemUserId, int roleId, string fcmToken = null, bool isGuest = false, bool isActive = true, bool requireConfirmMobile = true);
        Task<Result<OTPCodeDto>> ResendOTP(int accountId);
    }
}
