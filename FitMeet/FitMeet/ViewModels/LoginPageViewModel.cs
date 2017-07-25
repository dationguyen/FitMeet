using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private IFitMeetRestService _fitMeetRestService;

        public LoginPageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestServices) : base(navigationService)
        {
            _fitMeetRestService = fitMeetRestServices;
        }
    }
}
