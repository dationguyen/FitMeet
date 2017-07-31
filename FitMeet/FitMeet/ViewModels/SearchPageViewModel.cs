using FitMeet.Controls;
using FitMeet.Models;
using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Linq;

namespace FitMeet.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        private SpeedObservableCollection<Member> resultListItemsSource;
        private bool _isLoading;
        private int _pageCount = 1;
        private int _curentPage = 0;
        private bool _isRefreshing = false;
        private bool hasFilter = false;

        public string FilterImageSource
        {
            get
            {
                return hasFilter ? "filter_enabled.png" : "filter_disabled.png";
            }
        }


        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }
        private bool HasNext
        {
            get { return _curentPage < _pageCount; }
        }


        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public DelegateCommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    ResultListItemsSource.Clear();
                    _curentPage = 0;
                    _pageCount = 1;
                    LoadItems();
                });
            }
        }

        public DelegateCommand FilterCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    hasFilter = !hasFilter;
                    RaisePropertyChanged("FilterImageSource");
                    await _navigationService.NavigateAsync("AdvanceFilterPopup");
                });
            }
        }

        public DelegateCommand<object> ItemAppearingCommand { get; set; }

        public SpeedObservableCollection<Member> ResultListItemsSource
        {
            get { return resultListItemsSource; }
            set { SetProperty(ref resultListItemsSource, value); }
        }

        public SearchPageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestServices) : base(navigationService, fitMeetRestServices)
        {
            Title = "Search";
            ItemAppearingCommand = new DelegateCommand<object>(OnItemAppearing);
        }

        private void OnItemAppearing(object obj)
        {
            if (IsLoading || ResultListItemsSource.Count == 0)
                return;

            //hit bottom!
            if (obj == ResultListItemsSource.Last())
            {
                if (HasNext)
                    LoadItems();
            }
        }

        private async void LoadItems()
        {
            IsRefreshing = true;
            if (ResultListItemsSource == null)
            {
                ResultListItemsSource = new SpeedObservableCollection<Member>();
            }

            var restResponseMessage = await _fitMeetRestService.GetMembersAsync(_curentPage + 1);
            if (restResponseMessage == null)
            {
                _curentPage++;
                return;
            }
            var output = restResponseMessage?.Output;
            _curentPage = output.Currentpage;
            _pageCount = output.Pagecount;

            var result = output?.Response;
            if (result != null)
            {
                ResultListItemsSource.AddRange(result);
            }
            IsRefreshing = false;
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            LoadItems();
        }
    }
}
