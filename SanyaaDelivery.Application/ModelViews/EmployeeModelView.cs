using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.ModelViews
{
    public class EmployeeModelView
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public bool IsActive { get; set; }

        public string Deparment { get; set; }

        public bool IsFreeToday { get; set; }

        public DateTime Timestamp { get; set; }

        public DateTime LastSeenTime { get; set; }

    }
}
