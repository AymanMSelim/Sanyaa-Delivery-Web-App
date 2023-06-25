using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    [Authorize]
    public class APIBaseAuthorizeController : APIBaseController
    {
        public APIBaseAuthorizeController(CommonService commonService) : base(commonService)
        {

        }
    }
}
