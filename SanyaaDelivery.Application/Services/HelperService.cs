using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Domain.OtherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class HelperService : IHelperService
    {
        private readonly IRepository<ClientSubscriptionT> clientSubscriptionRepository;
        private readonly IRepository<DepartmentT> departmentRepository;
        private readonly IRepository<CityT> cityRepository;
        private readonly IRepository<RegionT> regionRepository;
        public string Host { get; private set; }
        public int CurrentSystemUserId { get; set; }

        public bool IsViaApp { get; set; }
        public bool IsViaClientApp { get; set; }
        public bool IsViaEmpApp { get; set; }
        public HelperService(IRepository<ClientSubscriptionT> clientSubscriptionRepository, IRepository<DepartmentT> departmentRepository,
             IRepository<CityT> cityRepository, IRepository<RegionT> regionRepository)
        {
            this.clientSubscriptionRepository = clientSubscriptionRepository;
            this.departmentRepository = departmentRepository;
            this.cityRepository = cityRepository;
            this.regionRepository = regionRepository;
        }
        public async Task<decimal> GetMinimumChargeAsync(int? cityId = null, int? departmentId = null)
        {
            decimal minimumCharge = GeneralSetting.MinimumCharge;
            if (cityId.HasValue)
            {
                var cityMinimumCharge = await cityRepository.Where(d => d.CityId == cityId)
                    .Select(d => d.MinimumCharge).FirstOrDefaultAsync();
                return cityMinimumCharge.GetValueOrDefault((short)minimumCharge);
            }
            return minimumCharge;
        }


        public async Task<decimal> GetDeliveryPriceAsync(int? cityId = null, int? regionId = null, int? departmentId = null)
        {
            decimal deliveryPrice = GeneralSetting.DeliveryPrice;
            if (departmentId.HasValue)
            {
                var isDeliveryPriceIncluded = await departmentRepository.Where(d => d.DepartmentId == departmentId.Value)
                    .Select(d => d.IncludeDeliveryPrice).FirstOrDefaultAsync();
                if(isDeliveryPriceIncluded == false)
                {
                    return 0;
                }
            }
            if (regionId.HasValue)
            {
                var regionDeliveryDetails = await regionRepository.Where(d => d.RegionId == regionId.Value)
                    .Select(d => new { d.IsDeliveryPriceActive, d.DeliveryPrice }).FirstOrDefaultAsync();
                if (regionDeliveryDetails.IsDeliveryPriceActive)
                {
                    return regionDeliveryDetails.DeliveryPrice.GetValueOrDefault(0);
                }
            }
            if (cityId.HasValue)
            {
                var cityDeliveryPrice = await cityRepository.Where(d => d.CityId == cityId)
                    .Select(d => d.DeliveryPrice).FirstOrDefaultAsync();
                return cityDeliveryPrice.GetValueOrDefault(0);
            }
            return deliveryPrice;
        }

        public async Task<Result<string>> ValidateClientSubscription(int clientSubscriptionId)
        {
            var clientSubscription = await clientSubscriptionRepository.GetAsync(clientSubscriptionId);
            return ValidateClientSubscription(clientSubscription);
        }

        public Result<string> ValidateClientSubscription(ClientSubscriptionT clientSubscription)
        {
            if (clientSubscription.IsNull())
            {
                return ResultFactory<string>.CreateErrorResponse(message: "Client subscription not found", resultStatusCode: App.Global.Enums.ResultStatusCode.NotFound);
            }
            if (clientSubscription.IsCanceled)
            {
                return ResultFactory<string>.CreateErrorResponse(message: "This subscription is canceled");
            }
            if (clientSubscription.IsActive is false)
            {
                return ResultFactory<string>.CreateErrorResponse(message: "This subscription not activated, please contact us to activate");
            }
            return ResultFactory<string>.CreateSuccessResponse();
        }

        public Result<T> ValidateRequest<T>(RequestT request, string employeeId = null, bool checkEmployee = false)
        {
            if (request.IsNull())
            {
                return ResultFactory<T>.CreateErrorResponseMessageFD(message: "This request not found");
            }
            if (request.IsCanceled)
            {
                return ResultFactory<T>.CreateErrorResponseMessageFD(message: "This request is canceled");
            }
            if (request.IsCompleted)
            {
                return ResultFactory<T>.CreateErrorResponseMessageFD(message: "This request is completed");
            }
            if (employeeId.IsNotNull() && request.EmployeeId.IsNotNull() && request.EmployeeId != employeeId)
            {
                return ResultFactory<T>.CreateErrorResponseMessageFD(message: "This request not belong to this employee");
            }
            if(checkEmployee && request.EmployeeId != employeeId)
            {
                return ResultFactory<T>.CreateErrorResponseMessageFD(message: "This request not belong to this employee");
            }
            return ResultFactory<T>.CreateSuccessResponse();
        }

        public Result<T> ValidateFollowUpRequest<T>(RequestT request)
        {
            if (request.IsCompleted is false && request.IsCanceled is false)
            {
                return ResultFactory<T>.CreateErrorResponseMessageFD("This request is not completed or canceled to follow up");
            }
            if (request.IsFollowed)
            {
                return ResultFactory<T>.CreateErrorResponseMessageFD("This request is already followed");
            }
            return ResultFactory<T>.CreateSuccessResponse();
        }

        public void SetHost(string host)
        {
            Host = host;
        }

        public string GetHost()
        {
            return Host;
        }

        public DepartmentTimeWhereBetween GetDepartmentTimeBetween(int departmentId, DateTime dateTime)
        {
            return GetDepartmentTimeBetween(new List<int> { departmentId }, dateTime);
        }

        public DepartmentTimeWhereBetween GetDepartmentTimeBetween(List<int> departmentIdList, DateTime dateTime)
        {
            DateTime startTime;
            DateTime endTime;
            if (departmentIdList.Contains(GeneralSetting.CleaningDepartmentId))
            {
                startTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 1);
                endTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 57);
            }
            else
            {
                startTime = dateTime.AddHours(-GeneralSetting.RequestExcutionHours);
                endTime = dateTime.AddHours(GeneralSetting.RequestExcutionHours);
            }
            return new DepartmentTimeWhereBetween { StartTime = startTime, EndTime = endTime };
        }

        public List<sbyte> GetNotAssignStatusList()
        {
            return new List<sbyte>
            {
                GeneralSetting.GetRequestStatusId(Domain.Enum.RequestStatus.Broadcast),
                GeneralSetting.GetRequestStatusId(Domain.Enum.RequestStatus.NotSet),
                GeneralSetting.GetRequestStatusId(Domain.Enum.RequestStatus.Rejected),
                GeneralSetting.GetRequestStatusId(Domain.Enum.RequestStatus.Delayed),
            };
        }

        public void SetSystemUser(int systemUserId)
        {
            CurrentSystemUserId = systemUserId;
        }

        public int GetSystemUser()
        {
            return CurrentSystemUserId;
        }

        public List<sbyte> GetExcutionStatusList()
        {
            return new List<sbyte>
            {
                GeneralSetting.GetRequestStatusId(Domain.Enum.RequestStatus.InExcution),
                GeneralSetting.GetRequestStatusId(Domain.Enum.RequestStatus.StartExcution)
            };
        }

        public void SetViaApp(bool isViaApp, bool isViaClientApp, bool isViaEmpApp)
        {
            this.IsViaApp = isViaApp;
            this.IsViaEmpApp = isViaEmpApp;
            this.IsViaClientApp = isViaClientApp;
        }
    }
}
