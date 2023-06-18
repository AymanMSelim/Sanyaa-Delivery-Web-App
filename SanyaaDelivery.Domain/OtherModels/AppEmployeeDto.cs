using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.OtherModels
{
    public class EmployeeReviewDto
    {
        public int RequestId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Review { get; set; }
        public sbyte? Rate { get; set; }
        public DateTime CreationTime { get; set; }
    }

    public class AppEmployeeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string Title { get; set; }
        public int Rate { get; set; }
        public bool IsFavourite { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public bool ShowCalendar { get; set; }
        public bool ShowContact { get; set; }
        public List<EmployeeReviewDto> EmployeeReviews { get; set; }
    }

    public class AppReviewIndexDto
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Title { get; set; }
        public int Rate { get; set; }
        public string Image { get; set; }
        public List<EmployeeReviewDto> ReviewList { get; set; }
    }
}
