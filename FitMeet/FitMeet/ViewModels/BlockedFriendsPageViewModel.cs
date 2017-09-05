using FitMeet.Models;
using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;

namespace FitMeet.ViewModels
{
    public class BlockedFriendsPageViewModel:ViewModelBase
    {
        private List<BlockedFriend> _blockedFriends;
        private bool _hasNoBlockedFriends = true;


        public BlockedFriendsPageViewModel(INavigationService navigationService,IFitMeetRestService fitMeetRestService) : base(navigationService,fitMeetRestService)
        {
            ItemTappedCommand = new DelegateCommand<string>(ItemTapped);
        }

        private void ItemTapped(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                Navigate("MemberDetailPage?id=" + id);
            }
        }

        public DelegateCommand<string> ItemTappedCommand { get; }

        public List<BlockedFriend> BlockedFriends
        {
            get
            {
                if(_blockedFriends == null)
                    _blockedFriends = new List<BlockedFriend>();
                return _blockedFriends;
            }
            set => SetProperty(ref _blockedFriends,value);
        }

        public bool HasNoBlockedFriends
        {
            get { return _hasNoBlockedFriends; }
            set { SetProperty(ref _hasNoBlockedFriends,value); }
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var data = await _fitMeetRestService.GetBlockedFriendsAsync();
            if(data?.Output?.Status == 1)
            {
                BlockedFriends = data?.Output?.Response;
                HasNoBlockedFriends = false;
            }
        }
    }
}
