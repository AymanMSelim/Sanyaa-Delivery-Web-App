using App.Global.Interfaces;
using App.Global.Models.Fawry;
using Microsoft.Extensions.Configuration;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class FawryService : IFawryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRequestService requestService;
        private readonly IEmployeeService employeeService;
        private readonly IConfiguration configuration;
        private readonly IFawryChargeService fawryChargeService;
        private readonly IFawryAPIService fawryAPIService;

        public FawryService(IUnitOfWork unitOfWork, IRequestService requestService, IEmployeeService employeeService, IConfiguration configuration, 
            IFawryAPIService fawryAPIService, IFawryChargeService fawryChargeService)
        {
            this.unitOfWork = unitOfWork;
            this.requestService = requestService;
            this.employeeService = employeeService;
            this.configuration = configuration;
            this.fawryChargeService = fawryChargeService;
            this.fawryAPIService = fawryAPIService;
        }
        public List<App.Global.Models.Fawry.FawryChargeItem> ConvertRequestToChargeItem(List<RequestT> requestList)
        {
            return requestList.Select(d => new App.Global.Models.Fawry.FawryChargeItem
            {
                Description = $"Request #{d.RequestId}",
                ItemId = d.RequestId.ToString(),
                Price = Math.Round(Convert.ToDecimal(d.RequestStagesT.Cost), 2),
                Quantity = 1
            }).ToList();
        }

        public App.Global.Models.Fawry.FawryRequest PrepareFawryRequest(List<App.Global.Models.Fawry.FawryChargeItem> fawryChargeItems, EmployeeT employee)
        {
            App.Global.Models.Fawry.FawryRequest fawryRequest = new App.Global.Models.Fawry.FawryRequest
            {
                Amount = Math.Round(fawryChargeItems.Sum(d => d.Price), 2),
                ChargeItems = fawryChargeItems,
                Description = $@"Request #{string.Join(",", fawryChargeItems.Select(d => d.ItemId).ToList())}",
                CustomerEmail = "ayman.mohaned5100@gmail.com",
                CustomerMobile = employee.EmployeePhone,
                CustomerName = employee.EmployeeName,
                //CustomerProfileId = Convert.ToInt32(employee.EmployeeId),
                CustomerProfileId = employee.EmployeeId,
                MerchantCode = configuration["FawryMarchantCode"].ToString(),
                CurrencyCode = "EGP",
                Language = "ar-eg",
                MerchantRefNum = 66666,
                PaymentExpiry = (long)DateTime.Now.AddDays(3).Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                PaymentMethod = App.Global.Enums.FawryPaymentMethod.PAYATFAWRY.ToString(),
            };
            return fawryRequest;
        }

        public async Task<FawryRefNumberResponse> SendAllUnpaidRequestAsync(string employeeId)
        {
            unitOfWork.StartTransaction();
            var employee = await employeeService.GetAsync(employeeId);
            var requestList = await requestService.GetUnPaidAsync(employeeId);
            var chargeItem = ConvertRequestToChargeItem(requestList);
           
            var fawryRequest = PrepareFawryRequest(chargeItem, employee);
            var fawryCharge = new FawryChargeT
            {
                ChargeAmount = fawryRequest.Amount,
                ChargeExpireDate = DateTime.Now.AddDays(3),
                ChargeStatus = App.Global.Enums.FawryRequestStatus.NEW.ToString(),
                EmployeeId = employeeId,
                FawryChargeRequestT = requestList.Select(d => new FawryChargeRequestT
                {
                    RequestId = d.RequestId
                }).ToList(),
                RecordTimestamp = DateTime.Now,
                IsConfirmed = false
            };
            var affectedRows = await fawryChargeService.AddAsync(fawryCharge);
            if(affectedRows <= 0)
            {
                return null;
            }
            fawryRequest.MerchantRefNum = fawryCharge.SystemId;
            fawryAPIService.SetFawryRequest(fawryRequest, configuration["FawrySecurityCode"].ToString());
            var result = await fawryAPIService.GetRefNumberAsync();
            if(result != null && !string.IsNullOrEmpty(result.ReferenceNumber))
            {
                fawryCharge.FawryRefNumber = long.Parse(result.ReferenceNumber);
                fawryCharge.IsConfirmed = true;
                await fawryChargeService.UpdateAsync(fawryCharge);
                await unitOfWork.CommitAsync();
            }
            return result;
        }

        public Task<FawryRefNumberResponse> SendRequest(int requestId)
        {
            throw new NotImplementedException();
        }
    }
}
