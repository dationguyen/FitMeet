using FitMeet.Models;
using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class NewsDetailPageViewModel : ViewModelBase
    {
        private NewsDetail dataSource;

        public NewsDetailPageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestServices) : base(navigationService, fitMeetRestServices)
        {

        }

        public NewsDetail DataSource
        {
            get => dataSource;
            set => SetProperty(ref dataSource, value);
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            var id = parameters["id"].ToString();
            var restResponseMessage = await _fitMeetRestService.GetNewsDetailAsync(id);
            DataSource = restResponseMessage?.Output?.Response;
        }
    }
}
