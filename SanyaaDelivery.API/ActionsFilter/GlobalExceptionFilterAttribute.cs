using App.Global.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.ActionsFilter
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            App.Global.Logging.LogHandler.PublishException(exception);
            var response = OpreationResultMessageFactory<string>.CreateExceptionResponse(exception);
            context.Result = new ContentResult
            {
                Content = App.Global.Serialization.Json.Serialize(response),
                ContentType = "application/json",
                StatusCode = (int?)HttpStatusCode.InternalServerError
            };
        }

        //public override Task OnExceptionAsync(ExceptionContext context)
        //{
        //    return Task.Run(() =>
        //    {
        //        var exception = context.Exception;
        //        App.Global.Logging.LogHandler.PublishException(exception);
        //        var response = HttpResponseDtoFactory<string>.CreateExceptionResponse(exception);
        //        context.Result = new ContentResult
        //        {
        //            Content = App.Global.Serialization.Json.Serialize(response),
        //            ContentType = "application/json",
        //            StatusCode = (int?)HttpStatusCode.InternalServerError
        //        };
        //    });
        //}
    }
}
