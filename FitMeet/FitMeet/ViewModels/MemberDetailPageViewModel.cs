using FitMeet.Models;
using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class MemberDetailPageViewModel : ViewModelBase
    {

        private MemberDetail _dataSource;
        private double _activitiesHeightRequest;
        private double _trainingPlaceHeightRequest;
        private bool _isAdding = false;

        public DelegateCommand AddFriendCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    IsAdding = true;
                    var success = await _fitMeetRestService.AddFriendsAsync(_dataSource.UserId);
                    if (success)
                    {
                        DataSource.IsFriend = "Sent";
                        RaisePropertyChanged("DataSource");
                    }
                    IsAdding = false;
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
            set { SetProperty(ref _dataSource, value); }
        }
        public double TrainingPlaceHeightRequest
        {
            get { return _trainingPlaceHeightRequest; }
            set => SetProperty(ref _trainingPlaceHeightRequest, value);
        }

        public double ActivitiesHeightRequest
        {
            get { return _activitiesHeightRequest; }
            set { SetProperty(ref _activitiesHeightRequest, value); }
        }
        public bool IsAdding
        {
            get { return _isAdding; }
            set { SetProperty(ref _isAdding, value); }
        }
        public MemberDetailPageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestService) : base(navigationService, fitMeetRestService)
        {
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            var id = parameters["id"].ToString();

            var restResponseMessage = await _fitMeetRestService.GetMemberDetailAsync(id);
            DataSource = restResponseMessage?.Output?.Response;
            ActivitiesHeightRequest = DataSource.CountSkill * 30;
            TrainingPlaceHeightRequest = DataSource.CountTrainPlace * 18;
        }
    }
}
