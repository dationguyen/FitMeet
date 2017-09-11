using FitMeet.Droid.DependencyServices;
using FitMeet.Droid.Renderers;
using FitMeet.Services.DependencyServices;
using System.Threading.Tasks;
using Xamarin.Facebook.Login;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FacebookLoginService))]
namespace FitMeet.Droid.DependencyServices
{
    public class FacebookLoginService:IFacebookLoginService
    {
        public FacebookLoginService()
        {
            LoginManager.Instance.SetLoginBehavior(LoginBehavior.KatanaOnly);
        }

        public Task<string> LoginAsync(string[] readPermissions)
        {
            var tcs = new TaskCompletionSource<string>();

            LoginManager.Instance.RegisterCallback(MainActivity.callbackManager,new FacebookCallback<LoginResult>()
            {
                HandleSuccess = token =>
                {
                    tcs.SetResult(token);
                },
                HandleCancel = () => tcs.SetResult(""),
                HandleError = (error) => tcs.SetException(error.InnerException)

            });
            var activity = (MainActivity)Forms.Context;


            LoginManager.Instance.LogInWithReadPermissions(activity,readPermissions);

            return tcs.Task;
        }
    }
}