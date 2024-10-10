namespace TaggerApi.Services.ErrorServices;
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}