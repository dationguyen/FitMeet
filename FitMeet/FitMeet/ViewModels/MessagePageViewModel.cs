using FitMeet.Models;
using FitMeet.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace FitMeet.ViewModels
{
    public class MessagePageViewModel:ViewModelBase
    {
        private bool _isTicking;
        private bool _hasNoData = true;
        private IRequestItem _selectedItem;

        private List<IRequestItem> _itemsDataSource;

        public MessagePageViewModel(INavigationService navigationService,IFitMeetRestService fitMeetRestServices) : base(navigationService,fitMeetRestServices)
        {
        }

        public List<IRequestItem> ItemsDataSource
        {
            get { return _itemsDataSource; }
            set { SetProperty(ref _itemsDataSource,value); }
        }

        public bool HasNoData
        {
            get => _hasNoData; set => SetProperty(ref _hasNoData,value);
        }

        public IRequestItem SelectedItem
        {
            get { return null; }
            set
            {
                SetProperty(ref _selectedItem,value);
                if(_selectedItem != null)
                {
                    if(_selectedItem is Msg)
                    {
                        var param = new NavigationParameters()
                        {
                            {"id",_selectedItem.UserId},
                            { "name",_selectedItem.UserName}
                        };
                        _navigationService.NavigateAsync("ChatPage",param,false,true);
                    }
                    else
                    {
                        Navigate("MemberDetailPage?id=" + _selectedItem.UserId);
                    }
                }
            }
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            LoadData();
            base.OnNavigatingTo(parameters);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            _isTicking = true;
            Device.StartTimer(TimeSpan.FromSeconds(5),TimerOnTick);
            base.OnNavigatedTo(parameters);
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            _isTicking = false;
            base.OnNavigatedFrom(parameters);
        }

        private bool TimerOnTick()
        {
            if(_isTicking)
            {
                LoadData();
            }
            return _isTicking;
        }

        private async void LoadData()
        {
            var data = await _fitMeetRestService.CheckRequestAsync();
            if(data != null)
            {
                var response = data.Output?.Response;
                if(response != null)
                {
                    var listItem = new List<IRequestItem>();
                    if(response.Friend != null)
                    {
                        foreach(var friend in response.Friend)
                        {
                            listItem.Add(friend);
                        }
                    }
                    if(response.Msg != null)
                    {
                        foreach(var msg in response.Msg)
                        {
                            if(msg.IsLastSender == 1)
                            {
                                msg.Message = "me: " + msg.Message;
                            }
                            listItem.Add(msg);
                        }
                    }
                    ItemsDataSource = listItem;
                    Debug.WriteLine("Load new item");
                }
            }
            if(ItemsDataSource != null && ItemsDataSource.Count > 0)
            {
                HasNoData = false;
                Debug.WriteLine("there are {0} item",ItemsDataSource.Count);
            }
        }
    }
}
