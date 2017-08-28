using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class ThirdSignUpPageViewModel:ViewModelBase
    {
        public ThirdSignUpPageViewModel(INavigationService navigationService,IFitMeetRestService fitMeetRestService) : base(navigationService,fitMeetRestService)
        {
        }
    }
}
