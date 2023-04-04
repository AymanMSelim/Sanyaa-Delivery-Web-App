using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class AppLandingIndexDto
    {
        public bool ActiveStatus { get; set; }
        public string EmployeeAmountPercentageCaption { get; set; }
        public string EmployeeAmountPercentage { get; set; }
        public string EmployeeAmountPercentageDescription { get; set; }
        public List<AppRequestDto> ActiveRequestList { get; set; }
    }
}
