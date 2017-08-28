using FitMeet.Controls;
using FitMeet.EventAggregator;
using FitMeet.Models;
using FitMeet.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitMeet.ViewModels
{
    public class SearchPageViewModel:ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private IGoogleLocationService _googleLocationService;

        private SpeedObservableCollection<Member> _resultListItemsSource;
        private int _pageCount = 1;
        private int _curentPage = 0;
        private bool _isSearching = false;
        private bool _isRefreshing = false;
        private bool _isLoading = false;
        private bool _hasFilter = false;
        private string _searchKeyWord;
        private UpdateFilterEventArgs _updateFilterEventArgs;
        private Member _searchListSelectedItem;
        private List<string> _autoCompleteCollection;
        private int _autoCompleteHeight;
        private string _autoCompleteResult;

        public Member SearchListSelectedItem
        {
            get { return null; }
            set
            {
                SetProperty(ref _searchListSelectedItem,value);
                if(value != null)
                {
                    Navigate("MemberDetailPage?id=" + ((Member)value).MemberId);
                }
            }
        }
        public string FilterImageSource
        {
            get
            {
                return _hasFilter ? "filter_enabled.png" : "filter_disabled.png";
            }
        }
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                SetProperty(ref _isRefreshing,value);
            }
        }
        private bool HasNext
        {
            get { return _curentPage < _pageCount; }
        }
        public DelegateCommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    ReloadItems();
                });
            }
        }
        private void ReloadItems()
        {
            IsRefreshing = true;
            ResultListItemsSource.Clear();
            _curentPage = 0;
            _pageCount = 1;
            LoadItems();
        }
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading,value); }
        }
        public bool IsSearching
        {
            get => _isSearching;
            set
            {
                SetProperty(ref _isSearching,value);
            }
        }
        public string SearchKeyWord
        {
            get { return _searchKeyWord; }
            set
            {
                if(_searchKeyWord != value)
                {
                    SetProperty(ref _searchKeyWord,value);

                    if(!String.IsNullOrEmpty(_searchKeyWord))
                    {
                        IsSearching = true;
                        GetAutoComplete(_searchKeyWord);

                    }
                    else
                    {
                        IsSearching = false;
                        AutoCompleteCollection.Clear();
                    }

                }
            }
        }
        public List<string> AutoCompleteCollection
        {
            get
            {
                if(_autoCompleteCollection == null)
                    _autoCompleteCollection = new List<string>();
                return _autoCompleteCollection;
            }
            set { SetProperty(ref _autoCompleteCollection,value); }
        }
        public string AutoCompleteResult
        {
            get { return null; }
            set
            {
                if(value != null)
                {
                    SetProperty(ref _autoCompleteResult,value);
                    _searchKeyWord = value;
                    RaisePropertyChanged("SearchKeyWord");
                    IsSearching = false;
                }
            }
        }
        public int AutoCompleteHeight
        {
            get { return _autoCompleteHeight; }
            set { SetProperty(ref _autoCompleteHeight,value); }
        }


        public DelegateCommand FilterCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var param = new NavigationParameters
                    {
                        { "agr", _updateFilterEventArgs }
                    };
                    await _navigationService.NavigateAsync("AdvanceFilterPopup",param);
                });
            }
        }
        public DelegateCommand<object> ItemAppearingCommand { get; set; }
        public SpeedObservableCollection<Member> ResultListItemsSource
        {
            get
            {
                if(_resultListItemsSource == null)
                    _resultListItemsSource = new SpeedObservableCollection<Member>();
                return _resultListItemsSource;
            }
            set { SetProperty(ref _resultListItemsSource,value); }
        }






        public SearchPageViewModel(INavigationService navigationService,IGoogleLocationService googleLocationService,IFitMeetRestService fitMeetRestServices,IEventAggregator eventAggregator) : base(navigationService,fitMeetRestServices)
        {
            Title = "Search";
            ItemAppearingCommand = new DelegateCommand<object>(OnItemAppearing);

            _googleLocationService = googleLocationService;

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<UpdateFilterEvent>().Subscribe(UpdateFilter);
        }

        private void UpdateFilter(UpdateFilterEventArgs obj)
        {
            if(obj != null)
            {
                _hasFilter = true;
                _updateFilterEventArgs = obj;
            }
            else
            {
                _hasFilter = false;
                _updateFilterEventArgs = null;
            }

            RaisePropertyChanged("FilterImageSource");
            ReloadItems();
        }

        private void OnItemAppearing(object obj)
        {
            var count = ResultListItemsSource.Count;
            if(IsLoading || count == 0 || !HasNext)
                return;

            //hit bottom!
            if(obj == ResultListItemsSource.Last())
            {
                LoadItems();
            }
        }

        private async void LoadItems()
        {
            IsLoading = true;
            if(ResultListItemsSource == null)
            {
                ResultListItemsSource = new SpeedObservableCollection<Member>();
            }

            var restResponseMessage = await GetDataFromRest(_curentPage + 1);
            if(restResponseMessage == null)
            {
                _curentPage++;
                IsLoading = false;
                IsRefreshing = false;
                return;
            }
            var output = restResponseMessage?.Output;
            _curentPage = output.Currentpage;
            _pageCount = output.Pagecount;

            var result = output?.Response;
            if(result != null)
            {
                ResultListItemsSource.AddRange(result);
                //result.ForEach(ResultListItemsSource.Add);
            }
            IsRefreshing = false;
            IsLoading = false;
        }

        private async Task<ResponseMessage<List<Member>>> GetDataFromRest(int page)
        {
            ResponseMessage<List<Member>> restResponseMessage;
            if(!_hasFilter)
                restResponseMessage = await _fitMeetRestService.GetMembersAsync(page);
            else
            {
                var distance = _updateFilterEventArgs.Distance;
                var gender = _updateFilterEventArgs.IsMale ? "male" : "female";
                var activities = _updateFilterEventArgs.Activities;
                restResponseMessage = await _fitMeetRestService.SearchMembersAsync(page,distance,gender,activities);
            }
            return restResponseMessage;
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if(ResultListItemsSource.Count == 0)
                LoadItems();
        }

        private async void GetAutoComplete(string searchKeyWord)
        {
            var result = await _googleLocationService.AutoComplete(searchKeyWord);
            if(result != null && result.Count > 0)
            {
                AutoCompleteHeight = 36 * result.Count;
                AutoCompleteCollection = result;
            }
        }
    }
}
