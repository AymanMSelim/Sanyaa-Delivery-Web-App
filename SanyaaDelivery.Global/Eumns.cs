using System;
using System.Collections.Generic;
using System.Text;

namespace App.Global
{
    public class Eumns
    {
        public enum TokenType
        {
            None,
            Bearer
        }

        public enum FawryPaymentMethod
        {
            PAYATFAWRY
        }
        public enum FawryOrderStatus
        {
            NEW,
            PAID,
            UNPAID
        }

        public enum ResponseStatusCode
        {
            Exception = -13,
            OTPExpired = -12,
            InvalidUserOrPassword = -11,
            NoRoleFound = -10,
            InvalidOTP = -9,
            InvalidSignature = -8,
            AccountSuspended = -7,
            MobileVerificationRequired = -6,
            NullableObject = -5,
            EmptyData = -4,
            InvalidData = -3,
            NotFound = -2,
            Failed = -1,
            None = 0,
            Success = 1,
            AlreadyExist = 2,
            RecordAddedSuccessfully = 3,
            RecordUpdatedSuccessfully = 4,
            RecordDeletedSuccessfully = 5,
            ClientRegisterdSuccessfully = 6,
        }
    }
}
