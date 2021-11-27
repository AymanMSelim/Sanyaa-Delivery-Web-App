using SanyaaDelivery.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IRegisterService
    {
        Task<ClientRegisterResponseDto> RegisterClient(Domain.DTOs.ClientRegisterDto clientRegisterDto); 

    }
}
