using System.IO;
using static App.Global.Enums;

namespace App.Global.DTOs
{
    public class Result<T>
    {
        public Result()
        {

        }
        public Result(int StatusCode, string StatusDescreption, string Message, int TotalCount, int AleartType, T Data)
        {
            this.StatusCode = StatusCode;
            this.StatusDescreption = StatusDescreption;
            this.Message = App.Global.Translation.Translator.STranlate(Message);
            this.TotalCount = TotalCount;
            this.Data = Data;
            this.AleartType = AleartType;
        }
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

    public class ResultFactory<T>
    {
        public static int SUCCESS_CODE = 1;
        public static int FAILED_CODE = 0;
        public static Result<T> HandleResponse(T data = default, ResultStatusCode resultStatusCode = ResultStatusCode.None, string message = null)
        {
            Result<T> Result = new Result<T>();
            if (data == null)
            {
                Result.TotalCount = 0;
                Result.StatusCode = FAILED_CODE;
                if (resultStatusCode == ResultStatusCode.None)
                {
                    Result.StatusDescreption = ResultStatusCode.NotFound.ToString();
                    Result.Message = "No data found";
                }
                else if(resultStatusCode == ResultStatusCode.Exception)
                {
                    Result.StatusDescreption = ResultStatusCode.Exception.ToString();
                    Result.Message = "Error happen while proccessing your request";
                }
                else
                {
                    Result.StatusDescreption = ResultStatusCode.Failed.ToString();
                    Result.Message = "Error happen while proccessing your request";
                }
                Result.Message = message ?? Result.Message;
                Result.Data = data;
            }
            else
            {
                Result.TotalCount = 1;
                Result.StatusCode = SUCCESS_CODE;
                Result.StatusDescreption = resultStatusCode == ResultStatusCode.None ? ResultStatusCode.Success.ToString() : resultStatusCode.ToString();
                Result.Message = message ?? "Opreation Done Successfully";
                Result.Data = data;
            }
            Result.Message = App.Global.Translation.Translator.STranlate(Result.Message);
            return Result;
        }

        public static Result<T> CreateSuccessResponse(T date = default, ResultStatusCode resultStatusCode = ResultStatusCode.Success, string message = "Opreation done successfully", ResultAleartType resultAleartType = ResultAleartType.None)
        {
            return new Result<T>(SUCCESS_CODE, resultStatusCode.ToString(), message, 0, ((int)resultAleartType), date);
        }

        public static Result<T> CreateSuccessResponseSD(T date = default, ResultStatusCode resultStatusCode = ResultStatusCode.Success, string message = "Opreation done successfully")
        {
            return CreateSuccessResponse(date, resultStatusCode, message, ResultAleartType.SuccessDialog);
        }

        public static Result<T> CreateErrorResponse(T date = default, ResultStatusCode resultStatusCode = ResultStatusCode.Failed, string message = "Error happen while processing your request", ResultAleartType resultAleartType = ResultAleartType.FailedToast)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = "Error happen while processing your request";
            }
            return new Result<T>(FAILED_CODE, resultStatusCode.ToString(), message, 0, ((int)resultAleartType), date);
        }

        public static Result<T> CreateNotFoundResponse(string message = null, ResultAleartType resultAleartType = ResultAleartType.FailedToast)
        {
            return new Result<T>(FAILED_CODE, ResultStatusCode.NotFound.ToString(), message == null ? "No data found" : message, 0, ((int)resultAleartType), default);           
        }

        public static Result<T> CreateExceptionResponse(System.Exception ex = null, ResultAleartType resultAleartType = ResultAleartType.FailedToast)
        {
            App.Global.Logging.LogHandler.PublishException(ex);
            var message = "Exception: " + ex.Message;
            if(ex.InnerException != null)
            {
                message = message + ", InnerException: " + ex.InnerException.Message;
            }
            return new Result<T>(FAILED_CODE, ResultStatusCode.Exception.ToString(), message, 0, ((int)resultAleartType), default);
        }

        public static Result<T> CreateErrorResponseMessage(string message, ResultStatusCode resultStatusCode = ResultStatusCode.Failed, ResultAleartType resultAleartType = ResultAleartType.FailedToast)
        {
            return CreateErrorResponse(resultStatusCode: resultStatusCode, message: message, resultAleartType: resultAleartType);
        }

        public static Result<T> CreateErrorResponseMessageFD(string message, ResultStatusCode resultStatusCode = ResultStatusCode.Failed, ResultAleartType resultAleartType = ResultAleartType.FailedDialog)
        {
            return CreateErrorResponse(resultStatusCode: resultStatusCode, message: message, resultAleartType: resultAleartType);
        }

        public static Result<T> CreateRequireRegisterResponse(T data = default)
        {
            return CreateErrorResponse(date : data, resultStatusCode: ResultStatusCode.NotAllowed, message: "You must register first", resultAleartType: ResultAleartType.RegistrationRequired);
        }

        public static Result<T> CreateModelNotValidResponse(string message, ResultStatusCode resultStatusCode = ResultStatusCode.ModelNotValid, ResultAleartType resultAleartType = ResultAleartType.SuccessToast)
        {
            return CreateErrorResponse(resultStatusCode: resultStatusCode, message: message, resultAleartType: resultAleartType);
        }

        public static Result<T> ReturnClientError()
        {
            return CreateErrorResponse(resultStatusCode: ResultStatusCode.EmptyData, message: "Error happen while getting client", resultAleartType: ResultAleartType.FailedToast);
        }
        public static Result<T> ReturnEmployeeError()
        {
            return CreateErrorResponse(resultStatusCode: ResultStatusCode.EmptyData, message: "This employee not exist, or not approved yet or account is suspended", resultAleartType: ResultAleartType.FailedToast);
        }

        public static Result<T> FileValidationError(string fileName = null)
        {
            return CreateErrorResponse(resultStatusCode: ResultStatusCode.InvalidData, message: $"Invalid file: {fileName}", resultAleartType: ResultAleartType.FailedToast);
        }

        public static Result<T> CreateAffectedRowsResult(int affectedRow, string errorMessage = null, T data = default)
        {
            if(affectedRow >= 0)
            {
                return CreateSuccessResponse(data);
            }
            else
            {
                return CreateErrorResponseMessageFD(errorMessage);
            }
        }

    }

    public static class ResultExtention
    {
        public static Result<NewType> Convert<OldType, NewType>(this Result<OldType> result, NewType data) where OldType : class
        {
            return new Result<NewType>
            {
                AleartType = result.AleartType,
                Data = data,
                Message = result.Message,
                StatusCode = result.StatusCode,
                StatusDescreption = result.StatusDescreption,
                TotalCount = result.TotalCount
            };
        }
    }
}
