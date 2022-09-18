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

}
