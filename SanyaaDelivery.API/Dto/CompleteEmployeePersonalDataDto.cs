using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Dto
{
    public class CompleteEmployeePersonalDataDto
    {
        public string PhoneNumber { get; set; }
        public IFormFile ProfilePic { get; set; }
        public IFormFile NationalIdFront { get; set; }
        public IFormFile NationalIdBack { get; set; }
        public string NationalId { get; set; }
        public string RelativeName { get; set; }
        public string RelativePhone { get; set; }
    }
}
