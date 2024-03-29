﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.Enum
{
    public enum HttpResponseStatus
    {
        Failed = 0,
        Sucsess = 1
    }

    public enum EmployeeRegisterStep
    {
        Register = 1,
        ConfirmMobile = 2,
        CompleteNationalAndRelative = 3,
        CompleteAddress = 4,
        CompleteDepartmentAndWorkplace = 5,
        AdditionNonMandatoryData = 6,
        Done = 7
    }

    public enum ClientPointType
    {
        Withdraw = 1,
        Add = 2
    }

    public enum InsurancePaymentType
    {
        Withdraw = 1,
        Add = 2
    }

    public enum FawryRequestType
    {
        Request = 1,
        Insurance = 2
    }


    public enum LandingScreenItemType
    {
        Banner = 1,
        Offer = 2,
        Department = 3,
        Video = 4,
        Link = 5
    }

    public enum AccountType
    {
        User = 1,
        Employee = 2,
        Client = 3,
        
    }

    public enum BroadcastStatus
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3
    }

    public enum AttachmentType
    {
        CartImage = 1,
        ProfilePicture = 2,
        NationalIdFront = 3,
        NationalIdBack = 4,
        AppLandingItem = 5,
        ArmyCertificate = 6,
        DrivingLicense = 7,
        QualificationCertificate = 8,
        CriminalRecord = 9,
        ElectricReceipt = 10
    }

    public enum TechnicianSelectionType
    {
        None = 0,
        App = 1,
        BroadcastAll = 2,
        BroadcastFavourite = 3,
        SentToApprove = 4
    }

    public enum PromocodeType
    {
        Value = 1,
        Precentage = 2,
    }


    public enum RequestStatus
    {
        Canceled = 0,
        Delayed = 1,
        Rejected = 2,
        StartExcution = 3,
        Broadcast = 4,
        WaitingApprove = 5,
        NotSet = 10,
        Waiting = 11,
        Revieved = 12,
        InExcution = 13,
        Done = 14,
        FollowUp = 20
    }

    public enum EmployeeRequestTrackingItem
    {
        Waiting = 1,
        GoToClient = 2,
        InExcution = 3,
        DoneOrCanceled = 4,
    }
}
