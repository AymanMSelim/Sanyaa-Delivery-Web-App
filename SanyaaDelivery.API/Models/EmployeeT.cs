using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class EmployeeT
    {
        public EmployeeT()
        {
            ClientSubscriptionT = new HashSet<ClientSubscriptionT>();
            DepartmentEmployeeT = new HashSet<DepartmentEmployeeT>();
            EmployeeReviewT = new HashSet<EmployeeReviewT>();
            EmployeeWorkplacesT = new HashSet<EmployeeWorkplacesT>();
            FavouriteEmployeeT = new HashSet<FavouriteEmployeeT>();
            FawryChargeT = new HashSet<FawryChargeT>();
            IncreaseDiscountT = new HashSet<IncreaseDiscountT>();
            InsurancePaymentT = new HashSet<InsurancePaymentT>();
            MessagesT = new HashSet<MessagesT>();
            RejectRequestT = new HashSet<RejectRequestT>();
            RequestT = new HashSet<RequestT>();
            SiteT = new HashSet<SiteT>();
            SystemUserT = new HashSet<SystemUserT>();
            TimetableT = new HashSet<TimetableT>();
            VacationT = new HashSet<VacationT>();
        }

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
        public DateTime EmployeeHireDate { get; set; }
        public sbyte? EmployeePercentage { get; set; }
        public sbyte? EmployeeType { get; set; }
        public string EmployeeImageUrl { get; set; }
        public int? SubscriptionId { get; set; }

        public EmployeeSubscriptionT Subscription { get; set; }
        public EmployeeLocation EmployeeLocation { get; set; }
        public FiredStaffT FiredStaffT { get; set; }
        public LoginT LoginT { get; set; }
        public ICollection<ClientSubscriptionT> ClientSubscriptionT { get; set; }
        public ICollection<DepartmentEmployeeT> DepartmentEmployeeT { get; set; }
        public ICollection<EmployeeReviewT> EmployeeReviewT { get; set; }
        public ICollection<EmployeeWorkplacesT> EmployeeWorkplacesT { get; set; }
        public ICollection<FavouriteEmployeeT> FavouriteEmployeeT { get; set; }
        public ICollection<FawryChargeT> FawryChargeT { get; set; }
        public ICollection<IncreaseDiscountT> IncreaseDiscountT { get; set; }
        public ICollection<InsurancePaymentT> InsurancePaymentT { get; set; }
        public ICollection<MessagesT> MessagesT { get; set; }
        public ICollection<RejectRequestT> RejectRequestT { get; set; }
        public ICollection<RequestT> RequestT { get; set; }
        public ICollection<SiteT> SiteT { get; set; }
        public ICollection<SystemUserT> SystemUserT { get; set; }
        public ICollection<TimetableT> TimetableT { get; set; }
        public ICollection<VacationT> VacationT { get; set; }
    }
}
