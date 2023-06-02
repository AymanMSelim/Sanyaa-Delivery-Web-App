using App.Global.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.ActionsFilter
{
    public class UnauthorizedResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public UnauthorizedResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Call the next middleware component
            await _next(context);

            // Check if the response status code is 401 Unauthorized
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                Result<object> result;
                if (IsTokenExpire(context))
                {
                     result = ResultFactory<object>.CreateErrorResponseMessageFD("Your session has expired. Please logout and login again",
                         App.Global.Enums.ResultStatusCode.TokenExpired);
                }
                else
                {
                    result = ResultFactory<object>.CreateErrorResponseMessageFD("You are not authorized to access the service",
                        App.Global.Enums.ResultStatusCode.NotAuthenticated);
                }

                // Set the response status code and content type
                //context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";

                // Write the JSON response to the response body
                await context.Response.WriteAsync(App.Global.Serialization.Json.SerializeCamelCase(result));
            }

            if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                var result = ResultFactory<object>.CreateErrorResponseMessageFD("You are not authorized to access the service",
                    App.Global.Enums.ResultStatusCode.NotAuthorized);

                // Set the response status code and content type
                //context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";

                // Write the JSON response to the response body
                await context.Response.WriteAsync(App.Global.Serialization.Json.SerializeCamelCase(result));
            }
        }

        private bool IsTokenExpire(HttpContext context)
        {
            try
            {
                // Parse the bearer token from the request header
                var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                {
                    return false;
                }
                // Decode the token to get the expiration time
                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadJwtToken(token);
                var exp = decodedToken.Payload.Exp;

                // Convert the expiration time to a DateTime object
                var expDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddSeconds(Convert.ToDouble(exp))
                    .ToLocalTime();

                // Check if the token is expired
                if (expDateTime < DateTime.Now)
                {
                    // Return a message to the user
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
                return false;
            }
           
        }
    }
}

