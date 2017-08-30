namespace FitMeet.Services
{
    public interface ITokenService
    {
        void SetToken(string token);

        string GetToken();
    }
}
