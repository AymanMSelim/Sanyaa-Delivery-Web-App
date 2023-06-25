using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class CartT
{
    public int CartId { get; set; }

    public int DepartmentId { get; set; }

    public int ClientId { get; set; }

    public ulong IsViaApp { get; set; }

    public string Note { get; set; }

    public int? PromocodeId { get; set; }

    public ulong UsePoint { get; set; }

    public ulong HaveRequest { get; set; }

    public DateTime? CreationTime { get; set; }

    public DateTime? ModificationTime { get; set; }

    public virtual ICollection<CartDetailsT> CartDetailsT { get; set; } = new List<CartDetailsT>();

    public virtual ClientT Client { get; set; }

    public virtual DepartmentT Department { get; set; }

    public virtual PromocodeT Promocode { get; set; }

    public virtual ICollection<RequestT> RequestT { get; set; } = new List<RequestT>();
}
