using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class TranslatorT
    {
        public int TranslatorId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public sbyte? ReferenceType { get; set; }
        public string ReferenceId { get; set; }
        public int LangId { get; set; }
    }
}
