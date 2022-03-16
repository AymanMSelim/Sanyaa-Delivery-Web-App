using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class SiteT
    {
        public SiteT()
        {
            SiteContractT = new HashSet<SiteContractT>();
            SiteRequestT = new HashSet<SiteRequestT>();
        }

        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteDescreption { get; set; }
        public string SiteEngineer { get; set; }
        public int? ClientId { get; set; }
        public bool? IsComplete { get; set; }
        public DateTime? CompleteTime { get; set; }
        public DateTime? CreationTime { get; set; }
        public int SystemUserId { get; set; }

        public ClientT Client { get; set; }
        public EmployeeT SiteEngineerNavigation { get; set; }
        public SystemUserT SystemUser { get; set; }
        public ICollection<SiteContractT> SiteContractT { get; set; }
        public ICollection<SiteRequestT> SiteRequestT { get; set; }
    }
}
