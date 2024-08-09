namespace SocialMedia.Application.Common.Interfaces
{
    public interface IResponse
    {
        public bool IsSuccess { get; set; }

        public string ResultCode { get; set; }

        public string Message { get; set; }
    }
}
