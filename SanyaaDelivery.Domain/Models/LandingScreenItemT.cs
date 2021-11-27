using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class LandingScreenItemT
    {
        public LandingScreenItemT()
        {
            InverseParentSection = new HashSet<LandingScreenItemT>();
        }

        public int LandingScreenItemId { get; set; }
        public sbyte? ItemType { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemImagePath { get; set; }
        public string ItemUrl { get; set; }
        public string ApiUrl { get; set; }
        public string ReferenceId { get; set; }
        public int? ParentSectionId { get; set; }

        public LandingScreenItemT ParentSection { get; set; }
        public ICollection<LandingScreenItemT> InverseParentSection { get; set; }
    }
}
