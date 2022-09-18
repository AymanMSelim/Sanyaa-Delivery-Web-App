using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class DepartmentSubscription : SubscriptionT
    {
        public DepartmentSubscription(SubscriptionT subscription)
        {
            SubscriptionId = subscription.SubscriptionId;
            SubscriptionName = subscription.SubscriptionName;
            Description = subscription.Description;
            DepartmentId = subscription.DepartmentId;
            RequestNumberPerMonth = subscription.RequestNumberPerMonth;
            IsActive = subscription.IsActive;
            Department = subscription.Department;
            ClientSubscriptionT = subscription.ClientSubscriptionT;
            SubscriptionSequenceT = subscription.SubscriptionSequenceT;
            if(Department != null)
            {
                DepartmentName = Department.DepartmentName;
            }
        }
        public string DepartmentName { get; set; }
    }
}
