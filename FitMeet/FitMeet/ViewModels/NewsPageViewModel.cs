using FitMeet.Models;
using FitMeet.Services;
using Prism.Navigation;
using System.Collections.Generic;

namespace FitMeet.ViewModels
{
    public class NewsPageViewModel : ViewModelBase
    {
        //fields
        private string logoImageSource;

        private List<NewsInfomation> newsListItemsSource;
        private NewsInfomation _newsListSelectedItem;


        //Properties
        public string LogoImageSource
        {
            get { return logoImageSource; }
            set { SetProperty(ref logoImageSource, value); }
        }

        public List<NewsInfomation> NewsListItemsSource
        {
            get => newsListItemsSource;
            set => SetProperty(ref newsListItemsSource, value);
        }

        public NewsInfomation NewsListSelectedItem
        {
            get { return null; }
            set
            {
                SetProperty(ref _newsListSelectedItem, value);
                if (value != null)
                {
                    Navigate("NewsDetailPage?id=" + ((NewsInfomation)value).Id);
                }

            }
        }


        public NewsPageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestService) : base(navigationService, fitMeetRestService)
        {
            Title = "News";
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            var result = await _fitMeetRestService.GetNewsAsync();
            LogoImageSource = result.Output.Banner;
            NewsListItemsSource = result.Output.Response;
        }
    }
}
