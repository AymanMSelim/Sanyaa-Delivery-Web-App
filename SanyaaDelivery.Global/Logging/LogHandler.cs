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

        public static Result<T> PublishExceptionReturnResponse<T>(Exception ex)
        {
            return ResultFactory<T>.CreateExceptionResponse(ex);
        }

        public static Result<object> PublishExceptionReturnResponse(Exception ex)
        {
            return ResultFactory<object>.CreateExceptionResponse(ex);
        }
    }
}
