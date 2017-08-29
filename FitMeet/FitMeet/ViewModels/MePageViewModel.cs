using FitMeet.Models;
using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace FitMeet.ViewModels
{
    public class MePageViewModel:ViewModelBase
    {
        private readonly IPageDialogService _dialogService;

        private UserProfile _dataSource;
        private double _activitiesHeightRequest;
        private double _trainingPlaceHeightRequest;
        private bool _isVerifying = false;


        public object NullItem
        {
            get => null;
            set => RaisePropertyChanged("NullItem");
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

        public UserProfile DataSource
        {
            get => _dataSource;
            set => SetProperty(ref _dataSource,value);
        }

        public DelegateCommand EditCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var param = new NavigationParameters() { { "data",DataSource } };
                    await _navigationService.NavigateAsync("ProfileEditPage",param,false,true);
                });
            }
        }

        public DelegateCommand VerifyCommand
        {
            get
            {
                return new DelegateCommand(async () =>
               {
                   IsVerifying = true;
                   var success = await _fitMeetRestService.VerifyAsync();
                   if(success)
                   {
                       await _dialogService.DisplayAlertAsync("Vertifying","Please check your email to verify your account",
                         "Ok");
                   }
                   IsVerifying = false;

               });
            }
        }


        public bool IsVerifying
        {
            get => _isVerifying;
            set => SetProperty(ref _isVerifying,value);
        }

        public MePageViewModel(INavigationService navigationService,IFitMeetRestService fitMeetRestServices,IPageDialogService dialogService) : base(navigationService,fitMeetRestServices)
        {
            Title = "Me";
            _dialogService = dialogService;
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            var response = await _fitMeetRestService.GetUserProfileAsync();
            DataSource = response?.Output?.Response;
            if(DataSource != null)
            {
                ActivitiesHeightRequest = DataSource.CountSkill * 26;
                TrainingPlaceHeightRequest = DataSource.CountTrainPlace * 18;
            }
        }
    }
}
