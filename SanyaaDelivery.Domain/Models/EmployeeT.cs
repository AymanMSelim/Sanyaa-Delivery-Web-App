using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class EmployeeT
    {
        public EmployeeT()
        {
            DepartmentEmployeeT = new HashSet<DepartmentEmployeeT>();
            EmployeeWorkplacesT = new HashSet<EmployeeWorkplacesT>();
            FawryChargeT = new HashSet<FawryChargeT>();
            IncreaseDiscountT = new HashSet<IncreaseDiscountT>();
            MessagesT = new HashSet<MessagesT>();
            RejectRequestT = new HashSet<RejectRequestT>();
            RequestT = new HashSet<RequestT>();
            SiteT = new HashSet<SiteT>();
            SystemUserT = new HashSet<SystemUserT>();
            TimetableT = new HashSet<TimetableT>();
        }

        public string EmployeeId { get; set; }
        public string EmployeeFileNum { get; set; }
        public string EmployeeName { get; set; }
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
        public double EmployeePercentage { get; set; }
        public sbyte EmployeeType { get; set; }

        public EmployeeLocation EmployeeLocation { get; set; }
        public FiredStaffT FiredStaffT { get; set; }
        public LoginT LoginT { get; set; }
        public ICollection<DepartmentEmployeeT> DepartmentEmployeeT { get; set; }
        public ICollection<EmployeeWorkplacesT> EmployeeWorkplacesT { get; set; }
        public ICollection<FawryChargeT> FawryChargeT { get; set; }
        public ICollection<IncreaseDiscountT> IncreaseDiscountT { get; set; }
        public ICollection<MessagesT> MessagesT { get; set; }
        public ICollection<RejectRequestT> RejectRequestT { get; set; }
        public ICollection<RequestT> RequestT { get; set; }
        public ICollection<SiteT> SiteT { get; set; }
        public ICollection<SystemUserT> SystemUserT { get; set; }
        public ICollection<TimetableT> TimetableT { get; set; }
    }
}
