using App.Global.DTOs;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Domain.OtherModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IHelperService
    {
        public bool IsViaApp { get; set; }
        public bool IsViaClientApp { get; set; }
        public bool IsViaEmpApp { get; set; }

        Task<decimal> GetMinimumChargeAsync(int? cityId = null, int? departmentId = null);
        Task<Result<string>> ValidateClientSubscription(int clientSubscriptionId);
        Result<string> ValidateClientSubscription(ClientSubscriptionT clientSubscription);
        Task<decimal> GetDeliveryPriceAsync(int? cityId = null, int? region = null, int? departmentId = null);
        Result<T> ValidateRequest<T>(RequestT request, string employeeId = null, bool checkEmployee = false);
        Result<T> ValidateFollowUpRequest<T>(RequestT request);
        void SetHost(string host);
        void SetSystemUser(int systemUserId);
        int GetSystemUser();
        string GetHost();
        DepartmentTimeWhereBetween GetDepartmentTimeBetween(int departmentId, DateTime dateTime);
        DepartmentTimeWhereBetween GetDepartmentTimeBetween(List<int> departmentIdList, DateTime dateTime);
        List<sbyte> GetNotAssignStatusList();
        List<sbyte> GetExcutionStatusList();

        void SetViaApp(bool isViaApp, bool isViaClientApp, bool isViaEmpApp);
    }
}
