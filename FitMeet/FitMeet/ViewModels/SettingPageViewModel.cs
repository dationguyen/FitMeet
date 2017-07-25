using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class SettingPageViewModel : ViewModelBase
    {
        public SettingPageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestServices) : base(navigationService, fitMeetRestServices)
        {
        }
    }
}
