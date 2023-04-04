using App.Global.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
namespace App.Global.JWT
{
    public class TokenHelper
    {
        public static int? GetAccountId(ClaimsIdentity identity)
        {
            var value = GetClaimValue(identity, "AccountId");
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return int.Parse(value);
        }

        public static int? GetAccountType(ClaimsIdentity identity)
        {
            var accountType = GetClaimValue(identity, "AccountType");
            if (!string.IsNullOrEmpty(accountType))
            {
                return int.Parse(accountType);
            }
            return null;
        }

        public static int? GetReferenceId(ClaimsIdentity identity)
        {
            var value = GetClaimValue(identity, "ReferenceId");
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return int.Parse(value);
        }

        public static string GetReferenceIdString(ClaimsIdentity identity)
        {
            var value = GetClaimValue(identity, "ReferenceId");
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return value;
        }

        public static int? GetSystemUserId(ClaimsIdentity identity)
        {
            var value = GetClaimValue(identity, "SystemUserId");
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return int.Parse(value);
        }

        public static string GetClaimValue(ClaimsIdentity identity, string claimName)
        {
            if (identity != null)
            {
                var claimValue = identity.FindFirst(claimName);
                if (claimValue.IsNotNull())
                {
                    return claimValue.Value;
                }
            }
            return null;
        }
    }
}
