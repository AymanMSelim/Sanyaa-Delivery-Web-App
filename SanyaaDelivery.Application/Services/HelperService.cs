using SanyaaDelivery.Application.Interfaces;
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

        public HelperService(ICityService cityService, IDepartmentService departmentService)
        {
            this.cityService = cityService;
            this.departmentService = departmentService;
        }
        public async Task<int> GetMinimumCharge(int cityId, int departmentId)
        {
            //var city = await cityService.GetAsync(cityId);
            //var department = await departmentService.GetAsync(departmentId);
            //if(city. > department.)
            return 0;
        }
    }
}
