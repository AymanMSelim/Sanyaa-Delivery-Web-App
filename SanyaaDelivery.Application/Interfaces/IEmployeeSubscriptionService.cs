using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Domain.OtherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IEmployeeSubscriptionService
    {
        Task<bool> IsHasMinimumAmountPaidAsync(string employeeId);
        Task<bool> IsInsuranceAmountCompletedAsync(string employeeId);
        Task<decimal> GetMustPaidAmountAsync(string employeeId);
        Task<decimal> GetRemainInsuranceAmountAsync(string employeeId);
        Task<int> AddPaymentAmountAsync(string employeeId, decimal amount, int? requestId = null);
        Task<EmployeeInsuranceIndexDto> GetIndexAsync(string employeeId);
        Task<List<InsurancePaymentT>> GetPaymentListAsync(string employeeId);
        Task<List<InsurancePaymentDto>> GetPaymentCustomListAsync(string employeeId);
        Task<int> AddPaymentAsync(InsurancePaymentT payment);
        Task<int> DeletePaymentAsync(int id);
        Task<int> UpdatePaymentAsync(InsurancePaymentT payment);
        Task<InsurancePaymentT> GetPaymentAsync(int id);
        Task<List<EmployeeInsuranceInfo>> GetEmployeeInsuranceInfoAsync(List<string> employeeIdList);
        Task<EmployeeInsuranceInfo> GetEmployeeInsuranceInfoAsync(string employeeId);
    }
}
