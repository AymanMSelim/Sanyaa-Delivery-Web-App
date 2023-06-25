using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class SystemUserT
{
    public int SystemUserId { get; set; }

    public string SystemUserUsername { get; set; }

    public string SystemUserPass { get; set; }

    public string SystemUserLevel { get; set; }

    public string EmployeeId { get; set; }

    public int BranchId { get; set; }

    public virtual ICollection<AccountT> AccountT { get; set; } = new List<AccountT>();

    public virtual ICollection<BillNumberT> BillNumberT { get; set; } = new List<BillNumberT>();

    public virtual BranchT Branch { get; set; }

    public virtual ICollection<Cleaningsubscribers> Cleaningsubscribers { get; set; } = new List<Cleaningsubscribers>();

    public virtual ICollection<ClientSubscriptionT> ClientSubscriptionT { get; set; } = new List<ClientSubscriptionT>();

    public virtual ICollection<ClientT> ClientT { get; set; } = new List<ClientT>();

    public virtual EmployeeT Employee { get; set; }

    public virtual ICollection<EmployeeT> EmployeeT { get; set; } = new List<EmployeeT>();

    public virtual ICollection<FollowUpT> FollowUpT { get; set; } = new List<FollowUpT>();

    public virtual ICollection<IncreaseDiscountT> IncreaseDiscountT { get; set; } = new List<IncreaseDiscountT>();

    public virtual ICollection<InsurancePaymentT> InsurancePaymentT { get; set; } = new List<InsurancePaymentT>();

    public virtual ICollection<PartinerPaymentT> PartinerPaymentT { get; set; } = new List<PartinerPaymentT>();

    public virtual ICollection<PaymentT> PaymentT { get; set; } = new List<PaymentT>();

    public virtual ICollection<PromocodeT> PromocodeT { get; set; } = new List<PromocodeT>();

    public virtual ICollection<RequestCanceledT> RequestCanceledT { get; set; } = new List<RequestCanceledT>();

    public virtual ICollection<RequestComplaintT> RequestComplaintT { get; set; } = new List<RequestComplaintT>();

    public virtual ICollection<RequestDelayedT> RequestDelayedT { get; set; } = new List<RequestDelayedT>();

    public virtual ICollection<RequestDiscountT> RequestDiscountT { get; set; } = new List<RequestDiscountT>();

    public virtual ICollection<RequestServicesT> RequestServicesT { get; set; } = new List<RequestServicesT>();

    public virtual ICollection<RequestT> RequestT { get; set; } = new List<RequestT>();

    public virtual ICollection<SiteContractT> SiteContractTModificationSystemUser { get; set; } = new List<SiteContractT>();

    public virtual ICollection<SiteContractT> SiteContractTSystemUser { get; set; } = new List<SiteContractT>();

    public virtual ICollection<SiteT> SiteT { get; set; } = new List<SiteT>();

    public virtual ICollection<TransactionT> TransactionT { get; set; } = new List<TransactionT>();

    public virtual ICollection<VacationT> VacationT { get; set; } = new List<VacationT>();
}
