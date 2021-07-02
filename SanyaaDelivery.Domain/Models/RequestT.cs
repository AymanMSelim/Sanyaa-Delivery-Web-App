﻿using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class RequestT
    {
        public RequestT()
        {
            BillNumberT = new HashSet<BillNumberT>();
            FawryChargeRequestT = new HashSet<FawryChargeRequestT>();
            FollowUpT = new HashSet<FollowUpT>();
            PartinerPaymentRequestT = new HashSet<PartinerPaymentRequestT>();
            RequestCanceledT = new HashSet<RequestCanceledT>();
            RequestComplaintT = new HashSet<RequestComplaintT>();
            RequestDelayedT = new HashSet<RequestDelayedT>();
            RequestServicesT = new HashSet<RequestServicesT>();
        }

        public int RequestId { get; set; }
        public DateTime RequestCurrentTimestamp { get; set; }
        public DateTime? RequestTimestamp { get; set; }
        public string RequestNote { get; set; }
        public sbyte RequestStatus { get; set; }
        public int SystemUserId { get; set; }
        public int ClientId { get; set; }
        public string EmployeeId { get; set; }
        public int BranchId { get; set; }

        public BranchT Branch { get; set; }
        public ClientT Client { get; set; }
        public EmployeeT Employee { get; set; }
        public SystemUserT SystemUser { get; set; }
        public PaymentT PaymentT { get; set; }
        public RequestStagesT RequestStagesT { get; set; }
        public ICollection<BillNumberT> BillNumberT { get; set; }
        public ICollection<FawryChargeRequestT> FawryChargeRequestT { get; set; }
        public ICollection<FollowUpT> FollowUpT { get; set; }
        public ICollection<PartinerPaymentRequestT> PartinerPaymentRequestT { get; set; }
        public ICollection<RequestCanceledT> RequestCanceledT { get; set; }
        public ICollection<RequestComplaintT> RequestComplaintT { get; set; }
        public ICollection<RequestDelayedT> RequestDelayedT { get; set; }
        public ICollection<RequestServicesT> RequestServicesT { get; set; }
    }
}
