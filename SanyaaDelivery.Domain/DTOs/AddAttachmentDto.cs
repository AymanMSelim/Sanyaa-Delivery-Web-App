using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class AddAttachmentDto
    {
        public int ReferenceType { get; set; }
        public string ReferenceId { get; set; }
        public byte[] File { get; set; }
        public string FileName { get; set; }
    }
}
