using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs.Lookup
{
    public class LookupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DepartmentLookupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class CityLookupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GovernorateLookupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
