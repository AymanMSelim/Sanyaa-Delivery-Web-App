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
using SanyaaDelivery.Domain.Enum;
using System.Reflection.Metadata.Ecma335;
using SanyaaDelivery.Infra.Data.Repositories;
using SanyaaDelivery.Domain.OtherModels;

namespace SanyaaDelivery.Application.Services
{
    public class RequestUtiliyService : IRequestUtilityService
    {
        private readonly IRepository<RequestT> requestRepository;
        private readonly IRepository<PaymentT> paymentRepository;
        private readonly ITranslationService translationService;
        private readonly IEmployeeSubscriptionService employeeSubscriptionService;
        private readonly IRepository<FollowUpT> followUpRepository;
        private readonly IRepository<ClientPointT> pointRepository;
        private readonly IEmployeeRequestService employeeRequestService;
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly IRepository<ClientT> clientRepository;
        private readonly INotificatonService notificatonService;
        private readonly IRepository<RejectRequestT> rejectRequestRepository;
        private readonly IOperationService operationService;
        private readonly IRepository<MessagesT> messageRepository;
        private readonly IEmployeeAppAccountService employeeAppAccountService;
        private readonly IHelperService helperService;
        private readonly IUnitOfWork unitOfWork;

        public RequestUtiliyService(IRepository<RequestT> requestRepository, IRepository<PaymentT> paymentRepository, ITranslationService translationService, IEmployeeSubscriptionService employeeSubscriptionService,
            IRepository<FollowUpT> followUpRepository, IRepository<ClientPointT> pointRepository, IEmployeeRequestService employeeRequestService, IRepository<EmployeeT> employeeRepository,
            IRepository<ClientT> clientRepository, INotificatonService notificatonService, IRepository<RejectRequestT> rejectRequestRepository, IOperationService operationService,
            IRepository<MessagesT> messageRepository, IEmployeeAppAccountService employeeAppAccountService, IHelperService helperService, IUnitOfWork unitOfWork)
        {
            this.requestRepository = requestRepository;
            this.paymentRepository = paymentRepository;
            this.translationService = translationService;
            this.employeeSubscriptionService = employeeSubscriptionService;
            this.followUpRepository = followUpRepository;
            this.pointRepository = pointRepository;
            this.employeeRequestService = employeeRequestService;
            this.employeeRepository = employeeRepository;
            this.clientRepository = clientRepository;
            this.notificatonService = notificatonService;
            this.rejectRequestRepository = rejectRequestRepository;
            this.operationService = operationService;
            this.messageRepository = messageRepository;
            this.employeeAppAccountService = employeeAppAccountService;
            this.helperService = helperService;
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
                var requestValidation = helperService.ValidateRequest<object>(request);
                if (requestValidation.IsFail) { return requestValidation; }
                if (string.IsNullOrEmpty(request.EmployeeId))
                {
                    return ResultFactory<object>.CreateErrorResponseMessageFD("This request not assign to any employee, can't be set done");
                }
                request.IsCompleted = true;
                request.RequestStagesT.FinishTimestamp = DateTime.Now.EgyptTimeNow();
                request.RequestStatus = GeneralSetting.GetRequestStatusId(RequestStatus.Done);
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
                string title = $"طلب #{request.RequestId}";
                try
                {
                    string body = $"عزيزى العميل, تم تنقيذ الطلب الخاص بكم بنجاح فى حالة وجود أى شكاوى برجاء الاتصال بنا";
                    await notificatonService.SendFirebaseNotificationAsync(AccountType.Client, request.ClientId.ToString(), title, body);
                }
                catch { }
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
            request.RequestStatus = GeneralSetting.GetRequestStatusId(RequestStatus.Waiting);
            request.RequestStagesT.AcceptTimestamp = null;
            request.RequestStagesT.ReceiveTimestamp = null;
            request.RequestStagesT.FinishTimestamp = null;
            request.RequestStagesT.SentTimestamp = DateTime.Now.EgyptTimeNow();
            request.IsCanceled = false;
            request.IsCompleted = false;
            request.IsFollowed = false;
            requestRepository.Update(request.RequestId, request);
            var affectedRows = await requestRepository.SaveAsync();
            return ResultFactory<object>.CreateAffectedRowsResult(affectedRows);
        }

        public async Task<Result<FollowUpT>> FollowAsync(FollowUpT followUp)
        {
            bool isRootTransaction = false;
            try
            {
                unitOfWork.StartTransaction();
                var request = await requestRepository.DbSet.FirstOrDefaultAsync(d => d.RequestId == followUp.RequestId);
                var requestValidation = helperService.ValidateFollowUpRequest<FollowUpT>(request);
                if (requestValidation.IsFail)
                {
                    return requestValidation;
                }
                request.IsFollowed = true;
                request.RequestStatus = GeneralSetting.GetRequestStatusId(RequestStatus.FollowUp);
                requestRepository.Update(request.RequestId, request);
                followUp.Timestamp = DateTime.Now.EgyptTimeNow();
                await followUpRepository.AddAsync(followUp);
                int affectedRows = 0;
                if (isRootTransaction)
                {
                    affectedRows = await unitOfWork.CommitAsync(false);
                }
                return ResultFactory<FollowUpT>.CreateAffectedRowsResult(affectedRows);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<FollowUpT>(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
        }

        public async Task<List<EmployeeNotPaidRequestDto>> GetNotPaidAsync(string employeeId)
        {
            var employeeInsuranceInfo = await employeeSubscriptionService.GetEmployeeInsuranceInfoAsync(employeeId);
            var data = await requestRepository.Where(r => r.IsCanceled == false && r.IsCompleted && r.IsPaid == false && r.EmployeeId == employeeId)
                .Include(d => d.RequestStagesT)
                .Select(d => new EmployeeNotPaidRequestDto
                {
                    CompanyPercentage = d.CompanyPercentageAmount,
                    DueTime = d.RequestStagesT.FinishTimestamp.GetValueOrDefault().ToString("yyyy-MM-dd hh:mm tt"),
                    EmployeePercentage = d.EmployeePercentageAmount,
                    FinishTime = d.RequestStagesT.FinishTimestamp.GetValueOrDefault().ToString("yyyy-MM-dd hh:mm tt"),
                    Price = d.CustomerPrice,
                    RequestId = d.RequestId,
                }).ToListAsync();

            if (employeeInsuranceInfo.IsCompleteInsuranceAmount is false)
            {
                foreach (var item in data)
                {
                    var amounts = GetPercentageAfterInsurance(employeeInsuranceInfo.IsCompleteInsuranceAmount, item.EmployeePercentage, item.CompanyPercentage);
                    item.EmployeePercentage = amounts.EmployeeAmountPercentage;
                    item.CompanyPercentage = amounts.CompanyAmountPercentage;
                    item.InsuranceAmount = amounts.InsuranceAmountPercentage;
                }
            }
            if (employeeInsuranceInfo.IsCompleteMinAmount is false)
            {
                data.Add(new EmployeeNotPaidRequestDto
                {
                    RequestId = 0,
                    DueTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt"),
                    EmployeePercentage = 0,
                    FinishTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt"),
                    CompanyPercentage = employeeInsuranceInfo.RemainMinAmount,
                    Note = "طلب دفع أقل قيمة تأمين متبقية",
                });
            }
            if (employeeInsuranceInfo.IsCompleteInsuranceAmount is false)
            {
                data.Add(new EmployeeNotPaidRequestDto
                {
                    RequestId = 1,
                    DueTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt"),
                    EmployeePercentage = 0,
                    FinishTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt"),
                    CompanyPercentage = employeeInsuranceInfo.RemainAmount,
                    Note = "طلب دفع قيمة التأمين المتبقية",
                });
            }
            return data;
        }

        public async Task<List<EmployeeNotPaidRequestSummaryDto>> GetNotPaidSummaryAsync(DateTime? startTime = null,
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
            var data = await query
                .GroupBy(d => new { d.EmployeeId, d.Employee.EmployeeName, d.Employee.EmployeePhone })
                 .Select(d => new EmployeeNotPaidRequestSummaryDto
                 {
                     EmployeeId = d.Key.EmployeeId,
                     EmployeeName = d.Key.EmployeeName,
                     EmployeePhone = d.Key.EmployeePhone,
                     TotalUnPaidRequestCount = d.Count(),
                     TotalCompanyPercentage = d.Sum(s => s.CompanyPercentageAmount),
                     TotalEmployeePercentage = d.Sum(s => s.EmployeePercentageAmount),
                     TotalUnPaidRequestCost = d.Sum(s => s.CustomerPrice)
                 }).ToListAsync();
            var employeeInsuanceInfoList = await employeeSubscriptionService.GetEmployeeInsuranceInfoAsync(data.Select(d => d.EmployeeId).ToList());
            foreach (var item in employeeInsuanceInfoList)
            {
                if(item.IsCompleteInsuranceAmount is false)
                {
                    var paymentRaw = data.FirstOrDefault(d => d.EmployeeId == item.EmployeeId);
                    var amounts = GetPercentageAfterInsurance(item.IsCompleteInsuranceAmount, paymentRaw.TotalEmployeePercentage, paymentRaw.TotalCompanyPercentage);
                    paymentRaw.TotalEmployeePercentage = amounts.EmployeeAmountPercentage;
                    paymentRaw.TotalCompanyPercentage = amounts.CompanyAmountPercentage;
                    paymentRaw.TotalInsurancePercentage = amounts.InsuranceAmountPercentage;
                }
            }
            return data;
        }

        public async Task<Result<List<PaymentT>>> PayAllAsync(PayAllRequestDto model)
        {
            List<PaymentT> paymentList;
            bool isRootTransaction = false;
            try
            {
                paymentList = new List<PaymentT>();
                isRootTransaction = unitOfWork.StartTransaction();
                var isCompleteInsuranceAmount = await employeeSubscriptionService.IsInsuranceAmountCompletedAsync(model.EmployeeId);
                foreach (var request in model.RequestList)
                {
                    var result = await PayAsync(request.RequestId, helperService.CurrentSystemUserId, request.Amount, isCompleteInsuranceAmount);
                    if (result.IsFail)
                    {
                        return result.Convert(paymentList);
                    }
                    paymentList.Add(result.Data);
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
            DateTime dateTime3DayAgo = DateTime.Now.EgyptTimeNow().AddHours(-3);
            return requestRepository.DbSet
                .AnyAsync(d => d.EmployeeId == employeeId && d.IsPaid == false && d.RequestTimestamp.Value <= dateTime3DayAgo);
        }

        private async Task<Result<decimal>> GetInsuranceAmount(RequestT request, decimal amount, bool? isCompleteInsuranceAmount = null)
        {
            if (isCompleteInsuranceAmount.IsNull())
            {
                isCompleteInsuranceAmount = await employeeSubscriptionService.IsInsuranceAmountCompletedAsync(request.EmployeeId);
            }
            if (isCompleteInsuranceAmount.Value)
            {
                return ResultFactory<decimal>.CreateSuccessResponse(0);
            }
            var amounts = GetPercentageAfterInsurance(isCompleteInsuranceAmount.Value, request.EmployeePercentageAmount, request.CompanyPercentageAmount);
            if(amounts.CompanyAmountPercentage != amount)
            {
                return ResultFactory<decimal>.CreateErrorResponseMessage("Invaid amount value, please refersh and try again");
            }
            return ResultFactory<decimal>.CreateSuccessResponse(amounts.InsuranceAmountPercentage);
        }

        public async Task<Result<PaymentT>> PayAsync(int requestId, int systemUserId, decimal amount, bool? isCompleteInsurancePayment = null)
        {
            decimal insuranceAmount = 0;
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                if (amount.IsNull()) { return ResultFactory<PaymentT>.CreateErrorResponseMessage("Amount can't be null"); }
                var request = await requestRepository.DbSet.Where(d => d.RequestId == requestId)
                    .Include(d => d.RequestStagesT)
                    .Include(d => d.PaymentT)
                    .FirstOrDefaultAsync();
                var requestValidation = helperService.ValidateRequestForPayment<PaymentT>(request);
                if (requestValidation.IsFail) { return requestValidation; }
                var insuranceAmountResult = await GetInsuranceAmount(request, amount, isCompleteInsurancePayment);
                if (insuranceAmountResult.IsFail)
                {
                    return ResultFactory<PaymentT>.CreateErrorResponseMessage(insuranceAmountResult.Message);
                }
                insuranceAmount = insuranceAmountResult.Data;
                request.IsPaid = true;
                request.RequestStagesT.PaymentFlag = true;
                requestRepository.Update(requestId, request);
                await unitOfWork.SaveAsync();
                var payment = new PaymentT
                {
                    Payment = (double)request.CompanyPercentageAmount,
                    PaymentTimestamp = DateTime.Now.EgyptTimeNow(),
                    RequestId = request.RequestId,
                    SystemUserId = systemUserId
                };
                await paymentRepository.AddAsync(payment);
                if (insuranceAmount > 0)
                {
                    await employeeSubscriptionService.AddPaymentAmountAsync(request.EmployeeId, insuranceAmount, requestId);
                }
                string title = "دفع طلب #" + requestId;
                string body = "تم دفع الطلب #" + requestId;
                try { await notificatonService.SendFirebaseNotificationAsync(Domain.Enum.AccountType.Employee, request.EmployeeId, title, body); } catch { }
                var message = new MessagesT
                {
                    Title = title,
                    Body = body,
                    EmployeeId = request.EmployeeId,
                    IsRead = 0,
                    MessageTimestamp = DateTime.Now.EgyptTimeNow()
                };

                await messageRepository.AddAsync(message);
                //if (activeEmployeeAccount)
                //{
                //    bool haveUnPaidRequest = await IsHaveUnPaidRequestExceed3Days(request.EmployeeId);
                //    if (haveUnPaidRequest == false)
                //    {
                //        await employeeAppAccountService.ActiveAccountAsync(request.EmployeeId);
                //    }
                //}
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

        private PercentageAfterInsuranceAmounts GetPercentageAfterInsurance(bool isCompleteInsuranceAmount, decimal employeePercentage, decimal companyPercentage)
        {
            var totalAmount = employeePercentage + companyPercentage;
            var percentage = totalAmount * (GeneralSetting.RequestInsurancePercentge / 100);
            var employeeAmountPercentage = employeePercentage - percentage;
            var companyAmountPercentage = companyPercentage + percentage;
            return new PercentageAfterInsuranceAmounts
            {
                EmployeeAmountPercentage = Math.Round(employeeAmountPercentage, 2),
                CompanyAmountPercentage = Math.Round(companyAmountPercentage, 2),
                InsuranceAmountPercentage = Math.Round(percentage, 2),
            };
        }

        public async Task<EmployeeAppPaymentIndexDto> GetEmployeeAppPaymentIndex(string employeeId)
        {
            var employeeInsuranceInfo = await employeeSubscriptionService.GetEmployeeInsuranceInfoAsync(employeeId);
            DateTime startMonthDate = App.Global.DateTimeHelper.DateTimeHelperService.GetStartDateOfMonthS();
            DateTime endMonthDate = App.Global.DateTimeHelper.DateTimeHelperService.GetEndDateOfMonthS();
            string maxsabTranslation = translationService.Translate("Maxsab");
            string requestTranslation = translationService.Translate("Request");
            string companyPercentageTranslation = translationService.Translate("CompanyPercentage");
            string lastDuoDateTranslation = translationService.Translate("LastDuoDate");
            string egpTranslation = translationService.Translate("EGP");
            string insuranceTranslation = translationService.Translate("Insurance");

            var monthlyRequest = await requestRepository
                .Where(d => d.EmployeeId == employeeId && d.IsCompleted && d.RequestTimestamp >= startMonthDate && d.RequestTimestamp <= endMonthDate)
                .Select(d => new { d.NetPrice, d.EmployeePercentageAmount })
                .ToListAsync();

            var unPaidRequestList = await requestRepository.Where(d => d.EmployeeId == employeeId && d.IsCompleted && d.IsPaid == false)
                .Select(d => new
                {
                    Address = d.RequestedAddress.City.CityName + ", " + d.RequestedAddress.Region.RegionName,
                    d.Client.ClientName,
                    d.RequestId,
                    EmployeeAmountPercentage = d.EmployeePercentageAmount,
                    CompanyAmountPercentage = d.CompanyPercentageAmount,
                    RequestTimestamp = d.RequestTimestamp.Value
                }).ToListAsync();

            var unPaidRequestCustomList = new List<EmployeeAppPaymentRequestItemDto>();
            foreach (var item in unPaidRequestList)
            {
                var temp = new EmployeeAppPaymentRequestItemDto
                {
                    Address = item.Address,
                    ClientName = item.ClientName,
                    RequestId = item.RequestId,
                    RequestCaption = requestTranslation + " #" + item.RequestId,
                    DuoDateDecription = lastDuoDateTranslation + " " + item.RequestTimestamp.AddDays(3).ToShortDateString()
                };
                if (employeeInsuranceInfo.IsCompleteInsuranceAmount)
                {
                    temp.EmployeeAmountPercentageDescription = $"{maxsabTranslation} {item.EmployeeAmountPercentage} {egpTranslation}";
                    temp.CompanyAmountPercentageDescription = $"{companyPercentageTranslation} {item.CompanyAmountPercentage} {egpTranslation}";
                }
                else
                {
                    var percentageAfterInsurance = GetPercentageAfterInsurance(employeeInsuranceInfo.IsCompleteInsuranceAmount, item.EmployeeAmountPercentage, item.CompanyAmountPercentage);
                    temp.EmployeeAmountPercentageDescription = $"{maxsabTranslation} {percentageAfterInsurance.EmployeeAmountPercentage} {egpTranslation}";
                    temp.CompanyAmountPercentageDescription = $"{companyPercentageTranslation} {percentageAfterInsurance.CompanyAmountPercentage} {egpTranslation}\n{percentageAfterInsurance.InsuranceAmountPercentage} {insuranceTranslation}";
                }
                unPaidRequestCustomList.Add(temp);
            }
            if(employeeInsuranceInfo.IsCompleteMinAmount is false)
            {
                unPaidRequestCustomList.Add(new EmployeeAppPaymentRequestItemDto
                {
                    Address = "-",
                    ClientName = "تأمين",
                    RequestId = 0,
                    DuoDateDecription = lastDuoDateTranslation + " " + DateTime.Now.ToShortDateString(),
                    CompanyAmountPercentageDescription = "-",
                    EmployeeAmountPercentageDescription = $"{employeeInsuranceInfo.RemainMinAmount} EGP",
                    RequestCaption = "طلب دفع أقل قيمة تأمين متبقية",
                });
            }
            if(employeeInsuranceInfo.IsCompleteInsuranceAmount is false)
            {
                unPaidRequestCustomList.Add(new EmployeeAppPaymentRequestItemDto
                {
                    Address = "-",
                    ClientName = "تأمين",
                    RequestId = 1,
                    DuoDateDecription = lastDuoDateTranslation + " " + DateTime.Now.ToShortDateString(),
                    CompanyAmountPercentageDescription = "-",
                    EmployeeAmountPercentageDescription = $"{employeeInsuranceInfo.RemainAmount} EGP",
                    RequestCaption = "طلب دفع قيمة التأمين المتبقية",
                });
            }
            return new EmployeeAppPaymentIndexDto
            {
                EmployeeAmountPercentage = monthlyRequest.Sum(d => d.EmployeePercentageAmount).ToString() + " ج",
                EmployeeAmountPercentageCaption = translationService.Translate("Your percentage this month is"),
                EmployeeAmountPercentageDescription = $"{translationService.Translate("You do")} {monthlyRequest.Count} {translationService.Translate("Requests with total cost")} {monthlyRequest.Sum(d => d.NetPrice)} {egpTranslation}",
                RequestList = unPaidRequestCustomList
            };
        }

        public async Task<Result<object>> ConfirmArrivalAsync(int id)
        {
            var request = await requestRepository.Where(d => d.RequestId == id)
                .FirstOrDefaultAsync();
            var requestValidation = helperService.ValidateRequest<object>(request);
            if (requestValidation.IsFail) { return requestValidation; }
            if(request.RequestStatus == GeneralSetting.GetRequestStatusId(RequestStatus.StartExcution))
            {
                return ResultFactory<object>.CreateErrorResponseMessageFD("This request is already confirmed");
            }
            request.RequestStatus = GeneralSetting.GetRequestStatusId(RequestStatus.StartExcution);
            requestRepository.Update(request.RequestId, request);
            var affectedRows = await requestRepository.SaveAsync();
            return ResultFactory<object>.CreateAffectedRowsResult(affectedRows);
        }

        public async Task<bool> IsEmployeeCanEditRequest(int id)
        {
            var status = await requestRepository.Where(d => d.RequestId == id)
                .Select(d => d.RequestStatus)
                .FirstOrDefaultAsync();
            if(status == GeneralSetting.GetRequestStatusId(RequestStatus.StartExcution))
            {
                return true;
            }
            return false;
        }

        public async Task<Result<object>> StartRequestAsync(int requestId, string employeeId)
        {
            var request = await requestRepository.GetAsync(requestId);
            var requestValidation = helperService.ValidateRequest<object>(request, employeeId);
            if (requestValidation.IsFail) { return requestValidation; }

            var inExcutionStatus = helperService.GetExcutionStatusList();
            var inExcutionRequestCount = await requestRepository
                .DbSet.CountAsync(d => d.EmployeeId == employeeId && inExcutionStatus.Contains(d.RequestStatus));
            if(inExcutionRequestCount > 0)
            {
                return ResultFactory<object>.CreateErrorResponseMessageFD("You have un complete pending request, please take action for them first");
            }

            var previousRequestCount = await requestRepository
               .DbSet.CountAsync(d => d.EmployeeId == employeeId && d.IsCompleted == false && d.IsCanceled == false && d.RequestTimestamp < request.RequestTimestamp);
            if (previousRequestCount > 0)
            {
                return ResultFactory<object>.CreateErrorResponseMessageFD("You have an incomplete order before this order, please delay, cancel or complete it first");
            }

            var status = GeneralSetting.GetRequestStatusId(RequestStatus.InExcution);
            request.RequestStatus = status;
            requestRepository.Update(requestId, request);
            var affectedRows = await requestRepository.SaveAsync();
            string title = $"طلب #{request.RequestId}";
            try
            {
                string body = $"عزيزى العميل, العامل/العاملة قى الطريق إليكم";
                await notificatonService.SendFirebaseNotificationAsync(AccountType.Client, request.ClientId.ToString(), title, body);
            }
            catch { }
            return ResultFactory<object>.CreateAffectedRowsResult(affectedRows);
        }

        public async Task<Result<EmployeeT>> ReAssignEmployeeAsync(ReAssignEmployeeDto model)
        {
            EmployeeT employee = null;
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var request = await requestRepository.GetAsync(model.RequestId);
                var requestValidation = helperService.ValidateRequest<EmployeeT>(request);
                if (requestValidation.IsFail) { return requestValidation; }
                if (model.RequestTime.HasValue)
                {
                    request.RequestTimestamp = model.RequestTime;
                }
                if (string.IsNullOrEmpty(model.EmployeeId))
                {
                    if (helperService.IsViaClientApp)
                    {
                        await rejectRequestRepository.AddAsync(new RejectRequestT
                        {
                            EmployeeId = request.EmployeeId,
                            RejectRequestTimestamp = DateTime.Now.EgyptTimeNow(),
                            RequestId = model.RequestId
                        });
                    }
                    request.EmployeeId = null;
                    request.RequestStatus = GeneralSetting.GetRequestStatusId(RequestStatus.NotSet);
                    await unitOfWork.SaveAsync();
                    await operationService.BroadcastAsync(model.RequestId);
                }
                else
                {
                    request.EmployeeId = model.EmployeeId;
                    if (model.SkipCheckEmployee == false)
                    {
                        var employeeValidateResult = await employeeRequestService.ValidateEmployeeForRequest(model.EmployeeId, request.RequestTimestamp.Value, request.BranchId, request.DepartmentId, model.RequestId);
                        if (employeeValidateResult.IsFail)
                        {
                            return employeeValidateResult;
                        }
                    }
                    request.RequestStatus = GeneralSetting.GetRequestStatusId(RequestStatus.Waiting);
                    employee = await employeeRepository.GetAsync(model.EmployeeId);
                    string title = $"طلب #{request.RequestId}";
                    try 
                    {
                        string body = $"لقد تم تعيين العامل/العاملة {employee.EmployeeName} على الطلب الخاص بكم";
                        await notificatonService.SendFirebaseNotificationAsync(AccountType.Client, request.ClientId.ToString(), title, body); 
                    } catch { }
                    try
                    {
                        string body = $"لقد تم اختيارك على الطلب رقم {request.RequestId} برجاء المراجعة";
                        await notificatonService.SendFirebaseNotificationAsync(AccountType.Employee, request.EmployeeId.ToString(), title, body);
                    }
                    catch { }
                }
                request.IsReviewed = false;
                requestRepository.Update(request.RequestId, request);
                int affectedRows = -1;
                if (isRootTransaction)
                {
                    affectedRows = await unitOfWork.CommitAsync(false);
                }
                return ResultFactory<EmployeeT>.CreateAffectedRowsResult(affectedRows, data: employee);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<EmployeeT>(ex);
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
