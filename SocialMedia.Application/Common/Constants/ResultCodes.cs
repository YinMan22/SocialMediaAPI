namespace SocialMedia.Application.Common.Constants
{
    public static class ResultCodes
    {
        public const string Success = "SM00000";
        public const string ValueNull = "SM00001";
        public const string ValueEmptyOrWhitespaces = "SM00002";
        public const string ValueLengthInvalid = "SM00003";
        public const string ValueNotMeetRequirement = "SM00004";
        public const string InvalidCredentials = "SM00005";
        public const string DuplicateUsername = "SM00006";
        public const string ListEmpty = "SM00007";
        public const string IdValueInvalid = "SM00008";
        public const string TokenIssue = "SM00009";

        public const string UncaughtError = "SM99999";
    }
}
