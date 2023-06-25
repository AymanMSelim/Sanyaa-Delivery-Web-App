using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Dto
{
    public class AddFormAttachmentDto
    {
        public int ReferenceType { get; set; }
        public string ReferenceId { get; set; }
        public IFormFile File { get; set; }
        public string FileName { get; set; }
    }
}
