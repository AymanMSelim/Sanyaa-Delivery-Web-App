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
        Task<Result<ClientRegisterResponseDto>> CRegisterClient(ClientRegisterDto clientRegisterDto);
        Task<Result<ClientRegisterResponseDto>> CRegisterGuestClient(int guestClientId, ClientRegisterDto clientRegisterDto);
        Task<ClientRegisterResponseDto> RegisterClient(ClientRegisterDto clientRegisterDto);
        Task<Result<EmployeeRegisterResponseDto>> RegisterEmployee(EmployeeRegisterDto employeeRegisterDto, int systemUserId);
        Task<Result<string>> CompleteEmployeePersonalData(string phoneNumber, string nationalId, string relativeName, string realtivePhone,
            byte[] profilePic, byte[] nationalIdFront, byte[] nationalIdBack);

        Task<int> CompleteEmployeeAddress(EmployeeAddressDto model);

        Task<int> CompleteEmployeeWorkingData(EmployeeWorkingDataDto model);
        Task<Result<EmployeeRegisterStepDto>> GetEmployeeRegisterStep(string nationalId);

        Task<GuestClientRegisterResponseDto> RegisterGuest(ClientRegisterDto clientRegisterDto);
        Task<AccountT> RegisterClientAccount(ClientT client, Domain.DTOs.ClientRegisterDto clientRegisterDto);
        Task<AccountT> RegisterAccount(string id, string userName, string password, int accountTypeId, 
            int systemUserId, int roleId, string fcmToken = null, bool isGuest = false, bool isActive = true);

    }
}
