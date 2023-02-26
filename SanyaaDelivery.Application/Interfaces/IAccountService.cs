using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IAccountService
    {
        Task<int> DeleteSoft(int id);

        Task<AccountT> Get(int id);

        Task<AccountT> Get(int accountType, string referenceId);

        Task<int> Add(AccountT account);

        Task<int> Update(AccountT account);

        Task<int> UpdatePassword(AccountT account, string newPassword);

        bool IsMaxResetPasswordPerDayReached(AccountT account);

        Task<int> ResetPassword(AccountT account);

        bool IsMaxOtpPerDayReached(AccountT account);

        bool IsOtpExpired(AccountT account);

        Task<int> ConfirmResetPasswordOtp(AccountT account);

        Task<int> ConfirmRegisterOtp(AccountT account);
    }
}
