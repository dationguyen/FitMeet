using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class NewsPageViewModel : ViewModelBase
    {
        private IFitMeetRestService _fitMeetRestService;
        public NewsPageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestService) : base(navigationService)
        {
            Title = "News";
            _fitMeetRestService = fitMeetRestService;
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            var result = await _fitMeetRestService.GetNewsAsync();
        }
    }
}
