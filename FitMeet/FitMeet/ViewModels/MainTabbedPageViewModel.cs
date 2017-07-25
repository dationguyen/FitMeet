using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class MainTabbedPageViewModel : ViewModelBase
    {
        public MainTabbedPageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestServices) : base(navigationService, fitMeetRestServices)
        {
        }



        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

        }
    }
}
