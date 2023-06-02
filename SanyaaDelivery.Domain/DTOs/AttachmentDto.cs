using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class AttachmentDto
    {
        public int Id { get; set; }
        public string  Url { get; set; }
        public string  DomainUrl { get; set; }
        public string Path { get; set; }
    }
}
