

using System.Net;

namespace ProjectLothal.Elastic.Core.Responses
{
    public record BaseResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public HttpStatusCode? Status { get; set; }

        public static BaseResponse<T> SuccessResponse(T data,bool isSuccess, HttpStatusCode? statusCode)
        {
            return new BaseResponse<T> { Data = data, IsSuccess = isSuccess, Status = statusCode };
        }

        public static BaseResponse<T> ErrorResponse(bool isSuccess, string errorMessage, HttpStatusCode? statusCode)
        {
            return new BaseResponse<T> {IsSuccess = isSuccess , ErrorMessage= errorMessage, Status = statusCode};
        }
    }
}
