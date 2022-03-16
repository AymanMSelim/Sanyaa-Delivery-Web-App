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

    public enum LandingScreenItemType
    {
        Banner = 1,
        Offer = 2,
        Department = 3
    }

    public enum AccountType
    {
        User = 1,
        Employee = 2,
        Client = 3,
        
    }

}
