namespace NorthWindTraders.Application.CustomExceptions
{
    public class BadRequestException(string message, List<string> errors) : Exception(message)
    {
        public List<string> Errors { get; } = errors ?? new List<string>();
    }
}
