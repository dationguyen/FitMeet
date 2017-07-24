using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        public SearchPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Search";
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}
