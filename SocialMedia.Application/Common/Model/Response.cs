using SocialMedia.Application.Common.Interfaces;

namespace SocialMedia.Application.Common.Model
{
    public class Response : IResponse
    {
        public bool IsSuccess { get; set; }

        public string ResultCode { get; set; }

        public string Message { get; set; }

        public Response() { }
        public Response(bool isSuccess, string resultCode, string message)
        {
            IsSuccess = isSuccess;
            ResultCode = resultCode;
            Message = message;
        }
    }
}
