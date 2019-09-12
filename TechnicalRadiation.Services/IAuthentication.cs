namespace TechnicalRadiation.Services
{
    public interface IAuthentication
    {
        bool Authenticate(string secret);
    }
}