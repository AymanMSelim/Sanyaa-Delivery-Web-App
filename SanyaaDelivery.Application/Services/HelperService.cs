using App.Global.DTOs;
using App.Global.ExtensionMethods;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class HelperService : IHelperService
    {
        private readonly ICityService cityService;
        private readonly IDepartmentService departmentService;
        private readonly IRepository<ClientSubscriptionT> clientSubscriptionRepository;

        public HelperService(ICityService cityService, IDepartmentService departmentService, IRepository<ClientSubscriptionT> clientSubscriptionRepository)
        {
            this.cityService = cityService;
            this.departmentService = departmentService;
            this.clientSubscriptionRepository = clientSubscriptionRepository;
        }
        public async Task<int> GetMinimumCharge(int cityId, int departmentId)
        {
            //var city = await cityService.GetAsync(cityId);
            //var department = await departmentService.GetAsync(departmentId);
            //if(city. > department.)
            return 0;
        }

        public async Task<Result<string>> ValidateClientSubscription(int clientSubscriptionId)
        {
            var clientSubscription = await clientSubscriptionRepository.GetAsync(clientSubscriptionId);
            return ValidateClientSubscription(clientSubscription);
        }

        public Result<string> ValidateClientSubscription(ClientSubscriptionT clientSubscription)
        {
            if (clientSubscription.IsNull())
            {
                return ResultFactory<string>.CreateErrorResponse(message: "Client subscription not found", resultStatusCode: App.Global.Enums.ResultStatusCode.NotFound);
            }
            if (clientSubscription.IsCanceled)
            {
                return ResultFactory<string>.CreateErrorResponse(message: "This subscription is canceled");
            }
            if (clientSubscription.IsActive is false)
            {
                return ResultFactory<string>.CreateErrorResponse(message: "This subscription not activated, please contact us to activate");
            }
            return ResultFactory<string>.CreateSuccessResponse();
        }
    }
}
