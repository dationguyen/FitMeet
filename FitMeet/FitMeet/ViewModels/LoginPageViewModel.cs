using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class LoginPageViewModel:ViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService,IFitMeetRestService fitMeetRestServices) : base(navigationService,fitMeetRestServices)
        {
            App.PostSuccessFacebookAction = token =>
            {
                //you can use this token to authenticate to the server here
                //call your FacebookLoginService.LoginToServer(token)
                //I'll just navigate to a screen that displays the token:
               

            };
        }
    }
}
