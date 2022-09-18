using System.IO;
using static App.Global.Enums;

namespace App.Global.DTOs
{
    public class OpreationResultMessage<T>
    {
        public int StatusCode { get; set; }
        public string StatusDescreption { get; set; }
        public string Message { get; set; }
        public int TotalCount { get; set; }
        public int AleartType { get; set; }
        public T Data { get; set; }
        public bool IsSuccess
        {
            get
            {
                return StatusCode == 1;
            }
        }

        public bool IsFail
        {
            get
            {
                return StatusCode == 0;
            }
        }
    }

    public class OpreationResultMessageFactory<T>
    {
        public static int SUCCESS_CODE = 1;
        public static int FAILED_CODE = 0;
        public static OpreationResultMessage<T> HandleResponse(T data = default, OpreationResultStatusCode resultStatusCode = OpreationResultStatusCode.None, string message = null)
        {
            OpreationResultMessage<T> opreationResultMessage = new OpreationResultMessage<T>();
            if (data == null)
            {
                opreationResultMessage.TotalCount = 0;
                opreationResultMessage.StatusCode = FAILED_CODE;
                if (resultStatusCode == OpreationResultStatusCode.None)
                {
                    opreationResultMessage.StatusDescreption = OpreationResultStatusCode.NotFound.ToString();
                    opreationResultMessage.Message = "No data found";
                }
                else if(resultStatusCode == OpreationResultStatusCode.Exception)
                {
                    opreationResultMessage.StatusDescreption = OpreationResultStatusCode.Exception.ToString();
                    opreationResultMessage.Message = "Error happen while proccessing your request";
                }
                else
                {
                    opreationResultMessage.StatusDescreption = OpreationResultStatusCode.Failed.ToString();
                    opreationResultMessage.Message = "Error happen while proccessing your request";
                }
                opreationResultMessage.Message = message ?? opreationResultMessage.Message;
                opreationResultMessage.Data = data;
            }
            else
            {
                opreationResultMessage.TotalCount = 1;
                opreationResultMessage.StatusCode = SUCCESS_CODE;
                opreationResultMessage.StatusDescreption = resultStatusCode == OpreationResultStatusCode.None ? OpreationResultStatusCode.Success.ToString() : resultStatusCode.ToString();
                opreationResultMessage.Message = message ?? "Opreation Done Successfully";
                opreationResultMessage.Data = data;
            }
            return opreationResultMessage;
        }

        public static OpreationResultMessage<T> CreateSuccessResponse(T date = default, OpreationResultStatusCode resultStatusCode = OpreationResultStatusCode.Success, string message = "Opreation done successfully")
        {
            return new OpreationResultMessage<T>
            {
                StatusCode = SUCCESS_CODE,
                StatusDescreption = resultStatusCode.ToString(),
                Message = message,
                Data = date
            };
        }

        public static OpreationResultMessage<T> CreateErrorResponse(T date = default, OpreationResultStatusCode resultStatusCode = OpreationResultStatusCode.Failed, string message = "Error happen while processing your request")
        {
            return new OpreationResultMessage<T>
            {
                StatusCode = FAILED_CODE,
                StatusDescreption = resultStatusCode.ToString(),
                Message = message,
                Data = date
            };
        }

        public static OpreationResultMessage<T> CreateNotFoundResponse(string message = null)
        {
            return new OpreationResultMessage<T>
            {
                StatusCode = FAILED_CODE,
                TotalCount = 0,
                StatusDescreption = OpreationResultStatusCode.NotFound.ToString(),
                Message = message == null ?  "No data found" : message,
                Data = default
            };
        }

        public static OpreationResultMessage<T> CreateExceptionResponse(System.Exception ex = null)
        {
            var message = "Exception: " + ex.Message;
            if(ex.InnerException != null)
            {
                message = message + ", InnerException: " + ex.InnerException.Message;
            }
            return new OpreationResultMessage<T>
            {
                StatusCode = FAILED_CODE,
                TotalCount = 0,
                StatusDescreption = OpreationResultStatusCode.Exception.ToString(),
                Message = message,
                Data = default
            };
        }

        public static OpreationResultMessage<T> CreateErrorResponseMessage(string message, OpreationResultStatusCode resultStatusCode = OpreationResultStatusCode.Failed)
        {
            return CreateErrorResponse(resultStatusCode: resultStatusCode, message: message);
        }

        public static OpreationResultMessage<T> CreateModelNotValidResponse(string message, OpreationResultStatusCode resultStatusCode = OpreationResultStatusCode.ModelNotValid)
        {
            return CreateErrorResponse(resultStatusCode: resultStatusCode, message: message);
        }

    }
}
