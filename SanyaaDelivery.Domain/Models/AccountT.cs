using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class AccountT
    {
        public AccountT()
        {
            AccountRoleT = new HashSet<AccountRoleT>();
        }

        public int AccountId { get; set; }
        public string AccountUsername { get; set; }
        public string AccountPassword { get; set; }
        public string AccountHashSlat { get; set; }
        public int AccountTypeId { get; set; }
        public int? AccountReferenceId { get; set; }
        public string AccountSecurityCode { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public int SystemUserId { get; set; }

        public AccountTypeT AccountType { get; set; }
        public SystemUserT SystemUser { get; set; }
        public ICollection<AccountRoleT> AccountRoleT { get; set; }
    }
}
