﻿using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class SubscriptionSequenceT
{
    public int ClientSubscriptionSequenceId { get; set; }

    public int SubscriptionServiceId { get; set; }

    public sbyte Sequence { get; set; }

    public decimal DiscountPercentage { get; set; }

    public decimal CompanyDiscountPercentage { get; set; }

    public virtual SubscriptionServiceT SubscriptionService { get; set; }
}
