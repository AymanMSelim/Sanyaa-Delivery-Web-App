using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class RequestDto
    {
        public int RequestId { get; set; }
        public DateTime RequestTimestamp { get; set; }
        public string RequestNote { get; set; }
        public string BranchName { get; set; }
        public int BranchId { get; set; }
        public int? CityId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientAddress { get; set; }
        public string RequestStatusName { get; set; }
        public int RequestStatus { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string ServicesNames { get; set; }
        public bool IsCanceled { get; set; }
        public bool EmployeeAccountState { get; set; }
        public decimal CustomerPrice { get; set; }
        public decimal NetPrice { get; set; }
        public decimal CompanyPercentageAmount { get; set; }
        public string SystemUserName { get; set; }
        public int SystemUserId { get; set; }
        public int? SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public int DepartmentId { get; set; }
        public string DeparmentName { get; set; }
    }
}
