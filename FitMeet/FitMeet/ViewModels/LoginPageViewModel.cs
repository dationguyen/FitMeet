using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class LoginPageViewModel:ViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService,IFitMeetRestService fitMeetRestServices) : base(navigationService,fitMeetRestServices)
        {
        }

        public DelegateCommand<object> FacebookLoginCommand
        {
            get
            {
                return new DelegateCommand<object>((o) =>
                {

                });
            }
        }
    }
}
