using FitMeet.Models;
using FitMeet.Services;
using Prism.Navigation;
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
                if (_newsListSelectedItem != null)
                {
                    var index = NewsListItemsSource.IndexOf(_newsListSelectedItem);
                    if (index > -1)
                    {

                        _newsListSelectedItem.View = "1";
                        NewsListItemsSource.RemoveAt(index);
                        NewsListItemsSource.Insert(index, _newsListSelectedItem);
                    }
                    RaisePropertyChanged("NewsListItemsSource");
                    Navigate("NewsDetailPage?id=" + ((News)value).Id);
                }

            }
        }


        public NewsPageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestService) : base(navigationService, fitMeetRestService)
        {
            Title = "News";
        }
     
        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var result = await _fitMeetRestService.GetNewsAsync();
            LogoImageSource = result?.Output?.Banner;
            var list = result?.Output?.Response;
            NewsListItemsSource = new ObservableCollection<News>(list);
        }
    }
}
