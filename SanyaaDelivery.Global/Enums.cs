﻿using System;
using System.Collections.Generic;
using System.Text;

namespace App.Global
{
    public class Enums
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
        public enum FawryRequestStatus
        {
            NEW,
            PAID,
            UNPAID,
            EXPIRED,
            PROCESSED
        }

        public enum ResultAleartType
        {
            None = 0,
            SuccessToast = 1,
            FailedToast = 2,
            SuccessDialog = 3,
            FailedDialog = 4,
            RegistrationRequired = 5
        }

        public enum ResultStatusCode
        {
            NotAuthenticated = -27,
            TokenExpired = -26,
            NotAuthorized = -25,
            False = -24,
            DeleteFailed = -22,
            NotAvailable = -21,
            IncompleteClientData = -20,
            Mismatched = -19,
            MaximumCountReached = -18,
            ResetPasswordNotRequested = -17,
            EmailVerificationRequired = -16,
            ResetPasswordRequired = -15,
            ModelNotValid = -14,
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
            Allowed = 7,
            True = 8,
            NotAllowed = 9,
        }

        public enum TransactionType
        {
            None = 0,
            Expenses = 1
        }
    }
}
