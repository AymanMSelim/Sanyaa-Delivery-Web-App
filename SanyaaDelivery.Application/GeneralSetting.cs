using Microsoft.Extensions.Configuration;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanyaaDelivery.Application
{
    public interface IGeneralSetting
    {

    }

    public class GeneralSetting : IGeneralSetting
    {
        public static int CustomerAccountTypeId { 
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

        private static List<AccountTypeT> _accountTypeList;
        private static List<RoleT> _roleList;

        public static List<RoleT> RoleList { get => _roleList; private set => _roleList = value; }
        public static List<AccountTypeT> AccountTypeList { get => _accountTypeList; private set => _accountTypeList = value; }

        public GeneralSetting(Interfaces.IAccountTypeService accountTypeService, Interfaces.IRoleService roleService, IConfiguration configuration)
        {
            _accountTypeService = accountTypeService;
            _roleService = roleService;
            if(_roleList == null || _roleList.Count == 0)
            {
                _roleList = _roleService.GetList().Result;
            }
            if (_accountTypeList == null || _accountTypeList.Count == 0)
            {
                _accountTypeList = _accountTypeService.GetList().Result;
            }
            OTPExpireMinutes = configuration.GetValue<int>("OTPExpireMinutes");
        }
       
    }
}
