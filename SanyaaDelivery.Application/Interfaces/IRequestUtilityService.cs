using App.Global.DTOs;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IRequestUtilityService
    {
        Task<Result<PaymentT>> PayAsync(int requestId, int systemUserId, decimal? amount = null, bool activeEmployeeAccount = false);
        Task<Result<List<PaymentT>>> PayAllAsync(string employeeId, int systemUserId, decimal? amount = null);
        Task<Result<object>> CompleteAsync(int requestId, int systemUserId);
        Task<Result<FollowUpT>> FollowAsync(FollowUpT followUp);
        Task<List<EmployeeNotPaidRequestSummaryDto>> GetNotPaidSummaryAsync(DateTime? startTime = null,
            DateTime? endTime = null, int? departmentId = null, string employeeId = null, int? requestId = null);
        Task<List<EmployeeNotPaidRequestDto>> GetNotPaidAsync(string employeeId);
        Task<EmployeeAppPaymentIndexDto> GetEmployeeAppPaymentIndex(string employeeId);
        Task<Result<object>> SetAsUnReviewedAsync(int requestId);
        Task<Result<object>> SetReviewedAsync(int requestId);
        Task<Result<object>> StartRequestAsync(int requestId, string employeeId);
        Task<Result<object>> ResetRequestAsync(int requestId);
        Task<bool> IsHaveUnPaidRequestExceed3Days(string employeeId);
        Task<Result<object>> ConfirmArrivalAsync(int id);
        Task<bool> IsEmployeeCanEditRequest(int id);
        Task<Result<EmployeeT>> ReAssignEmployeeAsync(ReAssignEmployeeDto model);

    }
}
