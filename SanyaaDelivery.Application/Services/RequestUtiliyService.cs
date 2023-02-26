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

namespace SanyaaDelivery.Application.Services
{
    public class RequestUtiliyService : IRequestUtilityService
    {
        private readonly IRepository<RequestT> requestRepository;
        private readonly IRepository<PaymentT> paymentRepository;
        private readonly IRepository<FollowUpT> followUpRepository;
        private readonly IUnitOfWork unitOfWork;

        public RequestUtiliyService(IRepository<RequestT> requestRepository, IRepository<PaymentT> paymentRepository,
            IRepository<FollowUpT> followUpRepository, IUnitOfWork unitOfWork)
        {
            this.requestRepository = requestRepository;
            this.paymentRepository = paymentRepository;
            this.followUpRepository = followUpRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<object>> CompleteAsync(int requestId, int systemUserId)
        {
            try
            {
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
                request.IsCompleted = true;
                request.RequestStagesT.FinishTimestamp = DateTime.Now.EgyptTimeNow();
                request.RequestStatus = GeneralSetting.RequestStatusList
                    .FirstOrDefault(d => d.RequestStatusName.ToLower() == "Done").RequestStatusId;
                requestRepository.Update(requestId, request);
                var affectedRows = await requestRepository.SaveAsync();

                return ResultFactory<object>.CreateAffectedRowsResult(affectedRows);
            }
            finally
            {
                unitOfWork.Dispose();
            }
        }

        public async Task<Result<object>> FollowAsync(FollowUpT followUp)
        {
            try
            {
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
                var affectedRows = await requestRepository.SaveAsync();
                return ResultFactory<object>.CreateAffectedRowsResult(affectedRows);
            }
            finally
            {
                unitOfWork.Dispose();
            }
        }

        public Task<List<EmployeeNotPaidRequestDto>> GetNotPaidAsync(string employeeId)
        {
            var data = requestRepository.Where(r => r.IsCanceled == false && r.IsCompleted && r.IsPaid == false && r.EmployeeId == employeeId)
                .Include(d => d.RequestStagesT)
                .Select(d => new EmployeeNotPaidRequestDto
                {
                    CompanyPercentage = d.CompanyPercentageAmount,
                    DueTime = d.RequestStagesT.FinishTimestamp.Value.AddDays(3),
                    EmployeePercentage = d.EmployeePercentageAmount,
                    FinishTime = d.RequestStagesT.FinishTimestamp.Value,
                    Price = d.CustomerPrice,
                    RequestId = d.RequestId,
                    Note = DateTime.Now > d.RequestStagesT.FinishTimestamp.Value.AddDays(3) ? "متأخر الدفع" : ""
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
                     AccountState = d.Key.AccountState.Value,
                     //IncreaseDiscountTotal = d.IncreaseDiscountT.Sum(s => s.IncreaseDiscountValue),
                     TotalUnPaidRequestCount = d.Count(),
                     TotalCompanyPercentage = d.Sum(s => s.CompanyPercentageAmount),
                     TotalEmployeePercentage = d.Sum(s => s.EmployeePercentageAmount),
                     TotalUnPaidRequestCost = d.Sum(s => s.CustomerPrice)
                 }).ToListAsync();
            return data;
            //return employeeRepository.DbSet.Where(d => d.RequestT.Any(r => r.IsCanceled == false && r.IsCompleted && r.IsPaid == false))
            //    .Include(d => d.LoginT)
            //    .Include(d => d.IncreaseDiscountT)
            //    .Include(d => d.RequestT)
            //    .Select(d => new { Employee = d, RequestT = d.RequestT.Where(r => r.IsCanceled == false && r.IsCompleted && r.IsPaid == false) })
            //    .Select(d => new EmployeeNotPaidRequestSummaryDto
            //    {
            //        EmployeeId = d.Employee.EmployeeId,
            //        EmployeeName = d.Employee.EmployeeName,
            //        EmployeePhone = d.Employee.EmployeePhone,
            //        AccountState = d.Employee.LoginT.LoginAccountState.Value,
            //        //IncreaseDiscountTotal = d.IncreaseDiscountT.Sum(s => s.IncreaseDiscountValue),
            //        TotalUnPaidRequestCount = d.RequestT.Count(),
            //        TotalCompanyPercentage = d.RequestT.Sum(s => s.CompanyPercentageAmount),
            //        TotalEmployeePercentage = d.RequestT.Sum(s => s.EmployeePercentageAmount),
            //        TotalUnPaidRequestCost = d.RequestT.Sum(s => s.CustomerPrice)
            //    }).ToListAsync();
        }

        public async Task<Result<List<PaymentT>>> PayAllAsync(string employeeId, int systemUserId, decimal? amount = null)
        {
            List<PaymentT> paymentList;
            try
            {
                paymentList = new List<PaymentT>();
                unitOfWork.StartTransaction();
                var requestList = await GetNotPaidAsync(employeeId);
                if(requestList is null || requestList.Count == 0)
                {
                    return ResultFactory<List<PaymentT>>.CreateErrorResponseMessageFD("This employee have no un paid requests");
                }
                foreach (var request in requestList)
                {
                    var result = await PayAsync(request.RequestId, systemUserId, request.CompanyPercentage);
                    if (result.IsFail)
                    {
                        return result.Convert(paymentList);
                    }
                    paymentList.Add(result.Data);
                }
                var affectedRows = await unitOfWork.CommitAsync();
                return ResultFactory<List<PaymentT>>.CreateAffectedRowsResult(affectedRows, data: paymentList);
            }
            finally
            {
                unitOfWork.Dispose();
            }
        }

        public async Task<Result<PaymentT>> PayAsync(int requestId, int systemUserId, decimal? amount = null)
        {
            try
            {
                unitOfWork.StartTransaction();
                var request = await requestRepository.DbSet.Where(d => d.RequestId == requestId)
                    .Include(d => d.RequestStagesT).FirstOrDefaultAsync();
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
                    return ResultFactory<PaymentT>.CreateErrorResponseMessageFD("This request is already paid");
                }
                request.IsPaid = true;
                request.RequestStagesT.PaymentFlag = true;
                requestRepository.Update(requestId, request);
                var payment = new PaymentT
                {
                    Payment = amount.HasValue ? (double)amount : (double)request.CompanyPercentageAmount,
                    PaymentTimestamp = DateTime.Now.EgyptTimeNow(),
                    RequestId = request.RequestId,
                    SystemUserId = systemUserId
                };
                await paymentRepository.AddAsync(payment);
                var affectedRows = await unitOfWork.CommitAsync();
                return ResultFactory<PaymentT>.CreateAffectedRowsResult(affectedRows, data: payment);
            }
            finally
            {
                unitOfWork.Dispose();
            }
        }
    }
}
