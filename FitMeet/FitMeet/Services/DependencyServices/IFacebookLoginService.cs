using System.Threading.Tasks;

namespace FitMeet.Services.DependencyServices
{
    public interface IFacebookLoginService
    {
        /// <summary>
        /// Login to facebook with the permission
        /// </summary>
        /// <param name="readPermissions"></param>
        /// <returns>Facebook Login Token</returns>
        Task<string> LoginAsync(string[] readPermissions);

        /// <summary>
        /// Simple Login to get UserID
        /// </summary>
        /// <returns></returns>
        Task<string> LoginAsync();
    }
}