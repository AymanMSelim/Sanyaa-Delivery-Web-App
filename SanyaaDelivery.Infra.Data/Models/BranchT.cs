using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class BranchT
{
    public int BranchId { get; set; }

    public string BranchName { get; set; }

    public string BranchPhone { get; set; }

    public string BranchGov { get; set; }

    public string BranchCity { get; set; }

    public string BranchRegion { get; set; }

    public string BranchStreet { get; set; }

    public int? BranchBlockNum { get; set; }

    public int? BranchFlatNum { get; set; }

    public string BranchDes { get; set; }

    public virtual ICollection<CityT> CityT { get; set; } = new List<CityT>();

    public virtual ICollection<ClientT> ClientT { get; set; } = new List<ClientT>();

    public virtual ICollection<EmployeeWorkplacesT> EmployeeWorkplacesT { get; set; } = new List<EmployeeWorkplacesT>();

    public virtual ICollection<ProductT> ProductT { get; set; } = new List<ProductT>();

    public virtual ICollection<RequestT> RequestT { get; set; } = new List<RequestT>();

    public virtual ICollection<SystemUserT> SystemUserT { get; set; } = new List<SystemUserT>();

    public virtual ICollection<WorkingAreaT> WorkingAreaT { get; set; } = new List<WorkingAreaT>();
}
