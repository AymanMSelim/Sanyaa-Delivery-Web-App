using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain
{
    public static class GlobalSetting
    {
        private static List<Models.AccountTypeT> _accountTypesList;
        // System user id for the customer app
        public const int CustomerAppUserId = 500;

        // Customer app role
        public const int CustomerAppDefaultRoleId = 2;

        // Employee app role
        public const int EmployeeAppDefaultRoleId = 3;

      
    }
}
