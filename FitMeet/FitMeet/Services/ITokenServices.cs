using System.Threading.Tasks;

namespace FitMeet.Services
{
    public interface ITokenServices
    {
        Task<bool> HasValidToken();

        void SetToken(string token);

        string GetToken();
    }
}
