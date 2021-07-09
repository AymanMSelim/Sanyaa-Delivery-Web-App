using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.ActionsFilter
{
    public class CheckOrderCountParameterActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            object employeeId; object day;
            if(context.ActionArguments.TryGetValue("employeeId", out employeeId))
            {
                if(employeeId == null || string.IsNullOrEmpty(employeeId.ToString()))
                {
                    context.Result = new BadRequestObjectResult(new { Message = "employeeId can't by empty"});
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult(new { Message = "employeeId can't by empty" });
            }
            if (context.ActionArguments.TryGetValue("day", out day))
            {
                if (day == null || string.IsNullOrEmpty(day.ToString()))
                {
                    context.Result = new BadRequestObjectResult(new { Message = "day can't by empty" });
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult(new { Message = "day can't by empty" });
            }

            

        }
    }
}
