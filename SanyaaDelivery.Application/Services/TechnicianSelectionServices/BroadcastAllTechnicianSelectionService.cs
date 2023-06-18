using App.Global.DTOs;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services.TechnicianSelectionServices
{
    class BroadcastAllTechnicianSelectionService : ITechnicianSelection
    {
        public BroadcastAllTechnicianSelectionService(IOperationService operationService)
        {
            OperationService = operationService;
        }

        public IOperationService OperationService { get; }

        public async Task<Result<List<BroadcastRequestT>>> SelectAsync(int requestId)
        {
            var broadcastResult = await OperationService.BroadcastAsync(requestId);
            return broadcastResult;
        }
    }
}
