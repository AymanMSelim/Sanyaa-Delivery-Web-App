using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.LightModels
{
    public class BranchLight
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
    }

    public class ClientLight
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int? BranchId { get; set; }
    }

    public class EmployeeLight
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int? SubscriptionId { get; set; }
    }

    public class CountryLight
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }

    public class GovernorateLight
    {
        public int GovernorateId { get; set; }
        public string GovernorateName { get; set; }
        public int? CountryId { get; set; }
    }

    public class CityLight
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int? GovernorateId { get; set; }
    }

    public class RegionLight
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int? CityId { get; set; }
    }

    public class DepartmentLight
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public sbyte? Percentage { get; set; }
    }


    public class DepartmentSub0Light
    {
        public int DepartmentSub0Id { get; set; }
        public string DepartmentName { get; set; }
        public int? DepartmentId { get; set; }
    }

    public class SystemUserLight
    {
        public int SystemUserId { get; set; }
        public string SystemUserName { get; set; }
    }


    public class DepartmentSub1Light
    {
        public int DepartmentSub1Id { get; set; }
        public string DepartmentName { get; set; }
        public int? DepartmentSub0Id { get; set; }
    }

    public class PhoneLight
    {
        public int ClientPhoneId { get; set; }
        public int? ClientId { get; set; }
        public string Phone { get; set; }
    }

    public class EmployeeDepartmentLight
    {
        public int ClientDepartmentId { get; set; }
        public string EmployeeId { get; set; }
        public int DepartmentId { get; set; }
    }

    public class EmployeeWorkplaceLight
    {
        public int EmployeeWorkplaceId { get; set; }
        public string EmployeeId { get; set; }
        public int BranchId { get; set; }
    }
}
