using static App.Global.Eumns;

namespace App.Global.DTOs
{
    public class HttpResponseDto<T>
    {
        public int StatusCode { get; set; }
        public string StatusDescreption { get; set; }
        public string Message { get; set; }
        public int TotalCount { get; set; }
        public T Data { get; set; }
    }

    public class HttpResponseDtoFactory<T>
    {
        public static int SUCCESS_CODE = 1;
        public static int FAILED_CODE = 0;
        public static HttpResponseDto<T> HandleResponse(T data = default, ResponseStatusCode responseStatusCode = ResponseStatusCode.None, string message = null)
        {
            HttpResponseDto<T> httpResponseDto = new HttpResponseDto<T>();

            if (data == null)
            {
                httpResponseDto.TotalCount = 0;
                httpResponseDto.StatusCode = FAILED_CODE;
                if (responseStatusCode == ResponseStatusCode.None)
                {
                    httpResponseDto.StatusDescreption = ResponseStatusCode.NotFound.ToString();
                    httpResponseDto.Message = "No data found";
                }
                else if(responseStatusCode == ResponseStatusCode.Exception)
                {
                    httpResponseDto.StatusDescreption = ResponseStatusCode.Exception.ToString();
                    httpResponseDto.Message = "Error happen while proccessing your request";
                }
                else
                {
                    httpResponseDto.StatusDescreption = ResponseStatusCode.Failed.ToString();
                    httpResponseDto.Message = "Error happen while proccessing your request";
                }
                httpResponseDto.Message = message ?? httpResponseDto.Message;
                httpResponseDto.Data = data;
            }
            else
            {
                httpResponseDto.TotalCount = 1;
                httpResponseDto.StatusCode = SUCCESS_CODE;
                httpResponseDto.StatusDescreption = responseStatusCode == ResponseStatusCode.None ? ResponseStatusCode.Success.ToString() : responseStatusCode.ToString();
                httpResponseDto.Message = message ?? "Opreation Done Successfully";
                httpResponseDto.Data = data;
            }
            return httpResponseDto;
        }

        public static HttpResponseDto<T> CreateSuccessResponse(T date = default, ResponseStatusCode responseStatusCode = ResponseStatusCode.Success, string message = "Opreation done successfully")
        {
            return new HttpResponseDto<T>
            {
                StatusCode = 1,
                StatusDescreption = responseStatusCode.ToString(),
                Message = message,
                Data = date
            };
        }

        public static HttpResponseDto<T> CreateErrorResponse(T date = default, ResponseStatusCode responseStatusCode = ResponseStatusCode.Failed, string message = "Error happen while processing your request")
        {
            return new HttpResponseDto<T>
            {
                StatusCode = 0,
                StatusDescreption = responseStatusCode.ToString(),
                Message = message,
                Data = date
            };
        }

        public static HttpResponseDto<T> CreateNotFoundResponse()
        {
            return new HttpResponseDto<T>
            {
                StatusCode = 0,
                TotalCount = 0,
                StatusDescreption = ResponseStatusCode.NotFound.ToString(),
                Message = "No data found",
                Data = default
            };
        }

        public static HttpResponseDto<T> CreateExceptionResponse(System.Exception ex = null)
        {
            return new HttpResponseDto<T>
            {
                StatusCode = 0,
                TotalCount = 0,
                StatusDescreption = ResponseStatusCode.Exception.ToString(),
                Message = "Exception happen while processing your request " + ex.Message,
                Data = default
            };
        }

        public static HttpResponseDto<T> CreateErrorResponseMessage(string message, ResponseStatusCode responseStatusCode = ResponseStatusCode.Failed)
        {
            return CreateErrorResponse(responseStatusCode: responseStatusCode, message: message);
        }

        public static HttpResponseDto<T> CreateModelNotValidResponse(string message, ResponseStatusCode responseStatusCode = ResponseStatusCode.ModelNotValid)
        {
            return CreateErrorResponse(responseStatusCode: responseStatusCode, message: message);
        }

    }
}
