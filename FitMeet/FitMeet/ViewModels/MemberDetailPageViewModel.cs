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

        public DelegateCommand AddFriendCommand => new DelegateCommand(AddFriend);

        public DelegateCommand<string> ReportAndBlockCommand => new DelegateCommand<string>(ReportAndBlock);

        public DelegateCommand UnfriendCommand => new DelegateCommand(Unfriend);

        public DelegateCommand MessageCommand => new DelegateCommand(Message);

        public DelegateCommand<string> ResponseCommand => new DelegateCommand<string>(Response);

        public DelegateCommand ReportCommand => new DelegateCommand(Report);
        public DelegateCommand UnblockCommand => new DelegateCommand(Unblock);

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


        private async void Message()
        {
            var param = new NavigationParameters()
            {
                {"id", _dataSource.UserId},
                {"name", _dataSource.FullName}
            };
            await _navigationService.NavigateAsync("ChatPage",param,false,true);
        }
        private async void Response(string status)
        {
            var success = await _fitMeetRestService.ResponseToFriendAsync(DataSource.UserId,status);
            if(success)
            {
                NavigateBackCommand.Execute();
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Error","Can't response to this person\nPlease try later","Ok");
            }
        }
        private async void Unfriend()
        {
            var action = await _pageDialogService?.DisplayAlertAsync("","I am sure about unfriending " + DataSource.FullName,"Unfriend","Cancel");
            if(action)
            {
                var result = await _fitMeetRestService.UnfriendAsync(DataSource.UserId);
                if(result?.Output?.Status == 1)
                {
                    NavigateBackCommand.Execute();
                }
            }
        }
        private async void Report()
        {
            var action = await _pageDialogService?.DisplayActionSheetAsync("","Cancel",null,"Block","Report and Block");
            switch(action)
            {
                case "Block":
                    await _fitMeetRestService.BlockfriendAsync(DataSource.UserId);
                    await _navigationService.GoBackAsync();
                    break;
                case "Report and Block":
                    IsShowInputDialog = true;
                    break;
                default:
                    break;
            }
        }
        private async void AddFriend()
        {
            IsAdding = true;
            var success = await _fitMeetRestService.AddFriendsAsync(_dataSource.UserId);
            if(success)
            {
                DataSource.IsFriend = "Sent";
                RaisePropertyChanged("DataSource");
            }
            IsAdding = false;
        }
        private async void ReportAndBlock(string s)
        {
            await _fitMeetRestService.BlockfriendAsync(DataSource.UserId,s);
            IsShowInputDialog = false;
            await _navigationService.GoBackAsync();
        }



        private async void Unblock()
        {
            IsAdding = true;
            var result = await _fitMeetRestService.UnblockfriendAsync(DataSource.UserId);
            if(result)
            {
                DataSource.IsFriend = "Friends";
                RaisePropertyChanged("DataSource");
            }
            IsAdding = false;

        }
    }
}
