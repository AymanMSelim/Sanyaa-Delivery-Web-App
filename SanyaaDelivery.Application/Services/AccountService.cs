using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App.Global.ExtensionMethods;
using System.Linq;
using App.Global.DTOs;
using SanyaaDelivery.Domain.DTOs;
using App.Global.DateTimeHelper;

namespace SanyaaDelivery.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<AccountT> accountRepository;

        public AccountService(IRepository<AccountT> accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task<int> Add(AccountT account)
        {
            await accountRepository.AddAsync(account);
            return await accountRepository.SaveAsync();
        }

        public Task<int> Update(AccountT account)
        {
            accountRepository.Update(account.AccountId, account);
            return accountRepository.SaveAsync();
        }

        public Task<AccountT> Get(int id)
        {
            return accountRepository.GetAsync(id);
        }

        public Task<AccountT> Get(int accountType, string referenceId)
        {
            return accountRepository.Where(a => a.AccountTypeId == accountType && a.AccountReferenceId == referenceId).FirstOrDefaultAsync();
        }

        public bool IsMaxResetPasswordPerDayReached(AccountT account)
        {
            if (account.LastResetPasswordRequestTime.IsNotNull())
            {
                if (account.LastResetPasswordRequestTime.Value.Date != DateTime.Now.Date)
                {
                    account.PasswordResetCountWithinDay = 0;
                }
            }
            if (account.PasswordResetCountWithinDay >= GeneralSetting.PasswordResetCountPerDay)
            {
                return true;
            }
            return false;
        }

        public bool IsMaxOtpPerDayReached(AccountT account)
        {
            if (account.LastOtpCreationTime.IsNotNull())
            {
                if (account.LastOtpCreationTime.Value.Date != DateTime.Now.Date)
                {
                    account.OtpCountWithinDay = 0;
                }
            }
            if (account.OtpCountWithinDay >= GeneralSetting.OtpCountPerDay)
            {
                return true;
            }
            return false;
        }

        public Task<int> ResetPassword(AccountT account)
        {
            account.MobileOtpCode = App.Global.Generator.GenerateOTPCode(4);
            account.LastOtpCreationTime = DateTime.Now;
            account.IsPasswordReseted = true;
            account.PasswordResetCountWithinDay++;
            account.LastResetPasswordRequestTime = DateTime.Now;
            account.ResetPasswordToken = Guid.NewGuid().ToString().Replace("-", "");
            return Update(account);
        }

        public bool IsOtpExpired(AccountT account)
        {
            if(DateTime.Now.EgyptTimeNow() > account.LastOtpCreationTime.Value.AddMinutes(GeneralSetting.OTPExpireMinutes))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<int> ConfirmResetPasswordOtp(AccountT account)
        {
            account.IsPasswordReseted = false;
            account.IsMobileVerfied = true;
            return Update(account);
        }

        public Task<int> ConfirmRegisterOtp(AccountT account)
        {
            account.IsMobileVerfied = true;
            account.IsActive = true;
            return Update(account);
        }

        public Task<int> UpdatePassword(AccountT account, string newPassword)
        {
            account.IsPasswordReseted = false;
            account.AccountPassword = App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(account.AccountHashSlat, newPassword);
            return Update(account);
        }

        public async Task<int> DeleteSoft(int id)
        {
            var account = await accountRepository.GetAsync(id);
            account.IsDeleted = true;
            accountRepository.Update(id, account);
            return await accountRepository.SaveAsync();
        }

        public async Task<string> GetOTPAsync(int? accountId = null, int? clientId = null, string employeeId = null)
        {
            string otp;
            if (accountId.HasValue)
            {
                otp = await accountRepository.Where(d => d.AccountId == accountId)
                    .Select(d => d.MobileOtpCode).FirstOrDefaultAsync();
                return otp; 
            }
            if (clientId.HasValue)
            {
                otp = await accountRepository.Where(d => d.AccountReferenceId == clientId.ToString() && d.AccountTypeId == GeneralSetting.CustomerAccountTypeId)
                    .Select(d => d.MobileOtpCode).FirstOrDefaultAsync();
                return otp;
            }
            if (!string.IsNullOrEmpty(employeeId))
            {
                otp = await accountRepository.Where(d => d.AccountReferenceId == employeeId && d.AccountTypeId == GeneralSetting.EmployeeAccountTypeId)
                   .Select(d => d.MobileOtpCode).FirstOrDefaultAsync();
                return otp;
            }
            return null;
        }

        public async Task<Result<bool>> ConfirmOTPAsync(ConfirmOtpDto confirmOtpDto)
        {
            var account = await accountRepository.GetAsync(confirmOtpDto.AccountId);
            if (account.IsNull())
            {
                return ResultFactory<bool>.CreateErrorResponseMessageFD("Account not found");
            }
            if(account.MobileOtpCode != confirmOtpDto.OTPCode)
            {
                return ResultFactory<bool>.CreateErrorResponseMessageFD("Invalid OTP code");
            }
            if (DateTime.Now.EgyptTimeNow() > account.LastOtpCreationTime.Value.AddMinutes(GeneralSetting.OTPExpireMinutes))
            {
                return ResultFactory<bool>.CreateErrorResponseMessageFD("Thid code is expired, please request new one");
            }
            account.IsMobileVerfied = true;
            accountRepository.Update(account.AccountId, account);
            await accountRepository.SaveAsync();
            return ResultFactory<bool>.CreateSuccessResponse(true); 
        }
    }
}
