using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Global.DateTimeHelper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SanyaaDelivery.Domain.OtherModels;

namespace SanyaaDelivery.Application.Services
{
    public class EmployeeSubscriptionService : IEmployeeSubscriptionService
    {
        private readonly IRepository<EmployeeSubscriptionT> empSubscriptionRepository;
        private readonly IRepository<InsurancePaymentT> insurancePaymentRepository;
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly IHelperService helperService;
        private readonly ITranslationService translationService;

        public EmployeeSubscriptionService(IRepository<EmployeeSubscriptionT> empSubscriptionRepository, IRepository<InsurancePaymentT> insurancePaymentRepository,
            IRepository<EmployeeT> employeeRepository, IHelperService helperService, ITranslationService translationService)
        {
            this.empSubscriptionRepository = empSubscriptionRepository;
            this.insurancePaymentRepository = insurancePaymentRepository;
            this.employeeRepository = employeeRepository;
            this.helperService = helperService;
            this.translationService = translationService;
        }

        public async Task<int> AddPaymentAmountAsync(string employeeId, decimal amount, int? requestId = null)
        {
            if (amount <= 0) { return ((int)App.Global.Enums.ResultStatusCode.Success); }
            var payment = new InsurancePaymentT
            {
                Amount = Convert.ToInt32(amount),
                CreationTime = DateTime.Now,
                EmployeeId = employeeId,
                ReferenceId = requestId,
                ReferenceType = ((int)Domain.Enum.InsurancePaymentType.Add),
                SystemUserId = helperService.CurrentSystemUserId,
                Description = translationService.Translate("Insurance")
            };
            if (requestId.HasValue)
            {
                payment.Description = $"{translationService.Translate("Request insurance percentage")} #{requestId}";
            }
            await insurancePaymentRepository.AddAsync(payment);
            return await insurancePaymentRepository.SaveAsync();
        }

        public async Task<decimal> GetMustPaidAmountAsync(string employeeId)
        {
            var subscriptionMinAmount = await employeeRepository.Where(d => d.EmployeeId == employeeId)
                 .Select(d => d.Subscription.MinInsuranceAmountMustPaid)
                 .FirstOrDefaultAsync();
            if (subscriptionMinAmount.IsNull()) { return 0; }

            var amounts = await GetInsuranceAmounts(employeeId);
            var paidAmount = amounts.AddAmount - amounts.WithdrawAmount;
            if(paidAmount >= subscriptionMinAmount)
            {
                return 0;
            }
            else
            {
                return (decimal)(subscriptionMinAmount - paidAmount);
            }
        }

        private async Task<InsuranceAmounts> GetInsuranceAmounts(string employeeId)
        {
            InsuranceAmounts insuranceAmounts = new InsuranceAmounts();
            var amounts = await insurancePaymentRepository
               .Where(d => d.EmployeeId == employeeId)
               .GroupBy(d => d.ReferenceType)
               .Select(g => new {
                   ReferenceType = g.Key,
                   Amount = g.Sum(d => d.Amount)
               })
               .ToListAsync();
            if (amounts.IsEmpty()) { return insuranceAmounts; }

            var addAmount = amounts.Where(d => d.ReferenceType == ((int)Domain.Enum.InsurancePaymentType.Add))
                .Select(d => d.Amount)
                .FirstOrDefault();
            if (addAmount.HasValue)
            {
                insuranceAmounts.AddAmount = addAmount.Value;
            }
            var withdrawAmount = amounts.Where(d => d.ReferenceType == ((int)Domain.Enum.InsurancePaymentType.Withdraw))
                .Select(d => d.Amount)
                .FirstOrDefault();
            if (withdrawAmount.HasValue)
            {
                insuranceAmounts.WithdrawAmount = withdrawAmount.Value;
            }
            return insuranceAmounts;
        }

        public async Task<decimal> GetRemainInsuranceAmountAsync(string employeeId)
        {
            var insuranceAmount = await employeeRepository.Where(d => d.EmployeeId == employeeId)
                .Select(d => d.Subscription.InsuranceAmount)
                .FirstOrDefaultAsync();
            if (insuranceAmount.IsNull()) { return 0; }

            var amounts = await GetInsuranceAmounts(employeeId);
            var paidAmount = amounts.AddAmount - amounts.WithdrawAmount;
            if (paidAmount >= insuranceAmount)
            {
                return 0;
            }
            else
            {
                return (decimal)(insuranceAmount - paidAmount);
            }
        }

        public async Task<bool> IsHasMinimumAmountPaidAsync(string employeeId)
        {
            var mustPaidAmount = await GetMustPaidAmountAsync(employeeId);
            if(mustPaidAmount > 0)
            {
                return false;
            }
            return true;

        }

        public async Task<bool> IsInsuranceAmountCompletedAsync(string employeeId)
        {
            var insuranceAmount = await GetRemainInsuranceAmountAsync(employeeId);
            if (insuranceAmount > 0)
            {
                return false;
            }
            return true;
        }

        public async Task<int> AddPaymentAsync(InsurancePaymentT payment)
        {
            if (payment.CreationTime.IsNull())
            {
                payment.CreationTime = DateTime.Now.EgyptTimeNow();
            }
            payment.SystemUserId = helperService.CurrentSystemUserId;
            await insurancePaymentRepository.AddAsync(payment);
            return await insurancePaymentRepository.SaveAsync();
        }

        public async Task<int> DeletePaymentAsync(int id)
        {
            await insurancePaymentRepository.DeleteAsync(id);
            return await insurancePaymentRepository.SaveAsync();
        }

        public async Task<EmployeeInsuranceIndexDto> GetIndexAsync(string employeeId)
        {
            int insuranceAmount = 0;
            int remainAmount = 0;
            var employee = await employeeRepository.Where(d => d.EmployeeId == employeeId)
             .Include(d => d.Subscription)
             .Include(d => d.InsurancePaymentT)
             .FirstOrDefaultAsync();

            if (employee.Subscription.InsuranceAmount.HasValue)
            {
                insuranceAmount = employee.Subscription.InsuranceAmount.Value;
            }
            var addAmount = employee.InsurancePaymentT
                .Where(d => d.ReferenceType == ((int)Domain.Enum.InsurancePaymentType.Add))
                .Sum(d => d.Amount.Value);
            var withdrawAmount = employee.InsurancePaymentT
                .Where(d => d.ReferenceType == ((int)Domain.Enum.InsurancePaymentType.Withdraw))
                .Sum(d => d.Amount.Value);

            var paidAmount = addAmount - withdrawAmount;

            var index = new EmployeeInsuranceIndexDto
            {
                SubscriptionCaptionTop = translationService.Translate("You subscripe in package"),
                SubscriptionCaptionDown = translationService.Translate("Insurance"),
                SubscriptionCaption = insuranceAmount + " " + translationService.Translate("EGP"),
                PaymentList = employee.InsurancePaymentT
                .OrderByDescending(d => d.CreationTime)
                .Select(t => new InsurancePaymentDto
                {
                    Id = t.InsurancePaymentId,
                    Amount = t.Amount.Value,
                    Date = t.CreationTime.Value.ToShortDateString(),
                    Description = t.Description,
                    Type = translationService.Translate(((Domain.Enum.InsurancePaymentType)t.ReferenceType).ToString())
                }).ToList()
            };
            if (employee.Subscription.InsuranceAmount.HasValue)
            {
                insuranceAmount = employee.Subscription.InsuranceAmount.Value;
            }
            if(insuranceAmount - paidAmount > 0)
            {
                remainAmount = insuranceAmount - paidAmount;
            }
            index.SubscriptionPaidAmountCaption = $"{translationService.Translate("We recive")} {paidAmount} {translationService.Translate("Remain")} {remainAmount}";
            return index;
        }

        public Task<int> UpdatePaymentAsync(InsurancePaymentT payment)
        {
            insurancePaymentRepository.Update(payment.InsurancePaymentId, payment);
            return insurancePaymentRepository.SaveAsync();
        }

        public async Task<List<InsurancePaymentDto>> GetPaymentCustomListAsync(string employeeId)
        {
            var paymentList = await insurancePaymentRepository
                .Where(d => d.EmployeeId == employeeId)
                .Select(t => new InsurancePaymentDto
                {
                    Id = t.InsurancePaymentId,
                    Amount = t.Amount.Value,
                    Date = t.CreationTime.ToString(),
                    Description = t.Description,
                    Type = t.ReferenceType.ToString()
                })
                .ToListAsync();

            foreach (var item in paymentList)
            {
                item.Type = translationService.Translate(((Domain.Enum.InsurancePaymentType)Convert.ToInt32(item.Type)).ToString());
            }
            return paymentList;
        }

        public Task<List<InsurancePaymentT>> GetPaymentListAsync(string employeeId)
        {
            return insurancePaymentRepository.Where(d => d.EmployeeId == employeeId)
                .ToListAsync();
        }

        public Task<InsurancePaymentT> GetPaymentAsync(int id)
        {
           return insurancePaymentRepository.GetAsync(id);
        }

        public async Task<List<EmployeeInsuranceInfo>> GetEmployeeInsuranceInfoAsync(List<string> employeeIdList)
        {
            var list = await employeeRepository.Where(d => employeeIdList.Contains(d.EmployeeId))
                .Select(d => new EmployeeInsuranceInfo
                {
                    EmployeeId = d.EmployeeId,
                    InsuranceAmount = d.Subscription.InsuranceAmount.Value,
                    InsuranceMinAmount = d.Subscription.MinInsuranceAmountMustPaid.Value,
                    SubsriptionId = d.SubscriptionId.Value,
                    SubsriptionName = d.Subscription.Description,
                    TotalAdd = d.InsurancePaymentT.Where(d => d.ReferenceType == ((int)Domain.Enum.InsurancePaymentType.Add)).Sum(d => d.Amount.Value),
                    TotalWithdraw = d.InsurancePaymentT.Where(d => d.ReferenceType == ((int)Domain.Enum.InsurancePaymentType.Withdraw)).Sum(d => d.Amount.Value),
                }).ToListAsync();

            foreach (var item in list)
            {
                item.PaidAmount = item.TotalAdd - item.TotalWithdraw;
                if(item.PaidAmount >= item.InsuranceAmount)
                {
                    item.IsCompleteInsuranceAmount = true;
                }
                else
                {
                    item.RemainAmount = item.InsuranceAmount - item.PaidAmount;
                }
                if(item.PaidAmount >= item.InsuranceMinAmount)
                {
                    item.IsCompleteMinAmount = true;
                }
                else
                {
                    item.RemainMinAmount = item.InsuranceMinAmount - item.PaidAmount;
                }
            }
            return list;
        }

        public async Task<EmployeeInsuranceInfo> GetEmployeeInsuranceInfoAsync(string employeeId)
        {
            var list = await GetEmployeeInsuranceInfoAsync(new List<string> { employeeId });
            return list.FirstOrDefault();
        }
    }
}
