using FitMeet.Models;
using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FitMeet.ViewModels
{
    public class ProfileEditPageViewModel : ViewModelBase
    {
        private readonly IActivityService _activityServices;

        private ObservableCollection<ActivityKey> _activitiesLevel;
        private UserProfile _dataSource;
        private List<Level> _levelsData;
        private List<Activity> _activitiesData;

        public ProfileEditPageViewModel( INavigationService navigationService ,
            IFitMeetRestService fitMeetRestServices , IActivityService activityServices ) : base(navigationService , fitMeetRestServices)
        {
            _activityServices = activityServices;
        }

        public UserProfile DataSource
        {
            get => _dataSource;
            set => SetProperty(ref _dataSource , value);
        }
        public ObservableCollection<ActivityKey> ActivitiesLevel
        {
            get
            {
                return _activitiesLevel;
            }
            set
            {
                SetProperty(ref _activitiesLevel , value);
            }
        }

        public DelegateCommand EditCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    ActivitiesLevel.Add(new ActivityKey(new Activity() , new Level()));
                    ActivitiesLevel = ActivitiesLevel;
                });
            }
        }

        public List<Level> LevelsData
        {
            get => _levelsData;
            set => SetProperty(ref _levelsData , value);
        }
        public List<Activity> ActivitiesData
        {
            get => _activitiesData;
            set => SetProperty(ref _activitiesData , value);
        }

        public override async void OnNavigatedTo( NavigationParameters parameters )
        {
            base.OnNavigatedTo(parameters);
            DataSource = (UserProfile)parameters["data"];
            await _activityServices.UpdateAsync();
            LevelsData = _activityServices.Levels;
            ActivitiesData = _activityServices.Activities;
            ActivitiesLevel = new ObservableCollection<ActivityKey>();
            foreach ( var skillData in DataSource.Skill )
            {
                var skill = _activityServices.GetLevel(skillData.LevelId);
                foreach ( var activityData in skillData.Activities )
                {
                    var activity = _activityServices.GetActivity(activityData.ActivityId);
                    ActivitiesLevel.Add(new ActivityKey(activity , skill));
                }
            }

        }


    }
}
