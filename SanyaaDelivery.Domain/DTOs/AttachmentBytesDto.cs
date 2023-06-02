using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class AttachmentBytesDto
    {
        public int Id { get; set; }
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string FileExtention { get; set; }
    }
}
