using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{

    public class EmployeeAppTrackingItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
    }
    public class EmployeeAppTrackingDto
    {
        public List<EmployeeAppTrackingItemDto> TrackingItemList { get; set; }
        public int SelectedItemId { get; set; }
    }

    public class EmpAppRequestDetailsDto
    {
        public int RequestId { get; set; }
        public int CartId { get; set; }
        public string RequestCaption { get; set; }
        public string EmployeeId { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public int? FlatNumber { get; set; }
        public int? BlockNumber { get; set; }
        public string Location { get; set; }
        public string Lng { get; set; }
        public string Lat { get; set; }
        public string AddressDescription { get; set; }
        public string ClientName { get; set; }
        public string Phone { get; set; }
        public List<InvoiceDetailsDto> InvoiceDetails { get; set; }
        public List<RequestServiceDto> RequestServices { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
        public bool CanAddOrUpdate { get; set; }
        public bool CanRejectRequest { get; set; }
        public int DepartmentId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public EmployeeAppTrackingDto Tracking { get; set; }
        public bool ShowCallClientButton { get; set; }
        public bool ShowArrivalButton { get; set; }
        public bool ShowEndRequestButton { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
    }
}
