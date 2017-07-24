using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class FriendsPageViewModel : ViewModelBase
    {
        public FriendsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Friends";
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}
