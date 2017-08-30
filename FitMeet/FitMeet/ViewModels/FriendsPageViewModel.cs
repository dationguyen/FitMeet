using FitMeet.Controls;
using FitMeet.EventAggregator;
using FitMeet.Models;
using FitMeet.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FitMeet.ViewModels
{
    public class FriendsPageViewModel:ViewModelBase
    {
        private IEventAggregator _eventAggregator;

        private int _pageCount = 1;
        private int _curentPage = 0;
        private bool _isRefreshing = false;
        private bool _isNoFriend = true;
        private Member _friendListSelectedItem;
        private List<Member> _friendList;
        private string _messageCount;
        private string _searchKeyword;
        private ObservableCollection<GroupsCollection<string,Member>> _friendsGrouped;
        private string _messageLogo = "message_logo_0.png";
        private bool _isChecking = false;

        public ObservableCollection<GroupsCollection<string,Member>> FriendsGrouped
        {
            get { return _friendsGrouped; }
            set { SetProperty(ref _friendsGrouped,value); }
        }

        public Member FriendListSelectedItem
        {
            get { return null; }
            set
            {
                SetProperty(ref _friendListSelectedItem,value);
                if(value != null)
                {
                    Navigate("MemberDetailPage?id=" + ((Member)value).MemberId);
                }
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

        public List<Member> FriendListItemsSource
        {
            get
            {
                if(_friendList == null)
                {
                    _friendList = new List<Member>();
                }
                return _friendList;
            }
            set => _friendList = value;
        }

        public bool IsNoFriend
        {
            get { return _isNoFriend; }
            set { SetProperty(ref _isNoFriend,value); }
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

        public DelegateCommand<object> ItemAppearingCommand
        {
            get
            {
                return new DelegateCommand<object>((obj) =>
                {
                    OnItemAppearing(obj);
                });
            }
        }

        public DelegateCommand GoToSearchCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _eventAggregator.GetEvent<ChangeTabbedEvent>().Publish(0);
                });
            }
        }

        public DelegateCommand ReloadFriend
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if(FriendListItemsSource.Count == 0)
                    {
                        LoadItems();
                    }
                });
            }
        }

        public string MessageCount
        {
            get => _messageCount;
            set
            {
                if(value != _messageCount)
                {
                    SetProperty(ref _messageCount,value);
                }
            }
        }

        public string SearchKeyword
        {
            get { return _searchKeyword; }
            set
            {
                SetProperty(ref _searchKeyword,value);

                //Use linq to sorty our monkeys by name and then group them by the new name sort property
                var sorted = from member in FriendListItemsSource
                             where member.FullName.ToLower().Contains(_searchKeyword.ToLower())
                             orderby member.MemberFirstName
                             group member by member.NameSort into memberGroup
                             select new GroupsCollection<string,Member>(memberGroup.Key,memberGroup);

                //create a new collection of groups
                FriendsGrouped = new ObservableCollection<GroupsCollection<string,Member>>(sorted);

            }
        }

        public string MessageLogo
        {
            get { return _messageLogo; }
            set { SetProperty(ref _messageLogo,value); }
        }

        public FriendsPageViewModel(INavigationService navigationService,IFitMeetRestService fitMeetRestServices,IEventAggregator eventAggregator) : base(navigationService,fitMeetRestServices)
        {
            Title = "Friends";
            _eventAggregator = eventAggregator;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _isChecking = true;
            Device.StartTimer(TimeSpan.FromSeconds(5),Callback);

            if(FriendListItemsSource.Count == 0)
            {
                LoadItems();
            }
        }

        private bool Callback()
        {
            if(_isChecking)
            {
                checkMessage();
            }
            return _isChecking;
        }

        private async void checkMessage()
        {
            var count = await _fitMeetRestService.CheckMessage();
            Debug.WriteLine("there are {0} message",count);
            if(count == 0)
            {
                MessageCount = String.Empty;
            }
            else
            {   
                MessageCount = count.ToString();
            }
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            _isChecking = false;
            base.OnNavigatedFrom(parameters);

        }


        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
        }

        private async void LoadItems()
        {
            IsRefreshing = true;

            var restResponseMessage = await GetDataFromRest(_curentPage + 1);
            if(restResponseMessage == null)
            {
                _curentPage++;
                return;
            }
            var output = restResponseMessage?.Output;
            _curentPage = output.Currentpage;
            _pageCount = output.Pagecount;

            var result = output?.Response;
            if(result != null)
            {
                IsNoFriend = false;
                FriendListItemsSource.AddRange(result);
                //Use linq to sorty our monkeys by name and then group them by the new name sort property
                var sorted = from member in FriendListItemsSource
                             orderby member.MemberFirstName
                             group member by member.NameSort into memberGroup
                             select new GroupsCollection<string,Member>(memberGroup.Key,memberGroup);

                //create a new collection of groups
                FriendsGrouped = new ObservableCollection<GroupsCollection<string,Member>>(sorted);
            }

            IsRefreshing = false;
        }
        private async Task<ResponseMessage<List<Member>>> GetDataFromRest(int page)
        {
            ResponseMessage<List<Member>> restResponseMessage;
            restResponseMessage = await _fitMeetRestService.GetFriendsAsync(page);

            return restResponseMessage;
        }

        private void ReloadItems()
        {
            FriendListItemsSource.Clear();
            FriendsGrouped.Clear();
            _curentPage = 0;
            _pageCount = 1;
            LoadItems();
        }

        ~FriendsPageViewModel()
        {

        }

        private void OnItemAppearing(object obj)
        {
            if(IsRefreshing || FriendListItemsSource.Count == 0)
                return;

            //hit bottom!
            if(obj == FriendListItemsSource.Last())
            {
                if(HasNext)
                    LoadItems();
            }
        }

    }
}
