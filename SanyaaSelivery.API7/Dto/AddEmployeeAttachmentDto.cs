using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Dto
{
    public class AddEmployeeAttachmentDto
    {
        public string EmployeeId { get; set; }
        public IFormFile File { get; set; }
        public int Type { get; set; }
    }
}
