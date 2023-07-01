using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly SanyaaDatabaseContext context;

        public TaskService(IServiceProvider serviceProvider)
        {
            this.context = serviceProvider.GetRequiredService<SanyaaDatabaseContext>();
        }
        public async Task<BatchResponse> BroadcastNotificationTask()
        {
            var tokenList = await (from request in context.RequestT
                             join broadcastRequest in context.BroadcastRequestT
                             on new { Id = request.RequestId, S = "Pending" } equals new { Id = broadcastRequest.RequestId, S = broadcastRequest.Status }
                             join account in context.AccountT
                             on new { EmpId = broadcastRequest.EmployeeId, TypeId = GeneralSetting.EmployeeAccountTypeId, Token = true }
                             equals new { EmpId = account.AccountReferenceId, TypeId = account.AccountTypeId, Token = account.FcmToken != null }
                             where request.IsCanceled == false
                             && request.IsCanceled == false
                             && request.EmployeeId == null
                             select account.FcmToken).Distinct().ToListAsync();
            return await App.Global.Firebase.FirebaseMessaging.SendMulticastToEmpAsync(tokenList, "طلب جديد", "لديك طلب جديد, من فضلك قم بالتفاعل معه سريعا");
        }

        public Task BroadcastTask(int requestId)
        {
            throw new NotImplementedException();

        }
    }
}
