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
        Task<ClientRegisterResponseDto> RegisterClient(Domain.DTOs.ClientRegisterDto clientRegisterDto);

        Task<AccountT> RegisterClientAccount(ClientT client, Domain.DTOs.ClientRegisterDto clientRegisterDto);

    }
}
