using FitMeet.Models;
using FitMeet.Services;
using Prism.Navigation;
using System.Collections.Generic;

namespace FitMeet.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        private List<Member> resultListItemsSource;

        public List<Member> ResultListItemsSource
        {
            get { return resultListItemsSource; }
            set { SetProperty(ref resultListItemsSource, value); }
        }

        public SearchPageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestServices) : base(navigationService, fitMeetRestServices)
        {
            Title = "Search";
        }

        public async override void OnNavigatingTo(NavigationParameters parameters)
        {
            var restResponseMessage = await _fitMeetRestService.GetMembersAsync();
            ResultListItemsSource = restResponseMessage.Output.Response;
        }
    }
}
