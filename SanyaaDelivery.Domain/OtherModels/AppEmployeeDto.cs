using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.OtherModels
{
    public class EmployeeReviewDto
    {
        public int RequestId { get; set; }
        public string ClientName { get; set; }
        public string Review { get; set; }
        public int Rate { get; set; }
    }

    public class AppEmployeeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Rate { get; set; }
        public bool IsFavourite { get; set; }
        public string Image { get; set; }
        public List<EmployeeReviewDto> EmployeeReviews { get; set; }
    }
}
