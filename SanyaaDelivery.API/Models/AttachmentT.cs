using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class AttachmentT
    {
        public int AttachmentId { get; set; }
        public int? AttachmentType { get; set; }
        public string ReferenceId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}
