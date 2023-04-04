using Microsoft.Extensions.Configuration;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanyaaDelivery.Application
{
    public interface IGeneralSetting
    {
        int CurrentClientId { get; set; }
        bool CurrentIsViaApp { get; set; }
    }

    public class GeneralSetting : IGeneralSetting
    {
        public int CurrentClientId { get; set; }
        public bool CurrentIsViaApp { get; set; }
        public static int CustomerAccountTypeId
        {
            get
            {
                return AccountTypeList.Where(a => a.AccountTypeName == "Customer").FirstOrDefault().AccountTypeId;
            }
        }

        public static int EmployeeAccountTypeId
        {
            get
            {
                return AccountTypeList.Where(a => a.AccountTypeName == "Employee").FirstOrDefault().AccountTypeId;
            }
        }

        public static int SystemUserAccountTypeId
        {
            get
            {
                return AccountTypeList.Where(a => a.AccountTypeName == "SystemUser").FirstOrDefault().AccountTypeId;
            }
        }

        public static int CustomerRoleId
        {
            get
            {
                return RoleList.Where(a => a.RoleName == "CustomerApp").FirstOrDefault().RoleId;
            }
        }

        public static int GuestRoleId
        {
            get
            {
                return RoleList.Where(a => a.RoleName == "Guest").FirstOrDefault().RoleId;
            }
        }

        public static int EmployeeRoleId
        {
            get
            {
                return RoleList.Where(a => a.RoleName == "EmployeeApp").FirstOrDefault().RoleId;
            }
        }

        public static int OTPExpireMinutes { get; set; }

        static Interfaces.IAccountTypeService _accountTypeService;
        static Interfaces.IRoleService _roleService;
        private readonly IAppSettingService appSettingService;

        private static List<AccountTypeT> _accountTypeList;

        private static List<RoleT> _roleList;

        private static List<AppSettingT> _appSettingList;

        private static List<TranslatorT> _translationList;

        private static List<RequestStatusT> _requestStatusList;

        public static List<RoleT> RoleList { get => _roleList; private set => _roleList = value; }

        public static List<AccountTypeT> AccountTypeList { get => _accountTypeList; private set => _accountTypeList = value; }
        
        public static List<RequestStatusT> RequestStatusList { get => _requestStatusList; private set => _requestStatusList = value; }

        public static List<AppSettingT> AppSetting { get => _appSettingList; private set => _appSettingList = value; }

        public static List<TranslatorT> TranslationList { get => _translationList; private set => _translationList = value; }

        public GeneralSetting(Interfaces.IAccountTypeService accountTypeService, 
            Interfaces.IRoleService roleService, Interfaces.IAppSettingService appSettingService, IRequestStatusService requestStatusService,
            IConfiguration configuration)
        {
            _accountTypeService = accountTypeService;
            _roleService = roleService;
            this.appSettingService = appSettingService;
            if (_roleList == null || _roleList.Count == 0)
            {
                _roleList = _roleService.GetList().Result;
            }
            if (_accountTypeList == null || _accountTypeList.Count == 0)
            {
                _accountTypeList = _accountTypeService.GetList().Result;
            }
            if (_appSettingList == null || _appSettingList.Count == 0)
            {
                _appSettingList = appSettingService.GetListAsync().Result;
            }
            if(_requestStatusList == null)
            {
                _requestStatusList = requestStatusService.GetListAsync().Result;
            }
            OTPExpireMinutes = configuration.GetValue<int>("OTPExpireMinutes");
        }

        public static sbyte GetRequestStatusId(string statusName)
        {
            return RequestStatusList.FirstOrDefault(d => d.RequestStatusName.ToLower() == statusName.ToLower())
                .RequestStatusId;
        }
        
        public const int TokenExpireInDays = 30;
        
        public const int CustomerAppSystemUserId = 500;

        public const int EmployeeAppSystemUserId = 900;

        public const int SystemUserDefaultRoleId = 1;

        public const int CustomerAppDefaultRoleId = 2;

        public const int EmployeeAppDefaultRoleId = 3;

        private const int DefaulPasswordResetCountPerDay = 5;

        private const int DefaulOtpCountPerDay = 10;

        private const int DefaultMinimumCharge = 80;

        private const int DefaultDeliveryPrice = 15;

        private const int DefaultPointsPerEGP = 15;

        private const decimal DefaultPromocodeCompanyDiscountPercentage = 100;
        private const decimal DefaultPointsCompanyDiscountPercentage = 100;

        private const int DefaultCity = 11;

        private const decimal DefaultEmployeePercentage = 70;

        private const int DefaultCleaningDepartmentId = 12;

        private const int DefaultRequestExcutionHours = 3;
         
        public static bool IsEmailVerificationRequired
        {
            get
            {
                var setting = AppSetting.FirstOrDefault(d => d.SettingKey == "RequireEmailVerification");
                if (setting == null || string.IsNullOrEmpty(setting.SettingValue))
                {
                    return false;
                }
                return Convert.ToBoolean(setting.SettingValue);
            }
        }

        public static int PasswordResetCountPerDay
        {
            get
            {
                var setting = AppSetting.FirstOrDefault(d => d.SettingKey == "LastPasswordResetCountPerDay");
                if (setting == null || string.IsNullOrEmpty(setting.SettingValue))
                {
                    return DefaulPasswordResetCountPerDay;
                }
                return Convert.ToInt32(setting.SettingValue);
            }
        }

        public static int OtpCountPerDay
        {
            get
            {
                var setting = AppSetting.FirstOrDefault(d => d.SettingKey == "OtpCountPerDay");
                if (setting == null || string.IsNullOrEmpty(setting.SettingValue))
                {
                    return DefaulOtpCountPerDay;
                }
                return Convert.ToInt32(setting.SettingValue);
            }
        }

        public static int MinimumCharge
        {
            get
            {
                var setting = AppSetting.FirstOrDefault(d => d.SettingKey == "MinimumCharge");
                if (setting == null || string.IsNullOrEmpty(setting.SettingValue))
                {
                    return DefaultMinimumCharge;
                }
                return Convert.ToInt32(setting.SettingValue);
            }
        }

        public static int DeliveryPrice
        {
            get
            {
                var setting = AppSetting.FirstOrDefault(d => d.SettingKey == "DeliveryPrice");
                if (setting == null || string.IsNullOrEmpty(setting.SettingValue))
                {
                    return DefaultDeliveryPrice;
                }
                return Convert.ToInt32(setting.SettingValue);
            }
        }

        public static int PointsPerEGP
        {
            get
            {
                var setting = AppSetting.FirstOrDefault(d => d.SettingKey == "PointsPerEGP");
                if (setting == null || string.IsNullOrEmpty(setting.SettingValue))
                {
                    return DefaultPointsPerEGP;
                }
                return Convert.ToInt32(setting.SettingValue);
            }
        }

        public static decimal PromocodeCompanyDiscountPercentage
        {
            get
            {
                var setting = AppSetting.FirstOrDefault(d => d.SettingKey == "PromocodeCompanyDiscountPercentage");
                if (setting == null || string.IsNullOrEmpty(setting.SettingValue))
                {
                    return DefaultPromocodeCompanyDiscountPercentage;
                }
                return Convert.ToDecimal(setting.SettingValue);
            }
        }
        public static decimal PointsCompanyDiscountPercentage
        {
            get
            {
                var setting = AppSetting.FirstOrDefault(d => d.SettingKey == "PointsCompanyDiscountPercentage");
                if (setting == null || string.IsNullOrEmpty(setting.SettingValue))
                {
                    return DefaultPointsCompanyDiscountPercentage;
                }
                return Convert.ToDecimal(setting.SettingValue);
            }
        }

        public static int DefaultCityId
        {
            get
            {
                var setting = AppSetting.FirstOrDefault(d => d.SettingKey == "DefaultCity");
                if (setting == null || string.IsNullOrEmpty(setting.SettingValue))
                {
                    return DefaultCity;
                }
                return Convert.ToInt32(setting.SettingValue);
            }
        }

        public static decimal EmployeePercentage
        {
            get
            {
                var setting = AppSetting.FirstOrDefault(d => d.SettingKey == "DefaultEmployeePercentage");
                if (setting == null || string.IsNullOrEmpty(setting.SettingValue))
                {
                    return DefaultEmployeePercentage;
                }
                return Convert.ToDecimal(setting.SettingValue);
            }
        }


        public static int CleaningDepartmentId
        {
            get
            {
                var setting = AppSetting.FirstOrDefault(d => d.SettingKey == "CleaningDepartmentId");
                if (setting == null || string.IsNullOrEmpty(setting.SettingValue))
                {
                    return DefaultCleaningDepartmentId;
                }
                return Convert.ToInt32(setting.SettingValue);
            }
        }
         public static int RequestExcutionHours
        {
            get
            {
                var setting = AppSetting.FirstOrDefault(d => d.SettingKey == "RequestExcutionHours");
                if (setting == null || string.IsNullOrEmpty(setting.SettingValue))
                {
                    return DefaultRequestExcutionHours;
                }
                return Convert.ToInt32(setting.SettingValue);
            }
        }

        public static int CurrentRequest { get; set; }
    }
}
