using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class EmployeeWorkingDataDto
    {
        public string NationalId { get; set; }
        public List<int> Departments { get; set; }
        public int CityId { get; set; }
    }
}
