using FitMeet.Models;
using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace FitMeet.ViewModels
{
    public class MemberDetailPageViewModel:ViewModelBase
    {
        private IPageDialogService _pageDialogService;

        private MemberDetail _dataSource;
        private double _activitiesHeightRequest;
        private double _trainingPlaceHeightRequest;
        private bool _isAdding = false;
        private bool _isLoading = true;
        private bool _isShowInputDialog = false;
        private bool _isFriend = true;

        public DelegateCommand AddFriendCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    IsAdding = true;
                    var success = await _fitMeetRestService.AddFriendsAsync(_dataSource.UserId);
                    if(success)
                    {
                        DataSource.IsFriend = "Sent";
                        RaisePropertyChanged("DataSource");
                    }
                    IsAdding = false;
                });
            }
        }

        public DelegateCommand UnfriendCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var action = await _pageDialogService?.DisplayAlertAsync("","I am sure about unfriending " + DataSource.FullName,
                        "Unfriend","Cancel");
                    if(action)
                    {
                        var result = await _fitMeetRestService.UnfriendAsync(DataSource.UserId);
                        if(result?.Output?.Status == 1)
                        {
                            NavigateBackCommand.Execute();
                        }
                    }

                });
            }
        }

        public DelegateCommand MessageCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var param = new NavigationParameters()
                    {
                        {"id",_dataSource.UserId},
                        { "name",_dataSource.FullName}
                    };
                    await _navigationService.NavigateAsync("ChatPage",param,false,true);
                });
            }
        }

        public DelegateCommand<string> ResponseCommand
        {
            get
            {
                return new DelegateCommand<string>(async (status) =>
                {
                    var success = await _fitMeetRestService.ResponseToFriendAsync(DataSource.UserId,status);
                    if(success)
                    {
                        NavigateBackCommand.Execute();
                    }
                    else
                    {
                        await _pageDialogService.DisplayAlertAsync("Error","Can't response to this person\nPlease try later",
                             "Ok");
                    }
                });
            }
        }

        public DelegateCommand ReportCommand
        {
            get
            {
                return new DelegateCommand(async () =>
               {
                   var action = await _pageDialogService?.DisplayActionSheetAsync("","Cancel",null,
                       "Report",
                       "Report and Block");
                   switch(action)
                   {
                       case "Report":
                           await _fitMeetRestService.BlockfriendAsync(DataSource.UserId);
                           await _navigationService.GoBackAsync();
                           break;
                       case "Report and Block":
                           IsShowInputDialog = true;
                           break;
                       default:
                           break;
                   }
               });
            }
        }
        public DelegateCommand<string> ReportAndBlockCommand
        {
            get
            {
                return new DelegateCommand<string>(async (s) =>
                {
                    await _fitMeetRestService.BlockfriendAsync(DataSource.UserId,s);
                    IsShowInputDialog = false;
                    await _navigationService.GoBackAsync();
                });
            }
        }

        public object NullItem
        {
            get => null;
            set => RaisePropertyChanged("NullItem");
        }

        public MemberDetail DataSource
        {
            get { return _dataSource; }
            set { SetProperty(ref _dataSource,value); }
        }
        public double TrainingPlaceHeightRequest
        {
            get { return _trainingPlaceHeightRequest; }
            set => SetProperty(ref _trainingPlaceHeightRequest,value);
        }

        public double ActivitiesHeightRequest
        {
            get { return _activitiesHeightRequest; }
            set { SetProperty(ref _activitiesHeightRequest,value); }
        }
        public bool IsAdding
        {
            get { return _isAdding; }
            set { SetProperty(ref _isAdding,value); }
        }

        public bool IsShowInputDialog
        {
            get { return _isShowInputDialog; }
            set { SetProperty(ref _isShowInputDialog,value); }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading,value); }
        }

        public bool IsFriend
        {
            get => _isFriend;
            set => SetProperty(ref _isFriend,value);
        }

        public MemberDetailPageViewModel(INavigationService navigationService,IFitMeetRestService fitMeetRestService,IPageDialogService pageDialogService) : base(navigationService,fitMeetRestService)
        {
            _pageDialogService = pageDialogService;
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            IsLoading = true;
            var id = parameters["id"].ToString();
            var restResponseMessage = await _fitMeetRestService.GetMemberDetailAsync(id);
            DataSource = restResponseMessage?.Output?.Response;
            ActivitiesHeightRequest = DataSource.CountSkill * 30;
            TrainingPlaceHeightRequest = DataSource.CountTrainPlace * 18;
            IsFriend = DataSource.IsFriend == "Friends";
            IsLoading = false;
            base.OnNavigatingTo(parameters);
        }
    }
}
