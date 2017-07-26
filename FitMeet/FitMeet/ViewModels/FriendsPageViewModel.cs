using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class FriendsPageViewModel : ViewModelBase
    {
        public FriendsPageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestServices) : base(navigationService, fitMeetRestServices)
        {
            Title = "Friends";
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}
