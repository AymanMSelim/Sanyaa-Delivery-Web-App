﻿using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Global.DateTimeHelper;

namespace SanyaaDelivery.Application.Services
{
    public class OperationService : IOperationService
    {
        private readonly IRepository<RequestT> requestRepository;
        private readonly IRepository<BroadcastRequestT> broadcastRequestRepository;
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly IRepository<OpreationT> opreationRepository;
        private readonly ITranslationService translationService;
        private readonly IRepository<DepartmentEmployeeT> employeeDepartmentRepository;
        private readonly IRepository<RejectRequestT> rejectRequestRepository;
        private readonly IUnitOfWork unitOfWork;

        public OperationService(IRepository<RequestT> requestRepository, IRepository<BroadcastRequestT> broadcastRequestRepository, 
            IRepository<EmployeeT> employeeRepository, IRepository<OpreationT> opreationRepository, ITranslationService translationService,
            IRepository<DepartmentEmployeeT> employeeDepartmentRepository, IRepository<RejectRequestT> rejectRequestRepository, IUnitOfWork unitOfWork)
        {
            this.requestRepository = requestRepository;
            this.broadcastRequestRepository = broadcastRequestRepository;
            this.employeeRepository = employeeRepository;
            this.opreationRepository = opreationRepository;
            this.translationService = translationService;
            this.employeeDepartmentRepository = employeeDepartmentRepository;
            this.rejectRequestRepository = rejectRequestRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<object>> ApproveRequestAsync(BroadcastRequestActionDto model)
        {
            try
            {
                var request = await requestRepository.GetAsync(model.RequestId);
                if (request.IsNull())
                {
                    return ResultFactory<object>.CreateNotFoundResponse("Request not found");
                }
                request.RequestStatus = GeneralSetting.GetRequestStatusId("Waiting");
                requestRepository.Update(request.RequestId, request);
                int affectedRows = await requestRepository.SaveAsync();

                return ResultFactory<object>.CreateAffectedRowsResult(affectedRows);
            }
            catch (Exception ex)
            {
                return ResultFactory<object>.CreateExceptionResponse(ex);
            }
        }

        public async Task<AppLandingIndexDto> GetAppLandingIndexAsync(string employeeId)
        {
            DateTime startMonthDate = App.Global.DateTimeHelper.DateTimeHelperService.GetStartDateOfMonthS();
            DateTime endMonthDate = App.Global.DateTimeHelper.DateTimeHelperService.GetEndDateOfMonthS();
            var operation = await opreationRepository.DbSet.FirstOrDefaultAsync(d => d.EmployeeId == employeeId);
            var monthlyRequest = await requestRepository
                .Where(d => d.EmployeeId == employeeId && d.IsCompleted && d.RequestTimestamp >= startMonthDate && d.RequestTimestamp <= endMonthDate)
                .Select(d => new { d.NetPrice, d.EmployeePercentageAmount })
                .ToListAsync();
            var broadcastRequstList = new List<AppRequestDto>();
            var waitingApproveRequstList = new List<AppRequestDto>();
            var activeRequestList = new List<AppRequestDto>();
            if (operation.IsActive)
            {
                //var employeeDepartmentIdList = await employeeDepartmentRepository.Where(d => d.EmployeeId == employeeId)
                //   .Select(d => d.DepartmentId)
                //   .ToListAsync();
                var broadcastStatus = GeneralSetting.GetRequestStatusId("Broadcast");
                var waitingApproveStatus = GeneralSetting.GetRequestStatusId("WaitingApprove");
                broadcastRequstList = await broadcastRequestRepository
                    .Where(d => d.Status == Domain.Enum.BroadcastStatus.Pending.ToString() && d.Request.RequestStatus == broadcastStatus)
                    .Select(d => new AppRequestDto
                    {
                        Date = d.Request.RequestTimestamp.Value.ToString("yyyy-MM-dd"),
                        RequestStatus = translationService.Translate(d.Request.RequestStatusNavigation.RequestStatusName),
                        DayOfWeek = translationService.Translate(d.Request.RequestTimestamp.Value.DayOfWeek.ToString()),
                        Department = d.Request.Department.DepartmentName,
                        RequestCaption = translationService.Translate("Request") + " #" + d.RequestId,
                        RequestId = d.RequestId,
                        Time = d.Request.RequestTimestamp.Value.ToString("hh:mm tt"),
                        Services = string.Join(", ", d.Request.RequestServicesT.Select(t => t.Service.ServiceName).ToList())
                    }).ToListAsync();
                waitingApproveRequstList = await requestRepository.Where(d => d.RequestStatus == waitingApproveStatus && d.EmployeeId == employeeId)
                         .Select(d => new AppRequestDto
                         {
                             Date = d.RequestTimestamp.Value.ToString("yyyy-MM-dd"),
                             RequestStatus = translationService.Translate(d.RequestStatusNavigation.RequestStatusName),
                             DayOfWeek = translationService.Translate(d.RequestTimestamp.Value.DayOfWeek.ToString()),
                             Department = d.Department.DepartmentName,
                             RequestCaption = translationService.Translate("Request") + " #" + d.RequestId,
                             RequestId = d.RequestId,
                             Time = d.RequestTimestamp.Value.ToString("hh:mm tt"),
                             Services = string.Join(", ", d.RequestServicesT.Select(t => t.Service.ServiceName).ToList())
                         }).ToListAsync();
                //activeRequest = await requestRepository
                //    .Where(d => d.RequestStatus == broadcastStatus && d.IsCanceled == false)
                //    .Where(d => d.BroadcastRequestT.Any(t => t.EmployeeId == employeeId && t.Status == Domain.Enum.BroadcastStatus.Pending.ToString()))
                //    .Select(d => new AppRequestDto
                //    {
                //        Date = d.RequestTimestamp.Value.ToString("yyyy-MM-dd"),
                //        RequestStatus = translationService.Translate(d.RequestStatusNavigation.RequestStatusName),
                //        DayOfWeek = translationService.Translate(d.RequestTimestamp.Value.DayOfWeek.ToString()),
                //        Department = d.Department.DepartmentName,
                //        RequestCaption = translationService.Translate("Request") + " #" + d.RequestId,
                //        RequestId = d.RequestId,
                //        Time = d.RequestTimestamp.Value.ToString("hh:mm tt"),
                //        Services = string.Join(", ", d.RequestServicesT.Select(t => t.Service.ServiceName).ToList())
                //    }).ToListAsync();
                var broadcastIdList = broadcastRequstList.Select(d => d.RequestId).ToList();
                if (broadcastIdList.HasItem())
                {
                    var broadcastRequests = await broadcastRequestRepository
                        .Where(d => d.EmployeeId == employeeId && broadcastIdList.Contains(d.RequestId))
                        .ToListAsync();
                    foreach (var broadcastRequest in broadcastRequests)
                    {
                        if (broadcastRequest.IsListed) { continue; }
                        broadcastRequest.IsListed = true;
                        broadcastRequest.ActionTime = DateTime.Now.EgyptTimeNow();
                        broadcastRequestRepository.Update(broadcastRequest.BroadcastRequestId, broadcastRequest);
                    }
                    await broadcastRequestRepository.SaveAsync();
                }
            }
            activeRequestList.AddRange(broadcastRequstList);
            activeRequestList.AddRange(waitingApproveRequstList);
            return new AppLandingIndexDto
            {
                ActiveStatus = operation.IsActive,
                EmployeeAmountPercentage = monthlyRequest.Sum(d => d.EmployeePercentageAmount).ToString() + " ج",
                EmployeeAmountPercentageCaption = translationService.Translate("Your percentage this month is"),
                EmployeeAmountPercentageDescription = $"لقد قمت بـ {monthlyRequest.Count} مهام بتكلفة كلية {monthlyRequest.Sum(d => d.NetPrice)}ج",
                ActiveRequestList = activeRequestList
            };
        }

        public async Task<BroadcastRequestDetailsDto> GetRequestDetailsAsync(BroadcastRequestActionDto model)
        {
            var broadcastRequest = await broadcastRequestRepository.Where(d => d.EmployeeId == model.EmployeeId && d.RequestId == model.RequestId)
                .FirstOrDefaultAsync();
            if (broadcastRequest.IsNotNull())
            {
                if (broadcastRequest.IsSeen == false)
                {
                    broadcastRequest.IsSeen = true;
                    broadcastRequest.ActionTime = DateTime.Now.EgyptTimeNow();
                    broadcastRequestRepository.Update(broadcastRequest.BroadcastRequestId, broadcastRequest);
                    await broadcastRequestRepository.SaveAsync();
                }
            }
            var request = await requestRepository.Where(d => d.RequestId == model.RequestId)
                .Select(d => new BroadcastRequestDetailsDto
                {
                    City = d.RequestedAddress.City.CityName,
                    Region = d.RequestedAddress.Region.RegionName,
                    Date = d.RequestTimestamp.Value.ToString("yyyy-MM-dd dddd"),
                    Time = d.RequestTimestamp.Value.ToString("hh:mm tt"),
                    Status = d.RequestStatusNavigation.RequestStatusName,
                    StatusTranslated = translationService.Translate(d.RequestStatusNavigation.RequestStatusName),
                    RequestCaption = translationService.Translate("Request") + " #" + d.RequestId,
                    RequestId = d.RequestId,
                    RequestServiceList = d.RequestServicesT.Select(t => new RequestServiceDto
                    {
                        Discount = t.ServiceDiscount,
                        NetPrice = t.ServicePrice - t.ServiceDiscount,
                        Price = t.ServicePrice,
                        Quantity = t.RequestServicesQuantity,
                        RequestServiceId = t.RequestServiceId,
                        ServiceDescription = t.Service.ServiceDes,
                        ServiceId = t.ServiceId,
                        ServiceName = t.Service.ServiceName
                    }).ToList(),
                    InvoiceDetails = new List<InvoiceDetailsDto>
                    {
                        new InvoiceDetailsDto { Name = translationService.Translate("TotalPrice"), Amount = d.TotalPrice },
                        new InvoiceDetailsDto { Name = translationService.Translate("Discount"), Amount = d.TotalDiscount },
                        new InvoiceDetailsDto { Name = translationService.Translate("NetPrice"), Amount = d.NetPrice }
                    }
                }).FirstOrDefaultAsync();
            return request;
        }

        public async Task<Result<BroadcastRequestT>> RejectBroadcastRequestAsync(BroadcastRequestActionDto model)
        {
            try
            {
                var broadcastRequest = await broadcastRequestRepository.Where(d => d.RequestId == model.RequestId && d.EmployeeId == model.EmployeeId)
                    .FirstOrDefaultAsync();
                if (broadcastRequest.IsNotNull())
                {
                    broadcastRequest.Status = Domain.Enum.BroadcastStatus.Rejected.ToString();
                    broadcastRequest.ActionTime = DateTime.Now.EgyptTimeNow();
                    broadcastRequestRepository.Update(broadcastRequest.BroadcastRequestId, broadcastRequest);
                }
                var affectedRows = await broadcastRequestRepository.SaveAsync();
                return ResultFactory<BroadcastRequestT>.CreateAffectedRowsResult(affectedRows, data: broadcastRequest);
            }
            catch (Exception ex)
            {
                return ResultFactory<BroadcastRequestT>.CreateExceptionResponse(ex);
            }
        }

        public async Task<Result<RejectRequestT>> RejectRequestAsync(BroadcastRequestActionDto model)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var request = await requestRepository.GetAsync(model.RequestId);
                if (request.IsNull())
                {
                    return ResultFactory<RejectRequestT>.CreateNotFoundResponse("Request not found");
                }
                request.RequestStatus = GeneralSetting.GetRequestStatusId("Rejected");
                request.EmployeeId = null;
                requestRepository.Update(request.RequestId, request);
                var rejectRequest = new RejectRequestT
                {
                    EmployeeId = model.EmployeeId,
                    RejectRequestTimestamp = DateTime.Now.EgyptTimeNow(),
                    RequestId = model.RequestId
                };
                await rejectRequestRepository.AddAsync(rejectRequest);
                int affectedRows = -1;
                if (isRootTransaction)
                {
                    affectedRows = await unitOfWork.CommitAsync(false);
                }
                rejectRequest.Request = null;
                return ResultFactory<RejectRequestT>.CreateAffectedRowsResult(affectedRows, data: rejectRequest);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return ResultFactory<RejectRequestT>.CreateExceptionResponse(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
        }

        public async Task<AppLandingIndexDto> SwitchActiveAsync(string employeeId)
        {
            var operation = await opreationRepository.DbSet.FirstOrDefaultAsync(d => d.EmployeeId == employeeId);
            operation.IsActive = !operation.IsActive;
            opreationRepository.Update(operation.EmployeeId, operation);
            await opreationRepository.SaveAsync();
            return await GetAppLandingIndexAsync(employeeId);

        }

        public async Task<int> SwitchOpenVacationAsync(string employeeId)
        {
            var operation = await opreationRepository.DbSet.FirstOrDefaultAsync(d => d.EmployeeId == employeeId);
            operation.OpenVacation = !operation.OpenVacation;
            opreationRepository.Update(operation.EmployeeId, operation);
            return await opreationRepository.SaveAsync();
        }

        public async Task<int> UpdatePreferredWorkingHourAsync(UpdatePreferredWorkingHourDto model)
        {
            var operation = await opreationRepository.DbSet.FirstOrDefaultAsync(d => d.EmployeeId == model.EmployeeId);
            operation.PreferredWorkingStartHour = model.StartHour;
            operation.PreferredWorkingEndHour = model.EndHour;
            opreationRepository.Update(operation.EmployeeId, operation);
            return await opreationRepository.SaveAsync();
        }

        public async Task<Result<BroadcastRequestT>> TakeBroadcastRequestAsync(BroadcastRequestActionDto model)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var request = await requestRepository.GetAsync(model.RequestId);
                if (request.IsNull())
                {
                    return ResultFactory<BroadcastRequestT>.CreateNotFoundResponse("Request not found");
                }
                var broadcastStatus = GeneralSetting.GetRequestStatusId("Broadcast");
                if (request.RequestStatus != broadcastStatus || request.EmployeeId.IsNotNull())
                {
                    return ResultFactory<BroadcastRequestT>.CreateErrorResponseMessageFD("This request have been taken");
                }
                var broadcastRequest = await broadcastRequestRepository.Where(d => d.RequestId == model.RequestId && d.EmployeeId == model.EmployeeId)
                    .FirstOrDefaultAsync();
                if (broadcastRequest.IsNotNull())
                {
                    broadcastRequest.Status = Domain.Enum.BroadcastStatus.Accepted.ToString();
                    broadcastRequest.ActionTime = DateTime.Now.EgyptTimeNow();
                    broadcastRequestRepository.Update(broadcastRequest.BroadcastRequestId, broadcastRequest);
                }
                request.EmployeeId = model.EmployeeId;
                request.RequestStatus = GeneralSetting.GetRequestStatusId("Waiting");
                requestRepository.Update(request.RequestId, request);
                int affectedRows = -1;
                if (isRootTransaction)
                {
                    affectedRows = await unitOfWork.CommitAsync(false);
                }
                return ResultFactory<BroadcastRequestT>.CreateAffectedRowsResult(affectedRows, data: broadcastRequest);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return ResultFactory<BroadcastRequestT>.CreateExceptionResponse(ex);
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