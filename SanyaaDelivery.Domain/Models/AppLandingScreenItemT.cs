using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class AppLandingScreenItemT
    {
        public int ItemId { get; set; }
        public int? ItemType { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public int? DepartmentId { get; set; }
    }
}
