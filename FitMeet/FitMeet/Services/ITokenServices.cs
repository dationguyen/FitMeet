namespace FitMeet.Services
{
    public interface ITokenServices
    {
        bool HasValidToken();

        void SetToken( string token );

        string GetToken();
    }
}
