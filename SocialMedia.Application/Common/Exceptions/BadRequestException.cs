namespace SocialMedia.Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public string ResultCode { get; private set; }

        public BadRequestException(string errorCode, string message) : base(message)
        {
            ResultCode = errorCode;
        }
    }
}
