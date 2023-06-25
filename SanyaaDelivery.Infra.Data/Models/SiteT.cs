using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class SiteT
{
    public int SiteId { get; set; }

    public string SiteName { get; set; }

    public string SiteDescreption { get; set; }

    public string SiteEngineer { get; set; }

    public int? ClientId { get; set; }

    public ulong? IsComplete { get; set; }

    public DateTime? CompleteTime { get; set; }

    public DateTime? CreationTime { get; set; }

    public int SystemUserId { get; set; }

    public virtual ClientT Client { get; set; }

    public virtual ICollection<RequestT> RequestT { get; set; } = new List<RequestT>();

    public virtual ICollection<SiteContractT> SiteContractT { get; set; } = new List<SiteContractT>();

    public virtual EmployeeT SiteEngineerNavigation { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
