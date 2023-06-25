using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class AccountT
{
    public int AccountId { get; set; }

    public string AccountUsername { get; set; }

    public string AccountPassword { get; set; }

    public string AccountHashSlat { get; set; }

    public int AccountTypeId { get; set; }

    public string AccountReferenceId { get; set; }

    public string AccountSecurityCode { get; set; }

    public string MobileOtpCode { get; set; }

    public ulong? IsMobileVerfied { get; set; }

    public string EmailOtpCode { get; set; }

    public DateTime? LastOtpCreationTime { get; set; }

    public sbyte OtpCountWithinDay { get; set; }

    public ulong? IsEmailVerfied { get; set; }

    public ulong? IsPasswordReseted { get; set; }

    public string ResetPasswordToken { get; set; }

    public DateTime? LastResetPasswordRequestTime { get; set; }

    public sbyte PasswordResetCountWithinDay { get; set; }

    public string FcmToken { get; set; }

    public string Description { get; set; }

    public ulong? IsActive { get; set; }

    public DateTime CreationDate { get; set; }

    public int SystemUserId { get; set; }

    public ulong IsDeleted { get; set; }

    public virtual ICollection<AccountRoleT> AccountRoleT { get; set; } = new List<AccountRoleT>();

    public virtual AccountTypeT AccountType { get; set; }

    public virtual ICollection<FirebaseCloudT> FirebaseCloudT { get; set; } = new List<FirebaseCloudT>();

    public virtual SystemUserT SystemUser { get; set; }

    public virtual ICollection<TokenT> TokenT { get; set; } = new List<TokenT>();
}
