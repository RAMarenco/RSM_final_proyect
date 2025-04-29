namespace NorthWindTraders.Application.CustomExceptions
{
    public class BadRequestException(string message) : Exception(message)
    {
    }
}
