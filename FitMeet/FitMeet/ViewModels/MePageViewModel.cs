using FitMeet.Models;
using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class MePageViewModel : ViewModelBase
    {
        private UserProfile _dataSource;
		private double _activitiesHeightRequest;
		private double _trainingPlaceHeightRequest;

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

		public UserProfile DataSource
		{
			get => _dataSource;
			set => SetProperty(ref _dataSource, value);
		}

        public MePageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestServices) : base(navigationService, fitMeetRestServices)
        {
            Title = "Me";
        }

        public async override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if (DataSource==null)
            {
				var response = await _fitMeetRestService.GetUserProfileAsync();
				DataSource = response?.Output?.Response;
            }
			ActivitiesHeightRequest = DataSource.CountSkill * 30;
			TrainingPlaceHeightRequest = DataSource.CountTrainPlace * 20;
        }
    }
}
