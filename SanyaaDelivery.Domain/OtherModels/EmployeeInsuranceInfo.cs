using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.OtherModels
{
    public class EmployeeInsuranceInfo
    {
        public string EmployeeId { get; set; }         
        public int SubsriptionId { get; set; }         
        public string SubsriptionName { get; set; }         
        public int TotalAdd { get; set; }         
        public int TotalWithdraw { get; set; }         
        public int InsuranceAmount { get; set; }         
        public int PaidAmount { get; set; }         
        public int RemainAmount { get; set; }         
        public int InsuranceMinAmount { get; set; }         
        public int RemainMinAmount { get; set; }         
        public bool IsCompleteMinAmount { get; set; }         
        public bool IsCompleteInsuranceAmount { get; set; }         
    }
}
