using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.ActionsFilter
{
    public class GlobalCommonActionFilter : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    if (!context.ModelState.IsValid)
        //    {
        //        // I know that this line indicates a BadRequestObjectResult
        //        // but I don't know how to returning like view if
        //        // the Web App use razor pages

        //        context.Result = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, output, actionContext.ControllerContext.Configuration.Formatters.JsonFormatter);
        //    }
        //    base.OnActionExecuting(context);
        //}
        //override    
    }
}
