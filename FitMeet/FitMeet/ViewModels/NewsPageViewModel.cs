using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class NewsPageViewModel : ViewModelBase
    {
        public NewsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "News";
        }
    }
}
