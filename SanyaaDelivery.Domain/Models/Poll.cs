using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class Poll
    {
        public int RequestId { get; set; }
        public string Employee { get; set; }
        public string Time { get; set; }
        public string Employee2 { get; set; }
        public string Price { get; set; }
        public string Place { get; set; }
        public string Knowme { get; set; }
        public string Note { get; set; }
        public string Vote { get; set; }
    }
}
