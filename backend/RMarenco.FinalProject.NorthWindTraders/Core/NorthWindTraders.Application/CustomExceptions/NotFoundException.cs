namespace NorthWindTraders.Application.CustomExceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
    }
}
