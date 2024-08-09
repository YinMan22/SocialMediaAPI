namespace SocialMedia.Application.Common.Exceptions
{
    public class FluentValidationException : Exception
    {
        public string ResultCode { get; private set; }

        public FluentValidationException(string errorCode, string message) : base(message)
        {
            ResultCode = errorCode;
        }
    }
}
