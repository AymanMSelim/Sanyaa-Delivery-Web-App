using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class SystemUserT
    {
        public SystemUserT()
        {
            AccountT = new HashSet<AccountT>();
            BillNumberT = new HashSet<BillNumberT>();
            CleaningSubscribersT = new HashSet<CleaningSubscribersT>();
            ClientT = new HashSet<ClientT>();
            ContractPaymentT = new HashSet<ContractPaymentT>();
            DeliveryPriceT = new HashSet<DeliveryPriceT>();
            FollowUpT = new HashSet<FollowUpT>();
            GeneralDiscountT = new HashSet<GeneralDiscountT>();
            IncreaseDiscountT = new HashSet<IncreaseDiscountT>();
            PartinerPaymentT = new HashSet<PartinerPaymentT>();
            PaymentT = new HashSet<PaymentT>();
            RequestCanceledT = new HashSet<RequestCanceledT>();
            RequestComplaintT = new HashSet<RequestComplaintT>();
            RequestDelayedT = new HashSet<RequestDelayedT>();
            RequestDiscountT = new HashSet<RequestDiscountT>();
            RequestT = new HashSet<RequestT>();
            ServiceCityRatioT = new HashSet<ServiceCityRatioT>();
            SiteContractT = new HashSet<SiteContractT>();
            SiteT = new HashSet<SiteT>();
        }

        public int SystemUserId { get; set; }
        public string SystemUserUsername { get; set; }
        public string SystemUserPass { get; set; }
        public string SystemUserLevel { get; set; }
        public string EmployeeId { get; set; }
        public int BranchId { get; set; }

        public BranchT Branch { get; set; }
        public EmployeeT Employee { get; set; }
        public ICollection<AccountT> AccountT { get; set; }
        public ICollection<BillNumberT> BillNumberT { get; set; }
        public ICollection<CleaningSubscribersT> CleaningSubscribersT { get; set; }
        public ICollection<ClientT> ClientT { get; set; }
        public ICollection<ContractPaymentT> ContractPaymentT { get; set; }
        public ICollection<DeliveryPriceT> DeliveryPriceT { get; set; }
        public ICollection<FollowUpT> FollowUpT { get; set; }
        public ICollection<GeneralDiscountT> GeneralDiscountT { get; set; }
        public ICollection<IncreaseDiscountT> IncreaseDiscountT { get; set; }
        public ICollection<PartinerPaymentT> PartinerPaymentT { get; set; }
        public ICollection<PaymentT> PaymentT { get; set; }
        public ICollection<RequestCanceledT> RequestCanceledT { get; set; }
        public ICollection<RequestComplaintT> RequestComplaintT { get; set; }
        public ICollection<RequestDelayedT> RequestDelayedT { get; set; }
        public ICollection<RequestDiscountT> RequestDiscountT { get; set; }
        public ICollection<RequestT> RequestT { get; set; }
        public ICollection<ServiceCityRatioT> ServiceCityRatioT { get; set; }
        public ICollection<SiteContractT> SiteContractT { get; set; }
        public ICollection<SiteT> SiteT { get; set; }
    }
}
