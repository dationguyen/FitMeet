using System.Threading.Tasks;

namespace FitMeet.Services
{
    public interface ITokenService
    {
        Task<bool> HasValidToken();

        void SetToken(string token);

        string GetToken();
    }
}
