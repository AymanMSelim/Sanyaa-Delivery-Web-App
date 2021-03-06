using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(string userId, string username, List<AccountRoleT> roles);
    }
}
