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
        Task<GuestClientRegisterResponseDto> RegisterGuest(ClientRegisterDto clientRegisterDto);
        Task<AccountT> RegisterClientAccount(ClientT client, Domain.DTOs.ClientRegisterDto clientRegisterDto);
        Task<AccountT> RegisterAccount(string id, string userName, string password, int accountTypeId, int systemUserId, int roleId, string fcmToken = null, bool isGuest = false);

    }
}
