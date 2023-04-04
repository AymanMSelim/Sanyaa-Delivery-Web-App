using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Domain.OtherModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class AppRequestDto
    {
        public int RequestId { get; set; }
        public string  RequestCaption { get; set; }
        public string  Department { get; set; }
        public string  Date { get; set; }
        public string  DayOfWeek { get; set; }
        public string  Time { get; set; }
        public string  Services { get; set; }
        public string  RequestStatus { get; set; }
    }

    public class AppRequestDetailsDto
    {
        public int RequestId { get; set; }
        public string RequestCaption { get; set; }
        public string Date { get; set; }
        public DateTime RequestTimestamp { get; set; }
        public string DayOfWeek { get; set; }
        public string Time { get; set; }
        public string RequestStatus { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string StreetName { get; set; }
        public string Location { get; set; }
        public string Longitude { get; set; }
        public int FlatNumber { get; set; }
        public int FloorNumber { get; set; }
        public string Latitude { get; set; }
        public string Phone { get; set; }
        public int DepartmentId{ get; set; }
        public string DepartmentName { get; set; }
        public bool ShowAddServiceButton { get; set; }
        public bool ShowCancelRequestButton { get; set; }
        public bool ShowDelayRequestButton { get; set; }
        public bool ShowReAssignEmployeeButton { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsCompleted { get; set; }
        public AppEmployeeDto Employee { get; set; }
        public Dictionary<string, decimal> InvoiceDetails { get; set; }
        public List<RequestServiceDto> RequestServiceList { get; set; }
    }

    public class RequestServiceDto
    {
        public int RequestServiceId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal NetPrice { get; set; }
    }

    public class RequestStatusDto
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
    }

    public class RequestGroupStatusDto
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
    }


}

