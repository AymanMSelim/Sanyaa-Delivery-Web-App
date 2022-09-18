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

        public static OpreationResultMessage<T> PublishExceptionReturnResponse<T>(Exception ex)
        {
            return OpreationResultMessageFactory<T>.CreateExceptionResponse(ex);
        }

        public static OpreationResultMessage<object> PublishExceptionReturnResponse(Exception ex)
        {
            return OpreationResultMessageFactory<object>.CreateExceptionResponse(ex);
        }
    }
}
