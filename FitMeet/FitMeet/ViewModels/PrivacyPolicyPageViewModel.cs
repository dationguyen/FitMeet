using FitMeet.Models;
using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class PrivacyPolicyPageViewModel : ViewModelBase
    {
        private const string pageName = "privacy-policy";

        private WebPageInfo dataSource;


        public WebPageInfo DataSource
        {
            get { return dataSource; }
            set { SetProperty(ref dataSource, value); }
        }


        public PrivacyPolicyPageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestService) : base(navigationService, fitMeetRestService)
        {
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            var responseMessage = await _fitMeetRestService.GetPageDetailAsync(pageName);
            DataSource = responseMessage?.Output?.Response;
        }
    }
}
