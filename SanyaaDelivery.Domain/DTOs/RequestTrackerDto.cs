using SanyaaDelivery.Domain.OtherModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class RequestTrackerDto
    {
        public List<RequestTrackItemDto> RequestTrackItemList { get; set; }
        public AppEmployeeDto SelectedEmployee { get; set; }
        public int SelectedTrackId { get; set; }
        public bool IsReqestCanceled { get; set; }
    }
}
