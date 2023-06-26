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
            var payment = new InsurancePaymentT
            {
                Amount = Convert.ToInt32(amount),
                CreationTime = DateTime.Now,
                EmployeeId = employeeId,
                ReferenceId = requestId,
                ReferenceType = ((int)Domain.Enum.InsurancePaymentType.Add),
                SystemUserId = helperService.CurrentSystemUserId,
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

            var amounts = await insurancePaymentRepository
                .Where(d => d.EmployeeId == employeeId)
                .GroupBy(d => d.ReferenceType)
                .Select(g => new {
                    ReferenceType = g.Key,
                    Amount = g.Sum(d => d.Amount)
                })
                .ToListAsync();

            var addAmount = amounts.FirstOrDefault(d => d.ReferenceType == ((int)Domain.Enum.InsurancePaymentType.Add))
                .Amount;
            var withdrawAmount = amounts.FirstOrDefault(d => d.ReferenceType == ((int)Domain.Enum.InsurancePaymentType.Withdraw))
                .Amount;
            var paidAmount = addAmount - withdrawAmount;
            if(paidAmount >= subscriptionMinAmount)
            {
                return 0;
            }
            else
            {
                return (decimal)(subscriptionMinAmount - paidAmount);
            }
        }

        public async Task<decimal> GetRemainInsuranceAmountAsync(string employeeId)
        {
            var insuranceAmount = await employeeRepository.Where(d => d.EmployeeId == employeeId)
                .Select(d => d.Subscription.InsuranceAmount)
                .FirstOrDefaultAsync();
            if (insuranceAmount.IsNull()) { return 0; }

            var amounts = await insurancePaymentRepository
                .Where(d => d.EmployeeId == employeeId)
                .GroupBy(d => d.ReferenceType)
                .Select(g => new {
                    ReferenceType = g.Key,
                    Amount = g.Sum(d => d.Amount)
                })
                .ToListAsync();

            var addAmount = amounts.FirstOrDefault(d => d.ReferenceType == ((int)Domain.Enum.InsurancePaymentType.Add))
                .Amount;
            var withdrawAmount = amounts.FirstOrDefault(d => d.ReferenceType == ((int)Domain.Enum.InsurancePaymentType.Withdraw))
                .Amount;
            var paidAmount = addAmount - withdrawAmount;
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
            var index = await employeeRepository.Where(d => d.EmployeeId == employeeId)
                .Select(d => new EmployeeInsuranceIndexDto
                {
                    SubscriptionCaptionTop = translationService.Translate("You subscripe in package"),
                    SubscriptionCaptionDown = translationService.Translate("Insurance"),
                    SubscriptionCaption = d.Subscription.InsuranceAmount + " " + translationService.Translate("EGP"),
                    SubscriptionPaidAmountCaption = translationService.Translate("We recive") + " " + d.InsurancePaymentT.Sum(t => t.Amount) + " " + translationService.Translate("Remain") + " " + (d.Subscription.InsuranceAmount.Value - d.InsurancePaymentT.Sum(t => t.Amount.Value)) + " " + translationService.Translate("EGP"),
                    PaymentList = d.InsurancePaymentT.Select(t => new InsurancePaymentDto
                    {
                        Id = t.InsurancePaymentId,
                        Amount = t.Amount.Value,
                        Date = t.CreationTime.ToString(),
                        Description = t.Description,
                        Type = t.ReferenceType.ToString()
                    }).ToList()
                }).FirstOrDefaultAsync();
            foreach (var item in index.PaymentList)
            {
                item.Type = translationService.Translate(((Domain.Enum.InsurancePaymentType)Convert.ToInt32(item.Type)).ToString());
            }
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
    }
}
