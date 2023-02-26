using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class BranchT
    {
        public BranchT()
        {
            CityT = new HashSet<CityT>();
            ClientT = new HashSet<ClientT>();
            EmployeeWorkplacesT = new HashSet<EmployeeWorkplacesT>();
            ProductT = new HashSet<ProductT>();
            RequestT = new HashSet<RequestT>();
            SystemUserT = new HashSet<SystemUserT>();
            WorkingAreaT = new HashSet<WorkingAreaT>();
        }

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

        public ICollection<CityT> CityT { get; set; }
        public ICollection<ClientT> ClientT { get; set; }
        public ICollection<EmployeeWorkplacesT> EmployeeWorkplacesT { get; set; }
        public ICollection<ProductT> ProductT { get; set; }
        public ICollection<RequestT> RequestT { get; set; }
        public ICollection<SystemUserT> SystemUserT { get; set; }
        public ICollection<WorkingAreaT> WorkingAreaT { get; set; }
    }
}
