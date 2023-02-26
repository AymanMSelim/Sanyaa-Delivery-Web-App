using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class AppLandingScreenItemT
    {
        public AppLandingScreenItemT()
        {
            LandingScreenItemDetailsT = new HashSet<LandingScreenItemDetailsT>();
        }

        public int ItemId { get; set; }
        public int? ItemType { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public int? DepartmentId { get; set; }
        public bool IsActive { get; set; }
        public bool HavePackage { get; set; }
        public string ActionLink { get; set; }

        public ICollection<LandingScreenItemDetailsT> LandingScreenItemDetailsT { get; set; }
    }
}
