using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Domain.Models
{
    public partial class TransactionT
    {
        public string ReferenceTypeName { get; set; }
        public string IsCanceledDescription { get; set; }
        public string SystemUserName { get; set; }
    }
}
