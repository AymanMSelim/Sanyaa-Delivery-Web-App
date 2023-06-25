using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using App.Global.DateTimeHelper;
using Microsoft.AspNetCore.Http;
using SanyaaDelivery.Application;

namespace SanyaaDelivery.API.ActionsFilter
{
    public class RequestTimerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor context;

        public RequestTimerMiddleware(RequestDelegate next, IHttpContextAccessor context)
        {
            _next = next;
            this.context = context;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentRequest = GeneralSetting.CurrentRequest++;
            var message = $"Time {DateTime.Now.EgyptTimeNow()} Request #{currentRequest} {context.Request.Path} Start\n";
            System.IO.File.AppendAllText($@"wwwroot\timer\timer-{DateTime.Now.EgyptTimeNow().ToString("yyyy-MM-dd HH")}.txt", message);
            var sw = new Stopwatch();
            sw.Start();
            await _next(context);
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;
            message = $"Time {DateTime.Now.EgyptTimeNow()} Request #{currentRequest} {context.Request.Path} End, Elapsed {elapsed} Milliseconds, Status code = {context.Response.StatusCode}\n";
            System.IO.File.AppendAllText($@"wwwroot\timer\timer-{DateTime.Now.EgyptTimeNow().ToString("yyyy-MM-dd HH")}.txt", message);
            // Do something with elapsed time, such as logging or storing in a database
        }
    }
}
