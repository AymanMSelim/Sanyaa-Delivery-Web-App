using App.Global.DTOs;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Global.DateTimeHelper;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Infra.Data.Context;
using App.Global.ExtensionMethods;

namespace SanyaaDelivery.Application.Services
{
    public class RequestUtiliyService : IRequestUtilityService
    {
        private readonly IRepository<RequestT> requestRepository;
        private readonly IRepository<PaymentT> paymentRepository;
        private readonly ITranslationService translationService;
        private readonly IRepository<FollowUpT> followUpRepository;
        private readonly IRepository<ClientPointT> pointRepository;
        private readonly IRepository<ClientT> clientRepository;
        private readonly IRepository<MessagesT> messageRepository;
        private readonly IEmployeeAppAccountService employeeAppAccountService;
        private readonly IUnitOfWork unitOfWork;

        public RequestUtiliyService(IRepository<RequestT> requestRepository, IRepository<PaymentT> paymentRepository, ITranslationService translationService,
            IRepository<FollowUpT> followUpRepository, IRepository<ClientPointT> pointRepository, IRepository<ClientT> clientRepository,
            IRepository<MessagesT> messageRepository, IEmployeeAppAccountService employeeAppAccountService, IUnitOfWork unitOfWork)
        {
            this.requestRepository = requestRepository;
            this.paymentRepository = paymentRepository;
            this.translationService = translationService;
            this.followUpRepository = followUpRepository;
            this.pointRepository = pointRepository;
            this.clientRepository = clientRepository;
            this.messageRepository = messageRepository;
            this.employeeAppAccountService = employeeAppAccountService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<object>> CompleteAsync(int requestId, int systemUserId)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var request = await requestRepository.DbSet.Where(d => d.RequestId == requestId)
                    .Include(d => d.RequestStagesT).FirstOrDefaultAsync();
                if (request.IsCompleted)
                {
                    return ResultFactory<object>.CreateErrorResponseMessageFD("This request is already complete");
                }
                if (request.IsCanceled)
                {
                    return ResultFactory<object>.CreateErrorResponseMessageFD("This request is canceled");
                }
                if (string.IsNullOrEmpty(request.EmployeeId))
                {
                    return ResultFactory<object>.CreateErrorResponseMessageFD("This request not assign to any employee, can't be set done");
                }
                request.IsCompleted = true;
                request.RequestStagesT.FinishTimestamp = DateTime.Now.EgyptTimeNow();
                request.RequestStatus = GeneralSetting.RequestStatusList
                    .FirstOrDefault(d => d.RequestStatusName.ToLower() == "done").RequestStatusId;
                requestRepository.Update(requestId, request);
                if (request.RequestPoints > 0)
                {
                    await pointRepository.AddAsync(new ClientPointT
                    {
                        ClientId = request.ClientId,
                        CreationDate = DateTime.Now.EgyptTimeNow(),
                        Points = request.RequestPoints,
                        Reason = App.Global.Translation.Translator.STranlate("Complete Request") + $" #{requestId}",
                        PointType = (int)Domain.Enum.ClientPointType.Add,
                        SystemUserId = systemUserId
                    });
                    var client = await clientRepository.GetAsync(request.ClientId);
                    client.ClientPoints += request.RequestPoints;
                    clientRepository.Update(client.ClientId, client);
                }
                int affectedRows = 0;
                if (isRootTransaction)
                {
                    affectedRows = await unitOfWork.CommitAsync(false);
                }
                return ResultFactory<object>.CreateAffectedRowsResult(affectedRows);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<object>(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
           
        }

        public async Task<Result<object>> SetReviewedAsync(int requestId)
        {
            var request = await requestRepository.DbSet.Where(d => d.RequestId == requestId).FirstOrDefaultAsync();
            request.IsReviewed = true;
            requestRepository.Update(request.RequestId, request);
            var affectedRows = await requestRepository.SaveAsync();
            return ResultFactory<object>.CreateAffectedRowsResult(affectedRows);
        }

        public async Task<Result<object>> SetAsUnReviewedAsync(int requestId)
        {
            var request = await requestRepository.DbSet.Where(d => d.RequestId == requestId).FirstOrDefaultAsync();
            request.IsReviewed = false;
            requestRepository.Update(request.RequestId, request);
            var affectedRows = await requestRepository.SaveAsync();
            return ResultFactory<object>.CreateAffectedRowsResult(affectedRows);
        }

        public async Task<Result<object>> ResetRequestAsync(int requestId)
        {
            var request = await requestRepository.DbSet.Where(d => d.RequestId == requestId)
                .Include(d => d.RequestStagesT).FirstOrDefaultAsync();
            request.RequestStatus = GeneralSetting.RequestStatusList.FirstOrDefault(d => d.RequestStatusName.ToLower() == "waiting").RequestStatusId;
            request.RequestStagesT.AcceptTimestamp = null;
            request.RequestStagesT.ReceiveTimestamp = null;
            request.RequestStagesT.FinishTimestamp = null;
            request.RequestStagesT.SentTimestamp = DateTime.Now.EgyptTimeNow();
            request.IsCanceled = false;
            request.IsCompleted = false;
            requestRepository.Update(request.RequestId, request);
            var affectedRows = await requestRepository.SaveAsync();
            return ResultFactory<object>.CreateAffectedRowsResult(affectedRows);
        }

        public async Task<Result<object>> FollowAsync(FollowUpT followUp)
        {
            bool isRootTransaction = false;
            try
            {
                unitOfWork.StartTransaction();
                var request = await requestRepository.DbSet.FirstOrDefaultAsync(d => d.RequestId == followUp.RequestId);
                if (request.IsCompleted is false && request.IsCanceled is false)
                {
                    return ResultFactory<object>.CreateErrorResponseMessageFD("This request is not completed or canceled o follow up");
                }
                if (request.IsFollowed)
                {
                    return ResultFactory<object>.CreateErrorResponseMessageFD("This request is already followed");
                }
                request.IsFollowed = true;
                request.RequestStatus = GeneralSetting.RequestStatusList
                    .FirstOrDefault(d => d.RequestStatusName.ToLower() == "FollowUp").RequestStatusId;
                requestRepository.Update(request.RequestId, request);
                followUp.Timestamp = DateTime.Now.EgyptTimeNow();
                await followUpRepository.AddAsync(followUp);
                int affectedRows = 0;
                if (isRootTransaction)
                {
                    affectedRows = await unitOfWork.CommitAsync(false);
                }
                return ResultFactory<object>.CreateAffectedRowsResult(affectedRows);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<object>(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
        }

        public Task<List<EmployeeNotPaidRequestDto>> GetNotPaidAsync(string employeeId)
        {
            var data = requestRepository.Where(r => r.IsCanceled == false && r.IsCompleted && r.IsPaid == false && r.EmployeeId == employeeId)
                .Include(d => d.RequestStagesT)
                .Select(d => new EmployeeNotPaidRequestDto
                {
                    CompanyPercentage = d.CompanyPercentageAmount,
                    DueTime = d.RequestStagesT.FinishTimestamp.GetValueOrDefault(),
                    EmployeePercentage = d.EmployeePercentageAmount,
                    FinishTime = d.RequestStagesT.FinishTimestamp.GetValueOrDefault(),
                    Price = d.CustomerPrice,
                    RequestId = d.RequestId,
                }).ToListAsync();
            return data;
        }

        public Task<List<EmployeeNotPaidRequestSummaryDto>> GetNotPaidSummaryAsync(DateTime? startTime = null,
            DateTime? endTime = null, int? departmentId = null, string employeeId = null, int? requestId = null)
        {
            var query = requestRepository.Where(r => r.IsCanceled == false && r.IsCompleted && r.IsPaid == false);
            if (startTime.HasValue)
            {
                query = query.Where(d => d.RequestTimestamp >= startTime);
            }
            if (endTime.HasValue)
            {
                query = query.Where(d => d.RequestTimestamp <= endTime);
            }
            if (departmentId.HasValue)
            {
                query = query.Where(d => d.DepartmentId == departmentId);
            }
            if (requestId.HasValue)
            {
                query = query.Where(d => d.RequestId == requestId);
            }
            if (string.IsNullOrEmpty(employeeId) is false)
            {
                query = query.Where(d => d.EmployeeId == employeeId);
            }
            var data = query
                .GroupBy(d => new { d.EmployeeId, d.Employee.EmployeeName, d.Employee.EmployeePhone, AccountState = d.Employee.LoginT.LoginAccountState })
                 .Select(d => new EmployeeNotPaidRequestSummaryDto
                 {
                     EmployeeId = d.Key.EmployeeId,
                     EmployeeName = d.Key.EmployeeName,
                     EmployeePhone = d.Key.EmployeePhone,
                     AccountState = d.Key.AccountState,
                     //IncreaseDiscountTotal = d.IncreaseDiscountT.Sum(s => s.IncreaseDiscountValue),
                     TotalUnPaidRequestCount = d.Count(),
                     TotalCompanyPercentage = d.Sum(s => s.CompanyPercentageAmount),
                     TotalEmployeePercentage = d.Sum(s => s.EmployeePercentageAmount),
                     TotalUnPaidRequestCost = d.Sum(s => s.CustomerPrice)
                 }).ToListAsync();
            return data;
        }

        public async Task<Result<List<PaymentT>>> PayAllAsync(string employeeId, int systemUserId, decimal? amount = null)
        {
            List<PaymentT> paymentList;
            bool isRootTransaction = false;
            try
            {
                paymentList = new List<PaymentT>();
                isRootTransaction = unitOfWork.StartTransaction();
                var requestList = await GetNotPaidAsync(employeeId);
                if (requestList is null || requestList.Count == 0)
                {
                    return ResultFactory<List<PaymentT>>.CreateErrorResponseMessageFD("This employee have no un paid requests");
                }
                foreach (var request in requestList)
                {
                    var result = await PayAsync(request.RequestId, systemUserId, request.CompanyPercentage, false);
                    if (result.IsFail)
                    {
                        return result.Convert(paymentList);
                    }
                    paymentList.Add(result.Data);
                }
                bool haveUnPaidRequest = await IsHaveUnPaidRequestExceed3Days(employeeId);
                if (haveUnPaidRequest == false)
                {
                    await employeeAppAccountService.ActiveAccountAsync(employeeId);
                }
                int affectedRows = 0;
                if (isRootTransaction)
                {
                    affectedRows = await unitOfWork.CommitAsync(false);
                }
                return ResultFactory<List<PaymentT>>.CreateAffectedRowsResult(affectedRows, data: paymentList);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<PaymentT>>(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
        }

        public Task<bool> IsHaveUnPaidRequestExceed3Days(string employeeId)
        {
            return requestRepository.DbSet
                .AnyAsync(d => d.EmployeeId == employeeId && d.IsPaid == false && d.RequestTimestamp.Value <= DateTime.Now.EgyptTimeNow().AddHours(-3));
        }

        public async Task<Result<PaymentT>> PayAsync(int requestId, int systemUserId, decimal? amount = null, bool activeEmployeeAccount = true)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var request = await requestRepository.DbSet.Where(d => d.RequestId == requestId)
                    .Include(d => d.RequestStagesT)
                    .Include(d => d.PaymentT)
                    .FirstOrDefaultAsync();
                if (request.IsCompleted is false)
                {
                    return ResultFactory<PaymentT>.CreateErrorResponseMessageFD("This request not complete");
                }
                if (request.IsCanceled)
                {
                    return ResultFactory<PaymentT>.CreateErrorResponseMessageFD("This request is canceled");
                }
                if (request.IsPaid.HasValue && request.IsPaid.Value)
                {
                    return ResultFactory<PaymentT>.CreateSuccessResponse(message: "This request is already paid");
                }
                request.IsPaid = true;
                request.RequestStagesT.PaymentFlag = true;
                requestRepository.Update(requestId, request);
                await unitOfWork.SaveAsync();
                PaymentT payment = request.PaymentT;
                if (payment.IsNull())
                {
                    payment = new PaymentT
                    {
                        Payment = amount.HasValue ? (double)amount : (double)request.CompanyPercentageAmount,
                        PaymentTimestamp = DateTime.Now.EgyptTimeNow(),
                        RequestId = request.RequestId,
                        SystemUserId = systemUserId
                    };
                    await paymentRepository.AddAsync(payment);
                }
                var message = new MessagesT
                {
                    
                    Title = "دفع طلب #" + requestId,
                    Body = "تم دفع الطلب #" + requestId,
                    EmployeeId = request.EmployeeId,
                    IsRead = 0,
                    MessageTimestamp = DateTime.Now.EgyptTimeNow()
                };
                await messageRepository.AddAsync(message);
                if (activeEmployeeAccount)
                {
                    bool haveUnPaidRequest = await IsHaveUnPaidRequestExceed3Days(request.EmployeeId);
                    if (haveUnPaidRequest == false)
                    {
                        await employeeAppAccountService.ActiveAccountAsync(request.EmployeeId);
                    }
                }
                int affectedRows = 0;
                if (isRootTransaction)
                {
                    affectedRows = await unitOfWork.CommitAsync(false);
                }
                return ResultFactory<PaymentT>.CreateAffectedRowsResult(affectedRows, data: payment);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<PaymentT>(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
        }

        public async Task<EmployeeAppPaymentIndexDto> GetEmployeeAppPaymentIndex(string employeeId)
        {
            DateTime startMonthDate = App.Global.DateTimeHelper.DateTimeHelperService.GetStartDateOfMonthS();
            DateTime endMonthDate = App.Global.DateTimeHelper.DateTimeHelperService.GetEndDateOfMonthS();
            var monthlyRequest = await requestRepository
                .Where(d => d.EmployeeId == employeeId && d.IsCompleted && d.RequestTimestamp >= startMonthDate && d.RequestTimestamp <= endMonthDate)
                .Select(d => new { d.NetPrice, d.EmployeePercentageAmount })
                .ToListAsync();
            var unPaidRequest = await requestRepository.Where(d => d.EmployeeId == employeeId && d.IsCompleted && d.IsPaid == false)
                .Select(d => new EmployeeAppPaymentRequestItemDto
                {
                    Address = d.RequestedAddress.City.CityName + ", " + d.RequestedAddress.Region.RegionName,
                    ClientName = d.Client.ClientName,
                    RequestId = d.RequestId,
                    RequestCaption = translationService.Translate("Request") + " #" + d.RequestId,
                    EmployeeAmountPercentageDescription = translationService.Translate("Maxsab") + " " + d.EmployeePercentageAmount + " ج",
                    CompanyAmountPercentageDescription = translationService.Translate("CompanyPercentage") + " " + d.CompanyPercentageAmount + " ج",
                    RequestTimestamp = d.RequestTimestamp.Value
                }).ToListAsync();
            foreach (var item in unPaidRequest)
            {
                item.DuoDateDecription = translationService.Translate("LastDuoDate") + " " + item.RequestTimestamp.AddDays(3).ToShortDateString();
            }
            return new EmployeeAppPaymentIndexDto
            {
                EmployeeAmountPercentage = monthlyRequest.Sum(d => d.EmployeePercentageAmount).ToString() + " ج",
                EmployeeAmountPercentageCaption = translationService.Translate("Your percentage this month is"),
                EmployeeAmountPercentageDescription = $"لقد قمت بـ {monthlyRequest.Count} مهام بتكلفة كلية {monthlyRequest.Sum(d => d.NetPrice)}ج",
                RequestList = unPaidRequest
            };
        }
    }
}
