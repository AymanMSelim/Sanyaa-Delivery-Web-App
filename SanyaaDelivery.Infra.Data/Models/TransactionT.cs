﻿using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class TransactionT
{
    public int Id { get; set; }

    public string Description { get; set; }

    public DateTime? Date { get; set; }

    public int? Amount { get; set; }

    public sbyte? ReferenceType { get; set; }

    public string ReferenceId { get; set; }

    public ulong? IsCanceled { get; set; }

    public DateTime? CreationTime { get; set; }

    public int? SystemUserId { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
