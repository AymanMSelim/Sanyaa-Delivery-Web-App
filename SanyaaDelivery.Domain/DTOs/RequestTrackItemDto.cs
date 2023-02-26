using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class RequestTrackItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string ActionTime { get; set; }
        public bool ShowEmployee { get; set; }
        public bool ShowReview { get; set; }
        public bool ShowCompliment { get; set; }
    }
}
