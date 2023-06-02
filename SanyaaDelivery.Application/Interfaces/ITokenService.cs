using App.Global.DTOs;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AccountT account);
        Task<Result<string>> RenewTokenAsync(RenewTokenDto model, bool skipCheckSignature = false);

        Task<int> AddAsync(TokenT token);
    }
}
