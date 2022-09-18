﻿using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class SystemUserT
    {
        public SystemUserT()
        {
            AccountT = new HashSet<AccountT>();
            BillNumberT = new HashSet<BillNumberT>();
            Cleaningsubscribers = new HashSet<Cleaningsubscribers>();
            ClientSubscriptionT = new HashSet<ClientSubscriptionT>();
            ClientT = new HashSet<ClientT>();
            ContractPaymentT = new HashSet<ContractPaymentT>();
            CouponT = new HashSet<CouponT>();
            FollowUpT = new HashSet<FollowUpT>();
            IncreaseDiscountT = new HashSet<IncreaseDiscountT>();
            InsurancePaymentT = new HashSet<InsurancePaymentT>();
            PartinerPaymentT = new HashSet<PartinerPaymentT>();
            PaymentT = new HashSet<PaymentT>();
            PromocodeT = new HashSet<PromocodeT>();
            RequestCanceledT = new HashSet<RequestCanceledT>();
            RequestComplaintT = new HashSet<RequestComplaintT>();
            RequestDelayedT = new HashSet<RequestDelayedT>();
            RequestDiscountT = new HashSet<RequestDiscountT>();
            RequestT = new HashSet<RequestT>();
            TransactionT = new HashSet<TransactionT>();
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
        public ICollection<Cleaningsubscribers> Cleaningsubscribers { get; set; }
        public ICollection<ClientSubscriptionT> ClientSubscriptionT { get; set; }
        public ICollection<ClientT> ClientT { get; set; }
        public ICollection<ContractPaymentT> ContractPaymentT { get; set; }
        public ICollection<CouponT> CouponT { get; set; }
        public ICollection<FollowUpT> FollowUpT { get; set; }
        public ICollection<IncreaseDiscountT> IncreaseDiscountT { get; set; }
        public ICollection<InsurancePaymentT> InsurancePaymentT { get; set; }
        public ICollection<PartinerPaymentT> PartinerPaymentT { get; set; }
        public ICollection<PaymentT> PaymentT { get; set; }
        public ICollection<PromocodeT> PromocodeT { get; set; }
        public ICollection<RequestCanceledT> RequestCanceledT { get; set; }
        public ICollection<RequestComplaintT> RequestComplaintT { get; set; }
        public ICollection<RequestDelayedT> RequestDelayedT { get; set; }
        public ICollection<RequestDiscountT> RequestDiscountT { get; set; }
        public ICollection<RequestT> RequestT { get; set; }
        public ICollection<TransactionT> TransactionT { get; set; }
    }
}