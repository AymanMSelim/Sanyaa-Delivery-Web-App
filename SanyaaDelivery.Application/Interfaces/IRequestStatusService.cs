using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IRequestStatusService
    {
        Task<List<RequestStatusT>> GetListAsync(int? groupId = null);
        Task<List<RequestStatusGroupT>> GetGroupListAsync();
    }
}
