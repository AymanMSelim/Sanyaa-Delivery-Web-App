using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class EmployeeT
{
    public string EmployeeId { get; set; }

    public string EmployeeFileNum { get; set; }

    public string EmployeeName { get; set; }

    public string Title { get; set; }

    public string EmployeePhone { get; set; }

    public string EmployeePhone1 { get; set; }

    public string EmployeeGov { get; set; }

    public string EmployeeCity { get; set; }

    public string EmployeeRegion { get; set; }

    public string EmployeeStreet { get; set; }

    public int? EmployeeBlockNum { get; set; }

    public int? EmployeeFlatNum { get; set; }

    public string EmployeeDes { get; set; }

    public string EmployeeRelativeName { get; set; }

    public string EmployeeRelativePhone { get; set; }

    public DateTime? EmployeeHireDate { get; set; }

    public sbyte? EmployeePercentage { get; set; }

    public sbyte? EmployeeType { get; set; }

    public string EmployeeImageUrl { get; set; }

    public int? SubscriptionId { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDataComplete { get; set; }

    public ulong IsNewEmployee { get; set; }

    public ulong IsApproved { get; set; }

    public ulong IsFired { get; set; }

    public int SystemId { get; set; }

    public virtual ICollection<BroadcastRequestT> BroadcastRequestT { get; set; } = new List<BroadcastRequestT>();

    public virtual ICollection<ClientSubscriptionT> ClientSubscriptionT { get; set; } = new List<ClientSubscriptionT>();

    public virtual ICollection<DepartmentEmployeeT> DepartmentEmployeeT { get; set; } = new List<DepartmentEmployeeT>();

    public virtual EmployeeLocation EmployeeLocation { get; set; }

    public virtual ICollection<EmployeeReviewT> EmployeeReviewT { get; set; } = new List<EmployeeReviewT>();

    public virtual ICollection<EmployeeWorkplacesT> EmployeeWorkplacesT { get; set; } = new List<EmployeeWorkplacesT>();

    public virtual ICollection<FavouriteEmployeeT> FavouriteEmployeeT { get; set; } = new List<FavouriteEmployeeT>();

    public virtual ICollection<FawryChargeT> FawryChargeT { get; set; } = new List<FawryChargeT>();

    public virtual FiredStaffT FiredStaffT { get; set; }

    public virtual ICollection<IncreaseDiscountT> IncreaseDiscountT { get; set; } = new List<IncreaseDiscountT>();

    public virtual ICollection<InsurancePaymentT> InsurancePaymentT { get; set; } = new List<InsurancePaymentT>();

    public virtual LoginT LoginT { get; set; }

    public virtual ICollection<MessagesT> MessagesT { get; set; } = new List<MessagesT>();

    public virtual OpreationT OpreationT { get; set; }

    public virtual ICollection<RejectRequestT> RejectRequestT { get; set; } = new List<RejectRequestT>();

    public virtual ICollection<RequestT> RequestT { get; set; } = new List<RequestT>();

    public virtual ICollection<SiteT> SiteT { get; set; } = new List<SiteT>();

    public virtual EmployeeSubscriptionT Subscription { get; set; }

    public virtual SystemUserT System { get; set; }

    public virtual ICollection<SystemUserT> SystemUserT { get; set; } = new List<SystemUserT>();

    public virtual ICollection<TimetableT> TimetableT { get; set; } = new List<TimetableT>();

    public virtual ICollection<VacationT> VacationT { get; set; } = new List<VacationT>();
}
