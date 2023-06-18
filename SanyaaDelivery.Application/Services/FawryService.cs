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
using App.Global.DateTimeHelper;
using App.Global.ExtensionMethods;
using SanyaaDelivery.Domain.DTOs;
using App.Global.DTOs;
using SanyaaDelivery.Domain;
using Microsoft.EntityFrameworkCore;

namespace SanyaaDelivery.Application.Services
{
    public class FawryService : IFawryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRequestService requestService;
        private readonly IEmployeeService employeeService;
        private readonly IConfiguration configuration;
        private readonly IFawryChargeService fawryChargeService;
        private readonly IRepository<RequestT> requestRepository;
        private readonly INotificatonService notificatonService;
        private readonly IRequestUtilityService requestUtilityService;
        private readonly IRepository<FawryChargeT> fawryChargeRepository;
        private readonly IRepository<MessagesT> messageRepository;
        private readonly IEmployeeAppAccountService employeeAppAccountService;
        private readonly IFawryAPIService fawryAPIService;

        public FawryService(IRequestService requestService, IEmployeeService employeeService, IConfiguration configuration, 
            IFawryAPIService fawryAPIService, IFawryChargeService fawryChargeService, IRepository<RequestT> requestRepository, INotificatonService notificatonService,
            IRequestUtilityService requestUtilityService, IRepository<FawryChargeT> fawryChargeRepository, IRepository<MessagesT> messageRepository,
            IEmployeeAppAccountService employeeAppAccountService, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.requestService = requestService;
            this.employeeService = employeeService;
            this.configuration = configuration;
            this.fawryChargeService = fawryChargeService;
            this.requestRepository = requestRepository;
            this.notificatonService = notificatonService;
            this.requestUtilityService = requestUtilityService;
            this.fawryChargeRepository = fawryChargeRepository;
            this.messageRepository = messageRepository;
            this.employeeAppAccountService = employeeAppAccountService;
            this.fawryAPIService = fawryAPIService;
            fawryAPIService.InitialAPI(configuration["FawryAPI"].ToString(), GeneralSetting.SendFawrySMS);
        }

        

        public List<App.Global.Models.Fawry.FawryChargeItem> ConvertRequestToChargeItem(List<RequestT> requestList)
        {
            return requestList.Where(d => d.CompanyPercentageAmount > 0).Select(d => new App.Global.Models.Fawry.FawryChargeItem
            {
                Description = $"Request #{d.RequestId}",
                ItemId = d.RequestId.ToString(),
                Price = Math.Round(d.CompanyPercentageAmount, 2),
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
                CustomerEmail = "ayman.mohamed5100@gmail.com",
                CustomerMobile = employee.EmployeePhone,
                CustomerName = employee.EmployeeName,
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

        public async Task<Result<FawryRefNumberResponse>> SendAllUnpaidRequestAsync(string employeeId, List<RequestT> requestList = null, bool ignoreValidFawryRequest = false)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var employee = await employeeService.GetAsync(employeeId);
                if (employee.IsNull())
                {
                    return ResultFactory<FawryRefNumberResponse>.CreateNotFoundResponse("This employee not found");
                }
                if (requestList.IsNull())
                {
                    requestList = await requestService.GetUnPaidAsync(employeeId, ignoreValidFawryRequest);
                }
                requestList = requestList.Where(d => d.IsPaid == false).ToList();
                if (requestList.IsEmpty())
                {
                    return ResultFactory<FawryRefNumberResponse>.CreateErrorResponseMessage("All request aleady paid");
                }
                if(requestList.Any(d => d.IsCanceled))
                {
                    return ResultFactory<FawryRefNumberResponse>.CreateErrorResponseMessage("There is a canceled request, please review");
                }
                if (requestList.Any(d => d.IsCompleted == false))
                {
                    return ResultFactory<FawryRefNumberResponse>.CreateErrorResponseMessage("There is a an un complete request, please review");
                }
                var chargeItem = ConvertRequestToChargeItem(requestList);
                if (chargeItem.IsEmpty())
                {
                    foreach (var request in requestList)
                    {
                        await requestUtilityService.PayAsync(request.RequestId, 700, 0);
                    }
                    var affecredRows = 0;
                    if (isRootTransaction)
                    {
                        affecredRows = await unitOfWork.CommitAsync(false);
                    }
                    return ResultFactory<FawryRefNumberResponse>.CreateSuccessResponse(message: "All requests auto paid, becasue company percentage <= 0");
                }
                var fawryRequest = PrepareFawryRequest(chargeItem, employee);
                var fawryCharge = new FawryChargeT
                {
                    ChargeAmount = fawryRequest.Amount,
                    ChargeExpireDate = DateTime.Now.AddDays(3),
                    ChargeStatus = App.Global.Enums.FawryRequestStatus.NEW.ToString(),
                    EmployeeId = employeeId,
                    FawryChargeRequestT = requestList.Select(d => new FawryChargeRequestT
                    {
                        RequestId = d.RequestId,
                    }).ToList(),
                    RecordTimestamp = DateTime.Now.EgyptTimeNow(),
                    IsConfirmed = false
                };

                await fawryChargeService.AddAsync(fawryCharge);
                var affectedRows = await unitOfWork.SaveAsync();
                if (affectedRows < 0)
                {
                    return ResultFactory<FawryRefNumberResponse>.CreateErrorResponseMessage("Failed to add fawry charge service");
                }
                fawryRequest.MerchantRefNum = fawryCharge.SystemId;
                fawryAPIService.SetFawryRequest(fawryRequest, configuration["FawrySecurityCode"].ToString());
                var result = await fawryAPIService.GetRefNumberAsync();
                if (result.IsNotNull() && !string.IsNullOrEmpty(result.ReferenceNumber))
                {
                    fawryCharge.FawryRefNumber = long.Parse(result.ReferenceNumber);
                    fawryCharge.IsConfirmed = true;
                    await fawryChargeService.UpdateAsync(fawryCharge);
                    string title = "كود فورى";
                    string body = $"برجاء دفع مستحقاتك {fawryCharge.ChargeAmount} فى فورى الرقم المرجعى {fawryCharge.FawryRefNumber} برجاء السديد لعد ايقاف الحساب";
                    try { await notificatonService.SendFirebaseNotificationAsync(Domain.Enum.AccountType.Employee, employeeId, title, body); } catch { }
                    await messageRepository.AddAsync(new MessagesT
                    {
                        Title = title,
                        Body = body,
                        EmployeeId = employeeId,
                        IsRead = 0,
                        MessageTimestamp = DateTime.Now.EgyptTimeNow()
                    });
                    if (isRootTransaction)
                    {
                        affectedRows = await unitOfWork.CommitAsync(false);
                    }
                    return ResultFactory<FawryRefNumberResponse>.CreateAffectedRowsResult(affectedRows, data: result);
                }
                return ResultFactory<FawryRefNumberResponse>.CreateErrorResponse(date: result);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return ResultFactory<FawryRefNumberResponse>.CreateExceptionResponse(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
           
        }

        public async Task<Result<List<FawryRefNumberResponse>>> SendAllUnpaidRequestAsync(bool ignoreValidFawryRequest = false)
        {
            List<FawryRefNumberResponse> response = new List<FawryRefNumberResponse>();
            var allNotPaid = await requestUtilityService.GetNotPaidSummaryAsync();
            if (allNotPaid.IsEmpty())
            {
                return ResultFactory<List<FawryRefNumberResponse>>.CreateNotFoundResponse("No data found");
            }
            foreach (var item in allNotPaid)
            {
                var result = await SendAllUnpaidRequestAsync(item.EmployeeId, ignoreValidFawryRequest: ignoreValidFawryRequest);
                if (result.IsSuccess)
                {
                    response.Add(result.Data);
                }
            }
            return ResultFactory<List<FawryRefNumberResponse>>.CreateSuccessResponse(response);
        }

        public Task<FawryRefNumberResponse> SendRequest(int requestId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<FawryRefNumberResponse>> SendUnpaidRequestAsync(GenerateRefNumberForRequestDto model)
        {
            if (model.RequestList.IsEmpty())
            {
                return ResultFactory<FawryRefNumberResponse>.CreateErrorResponseMessageFD("No requests selected");
            }
            var requestList = await requestRepository
                .Where(d => model.RequestList.Contains(d.RequestId) && d.IsPaid == false && d.IsCompleted && d.IsCanceled == false)
                .ToListAsync();
            if(requestList.Any(d => d.EmployeeId != model.EmployeeId))
            {
                return ResultFactory<FawryRefNumberResponse>.CreateErrorResponseMessageFD("One or more request not belong to this employee");
            }
            if (requestList.IsEmpty())
            {
                return ResultFactory<FawryRefNumberResponse>.CreateErrorResponseMessageFD("No unpaid request found");
            }
            return await SendAllUnpaidRequestAsync(model.EmployeeId, requestList);
        }

        public async Task UpdateStatusTask()
        {
            var list = await fawryChargeRepository.Where(d => d.ChargeStatus == App.Global.Enums.FawryRequestStatus.NEW.ToString() ||
            d.ChargeStatus == App.Global.Enums.FawryRequestStatus.UNPAID.ToString())
                .Include(d => d.FawryChargeRequestT)
                .ToListAsync();
            if (list.IsEmpty()) { return; }
            foreach (var item in list)
            {
                await CheckUpdateFawryChargeAsync(item);
            }
        }

        public async Task UpdateEmployeeValidChargeAsync(string employeeId)
        {
            var list = await fawryChargeRepository.Where(d => d.EmployeeId == employeeId &&
            (d.ChargeStatus == App.Global.Enums.FawryRequestStatus.NEW.ToString() || d.ChargeStatus == App.Global.Enums.FawryRequestStatus.UNPAID.ToString()))
                .Include(d => d.FawryChargeRequestT)
                .ToListAsync();
            if (list.IsEmpty()) { return; }
            foreach (var item in list)
            {
                await CheckUpdateFawryChargeAsync(item);
            }
        }

        public async Task<int> PayChargeAsync(FawryChargeT fawryCharge)
        {
            if (fawryCharge.IsNull() || fawryCharge.FawryChargeRequestT.IsEmpty())
            {
                return (int)App.Global.Enums.ResultStatusCode.Failed;
            }
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                foreach (var item in fawryCharge.FawryChargeRequestT)
                {
                    var result = await requestUtilityService.PayAsync(item.RequestId, 700, null, false);
                    await unitOfWork.SaveAsync();
                    if (result.IsFail)
                    {
                        return result.StatusCode;
                    }
                }
                bool haveUnPaidRequest = await requestUtilityService.IsHaveUnPaidRequestExceed3Days(fawryCharge.EmployeeId);
                if (haveUnPaidRequest == false)
                {
                    await employeeAppAccountService.ActiveAccountAsync(fawryCharge.EmployeeId);
                }
                if (isRootTransaction)
                {
                    return await unitOfWork.CommitAsync(false);
                }
                return (int)App.Global.Enums.ResultStatusCode.Success;
            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
                unitOfWork.RollBack();
                return (int)App.Global.Enums.ResultStatusCode.Exception;
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
        }

        public async Task CheckUpdateFawryChargeAsync(FawryChargeT fawryCharge)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var result = await fawryAPIService.GetStatusAsync(fawryCharge.SystemId,
                configuration["FawryMarchantCode"].ToString(),
                configuration["FawrySecurityCode"].ToString());
                if (result.IsNull())
                {
                    return;
                }
                if (result.StatusCode == 200 && string.IsNullOrEmpty(result.ReferenceNumber) == false)
                {

                    if(fawryCharge.ChargeStatus == result.PaymentStatus) { return; }
                    fawryCharge.ChargeStatus = result.PaymentStatus;
                    fawryChargeRepository.Update(fawryCharge.SystemId, fawryCharge);
                    if (result.PaymentStatus.ToLower() == App.Global.Enums.FawryRequestStatus.PAID.ToString().ToLower())
                    {
                        var payResult = await PayChargeAsync(fawryCharge);
                        if(payResult <= 0)
                        {
                            return;
                        }
                    }
                }
                if (isRootTransaction)
                {
                    await unitOfWork.CommitAsync(false);
                }
            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
                unitOfWork.RollBack();
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
        }

        private bool IsNotified(FawryChargeT charge)
        {
            if(charge.ChargeStatus == App.Global.Enums.FawryRequestStatus.NEW.ToString() ||
                charge.ChargeStatus == App.Global.Enums.FawryRequestStatus.UNPAID.ToString())
            {
                return false;
            }
            return true;
        }
        public async Task<int> CallbackNotification(FawryNotificationCallback model)
        {
            if(Convert.ToInt32(model.MerchantRefNumber) < 14000)
            {
                return (int)App.Global.Enums.ResultStatusCode.Success;
            }
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var charge = await fawryChargeRepository.Where(d => d.SystemId == (Convert.ToInt32(model.MerchantRefNumber)))
                    .Include(d => d.FawryChargeRequestT)
                    .FirstOrDefaultAsync();
                if (IsNotified(charge))
                {
                    return (int)App.Global.Enums.ResultStatusCode.Success;
                }
                charge.ChargeStatus = model.OrderStatus;
                if (charge.ChargeStatus.ToLower() == App.Global.Enums.FawryRequestStatus.PAID.ToString().ToLower())
                {
                    var result = await PayChargeAsync(charge);
                    if(result <= 0)
                    {
                        return (int)App.Global.Enums.ResultStatusCode.Failed;
                    }
                }
                fawryChargeRepository.Update(charge.SystemId, charge);
                if (isRootTransaction)
                {
                    return await unitOfWork.CommitAsync(false);
                }
                return (int)App.Global.Enums.ResultStatusCode.Success;
            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
                return (int)App.Global.Enums.ResultStatusCode.Exception;
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
           
        }
    }
}
