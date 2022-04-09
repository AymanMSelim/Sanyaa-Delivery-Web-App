using App.Global.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Global.Logging
{
    public static class LogHandler
    {
        public static void PublishException(Exception ex)
        {

        }

        public static HttpResponseDto<T> PublishExceptionReturnResponse<T>(Exception ex)
        {
            return HttpResponseDtoFactory<T>.CreateExceptionResponse(ex);
        }

        public static HttpResponseDto<object> PublishExceptionReturnResponse(Exception ex)
        {
            return HttpResponseDtoFactory<object>.CreateExceptionResponse(ex);
        }
    }
}
