using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.Enum
{
    public enum HttpResponseStatus
    {
        Failed = 0,
        Sucsess = 1
    }

    public enum ClientPointType
    {
        Add = 1,
        Withdraw = 2
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

    public enum AttachmentType
    {
        CartImage = 1,
    }

    public enum TechnicianSelectionType
    {
        Manual,
        Auto,
        App
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
        Accepted = 3,
        NotSet = 10,
        Waiting = 11,
        Revieved = 12,
        InExcution = 13,
        Done = 14,
        FollowUp = 20
    }

}
