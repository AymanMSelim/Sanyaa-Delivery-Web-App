using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.OtherModels
{
    public class AddRequestDto
    {
        public int? ClientId { get; set; }
        public int AddressId { get; set; }
        public int PhoneId { get; set; }
        public DateTime? RequestTime { get; set; }
        public string EmployeeId { get; set; }
    }
}
