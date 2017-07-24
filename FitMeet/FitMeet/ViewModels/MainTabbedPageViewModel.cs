using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class MainTabbedPageViewModel : ViewModelBase
    {
        public MainTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }



        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

        }
    }
}
