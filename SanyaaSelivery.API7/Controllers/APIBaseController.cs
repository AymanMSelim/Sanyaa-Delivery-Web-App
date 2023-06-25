using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.API.ActionsFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    [Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [GlobalExceptionFilterAttribute]
    public class APIBaseController : ControllerBase
    {
        private readonly CommonService commonService;

        //private readonly CommonService commonService;

        public APIBaseController(CommonService commonService)
        {
            this.commonService = commonService;
            //var serviceProvider = HttpContext.RequestServices;
            //this.commonService = serviceProvider.GetService(typeof(CommonService)) as CommonService;
        }

        //public string Host => commonService.GetHost();
        //public bool IsViaApp => commonService.IsViaApp();
        //public bool IsClientViaApp => commonService.IsViaClientApp();
        //public bool IsEmployeeViaApp => commonService.IsViaEmpApp();
    }
}
