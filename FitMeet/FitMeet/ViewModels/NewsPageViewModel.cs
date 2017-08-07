using FitMeet.Models;
using FitMeet.Services;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace FitMeet.ViewModels
{
    public class NewsPageViewModel : ViewModelBase
    {
        //fields
        private string logoImageSource;

        private ObservableCollection<News> _newsListItemsSource;
        private News _newsListSelectedItem;


        //Properties
        public string LogoImageSource
        {
            get { return logoImageSource; }
            set { SetProperty(ref logoImageSource, value); }
        }

        public ObservableCollection<News> NewsListItemsSource
        {
            get => _newsListItemsSource;
            set => SetProperty(ref _newsListItemsSource, value);
        }

        public News NewsListSelectedItem
        {
            get { return null; }
            set
            {
                SetProperty(ref _newsListSelectedItem, value);
                if (value != null)
                {
                    var item = NewsListItemsSource.Single(u => u.Id == value.Id);
                    item.View = "1";                
                    //NavigateCommand.Execute("NewsDetailPage?id=" + ((News)value).Id);
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
            var list = result.Output.Response;
            foreach (var item in list)
            {
                item.View = "0";
            };
            NewsListItemsSource =  new ObservableCollection<News>(list);
        }
    }
}
