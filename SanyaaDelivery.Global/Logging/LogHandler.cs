using App.Global.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using App.Global.DateTimeHelper;
namespace App.Global.Logging
{
    public static class LogHandler
    {
        public static void PublishException(Exception ex)
        {
            try
            {
                var folderPath = $@"wwwroot\Logs";
                string filePath = $@"{folderPath}\exceptions{DateTime.Now.EgyptTimeNow().ToString("yyyy-MM-dd")}.txt";
                if (Directory.Exists(folderPath) == false)
                {
                    Directory.CreateDirectory(folderPath);
                }
                string exceptionMessage = string.Format("Exception Message: {0}", ex.Message);
                string stackTrace = string.Format("Stack Trace: {0}", ex.StackTrace);
                string innerException = (ex.InnerException != null) ? string.Format("Inner Exception: {0}", ex.InnerException.Message) : "";

                // Create or append the exception details to the log file
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Exception Occurred: {0}", DateTime.Now.EgyptTimeNow().ToString());
                    writer.WriteLine(exceptionMessage);
                    writer.WriteLine(stackTrace);
                    writer.WriteLine(innerException);
                    writer.WriteLine("----------------------------------------");
                    writer.Flush();
                }
            }
            catch 
            {
            }
        }

        public static Result<T> PublishExceptionReturnResponse<T>(Exception ex)
        {
            PublishException(ex);
            return ResultFactory<T>.CreateExceptionResponse(ex);
        }

        public static Result<object> PublishExceptionReturnResponse(Exception ex)
        {
            PublishException(ex);
            return ResultFactory<object>.CreateExceptionResponse(ex);
        }
    }
}
