using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ITaskService
    {
        Task BroadcastTask(int requestId);

        Task<BatchResponse> BroadcastNotificationTask();
    }
}
