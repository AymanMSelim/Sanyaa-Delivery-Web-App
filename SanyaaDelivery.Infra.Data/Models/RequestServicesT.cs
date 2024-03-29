﻿using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class RequestServicesT
{
    public int RequestServiceId { get; set; }

    public int RequestId { get; set; }

    public int ServiceId { get; set; }

    public int RequestServicesQuantity { get; set; }

    public DateTime AddTimestamp { get; set; }

    public decimal ServicePrice { get; set; }

    public decimal ServiceDiscount { get; set; }

    public decimal ServiceMaterialCost { get; set; }

    public int ServicePoint { get; set; }

    public int? SystemUserId { get; set; }

    public virtual RequestT Request { get; set; }

    public virtual ServiceT Service { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
