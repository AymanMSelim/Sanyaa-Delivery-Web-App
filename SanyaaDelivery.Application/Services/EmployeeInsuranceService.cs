using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SanyaaDelivery.Application.Services
{
    public class EmployeeInsuranceService : IEmployeeInsuranceService
    {
        private readonly IRepository<InsurancePaymentT> insurancePatmentRepository;
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly IRepository<EmployeeSubscriptionT> employeeSubscriptionRepository;
        private readonly ITranslationService translationService;

        public EmployeeInsuranceService(IRepository<InsurancePaymentT> insurancePatmentRepository, IRepository<EmployeeT> employeeRepository,
            IRepository<EmployeeSubscriptionT> employeeSubscriptionRepository, ITranslationService translationService)
        {
            this.insurancePatmentRepository = insurancePatmentRepository;
            this.employeeRepository = employeeRepository;
            this.employeeSubscriptionRepository = employeeSubscriptionRepository;
            this.translationService = translationService;
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
                        Amount = t.Amount.Value,
                        Date = t.CreationTime.ToString(),
                        Description = t.Description,
                        Type = t.ReferenceType.ToString()
                    }).ToList()
                }).FirstOrDefaultAsync();
            foreach (var item in index.PaymentList)
            {
                item.Type = ((Domain.Enum.InsurancePaymentType)Convert.ToInt32(item.Type)).ToString();

            }
            return index;
        }
    }
}
