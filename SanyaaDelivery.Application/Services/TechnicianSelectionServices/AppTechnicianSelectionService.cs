using App.Global.DTOs;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services.TechnicianSelectionServices
{
    public class AppTechnicianSelectionService : ITechnicianSelection
    {
        private readonly IRepository<RequestT> requestRepository;

        public AppTechnicianSelectionService(IRepository<RequestT> requestRepository)
        {
            this.requestRepository = requestRepository;
        }
        public async Task<Result<T>> SelectAsync<T>(RequestT request, string employeeId = null)
        {
            request.EmployeeId = employeeId;
            if (string.IsNullOrEmpty(employeeId))
            {
                request.RequestStatus = GeneralSetting.GetRequestStatusId(Domain.Enum.RequestStatus.NotSet);
            }
            else
            {
                request.RequestStatus = GeneralSetting.GetRequestStatusId(Domain.Enum.RequestStatus.Waiting);
            }
            requestRepository.Update(request.RequestId, request);
            var affectedRows = await requestRepository.SaveAsync();
            return ResultFactory<T>.CreateAffectedRowsResult(affectedRows);
        }
    }
}
