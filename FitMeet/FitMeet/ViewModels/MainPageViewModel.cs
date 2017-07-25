using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestServices) : base(navigationService, fitMeetRestServices)
        {
        }
     
    }
}
