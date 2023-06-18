using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class EmpOptionalAttachmentIndexDto
    {
        public string EmpoyeeId { get; set; }
        public List<EmpOptionalAttachmentItemDto> ItemList { get; set; }
    }

    public class EmpOptionalAttachmentItemDto
    {
        public string Description { get; set; }
        public int Type { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
