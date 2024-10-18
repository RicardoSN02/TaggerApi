namespace TaggerApi.Services.ErrorServices;
public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}