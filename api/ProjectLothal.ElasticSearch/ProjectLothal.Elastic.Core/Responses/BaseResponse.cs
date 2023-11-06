

namespace ProjectLothal.Elastic.Core.Responses
{
    public record BaseResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

        public static BaseResponse<T> SuccessResponse(T data,bool isSuccess)
        {
            return new BaseResponse<T> { Data = data, IsSuccess = isSuccess };
        }

        public static BaseResponse<T> ErrorResponse(bool isSuccess, string errorMessage)
        {
            return new BaseResponse<T> {IsSuccess = isSuccess , ErrorMessage= errorMessage};
        }
    }
}
