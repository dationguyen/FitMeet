using FitMeet.Models;
using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class MePageViewModel : ViewModelBase
    {
        private UserProfile _dataSource;
        private double _activitiesHeightRequest;
        private double _trainingPlaceHeightRequest;

        public object NullItem
        {
            get => null;
            set => RaisePropertyChanged("NullItem");
        }

        public double TrainingPlaceHeightRequest
        {
            get { return _trainingPlaceHeightRequest; }
            set => SetProperty(ref _trainingPlaceHeightRequest , value);
        }

        public double ActivitiesHeightRequest
        {
            get { return _activitiesHeightRequest; }
            set { SetProperty(ref _activitiesHeightRequest , value); }
        }

        public UserProfile DataSource
        {
            get => _dataSource;
            set => SetProperty(ref _dataSource , value);
        }

        public DelegateCommand EditCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var param = new NavigationParameters() { { "data" , DataSource } };
                    await _navigationService.NavigateAsync("ProfileEditPage" , param , false , true);
                });
            }
        }

        public MePageViewModel( INavigationService navigationService , IFitMeetRestService fitMeetRestServices ) : base(navigationService , fitMeetRestServices)
        {
            Title = "Me";
        }

        public async override void OnNavigatedTo( NavigationParameters parameters )
        {
            base.OnNavigatingTo(parameters);
            var response = await _fitMeetRestService.GetUserProfileAsync();
            DataSource = response?.Output?.Response;
            if ( DataSource != null )
            {
                ActivitiesHeightRequest = DataSource.CountSkill * 30;
                TrainingPlaceHeightRequest = DataSource.CountTrainPlace * 18;
            }
        }
    }
}
