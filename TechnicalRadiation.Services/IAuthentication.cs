namespace TechnicalRadiation.Services
{
    public interface IAuthorization
    {
        bool Authorization(string secret);
    }
}