using FitMeet.Models;
using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FitMeet.ViewModels
{
    public class ProfileEditPageViewModel : ViewModelBase
    {
        private readonly IStaticDataService _staticDataServices;

        private ObservableCollection<ActivityKey> _activitiesLevel;
        private ObservableCollection<Place> _trainplaces;
        private ObservableCollection<Goal> _goals;

        private UserProfile _dataSource;
        private List<Level> _levelsData;
        private List<Activity> _activitiesData;
        private List<Place> _trainingLocations;
        private Goal _selectedGoal;
        private Goal _lastedGoal;
        private bool _isPopupVisible;

        public ProfileEditPageViewModel( INavigationService navigationService ,
            IFitMeetRestService fitMeetRestServices , IStaticDataService staticDataServices ) : base(navigationService , fitMeetRestServices)
        {
            _staticDataServices = staticDataServices;
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

        public DelegateCommand AddActivityCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    ActivitiesLevel.Add(new ActivityKey(ActivitiesData.First() , LevelsData.First()));

                });
            }
        }
        public DelegateCommand<object> DeleteActivityCommand
        {
            get
            {
                return new DelegateCommand<object>(( a ) =>
                {
                    var item = (ActivityKey)a;
                    ActivitiesLevel.Remove(item);
                });
            }
        }

        public DelegateCommand AddPlaceCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Trainplaces.Add(TrainingLocations?.First());
                });
            }
        }
        public DelegateCommand<object> DeletePlaceCommand
        {
            get
            {
                return new DelegateCommand<object>(( a ) =>
                {
                    var item = (Place)a;
                    Trainplaces.Remove(item);
                });
            }
        }
        public DelegateCommand<object> GoalSelected
        {
            get
            {
                return new DelegateCommand<object>(( a ) =>
                {
                    if ( _selectedGoal == Goals.Last() )
                    {
                        IsPopupVisible = true;
                    }
                });
            }
        }
        public DelegateCommand HidePopup
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SelectedGoal = _lastedGoal;
                    IsPopupVisible = false;
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

        public List<Place> TrainingLocations
        {
            get => _trainingLocations;
            set => SetProperty(ref _trainingLocations , value);
        }

        public ObservableCollection<Place> Trainplaces
        {
            get { return _trainplaces; }
            set { SetProperty(ref _trainplaces , value); }
        }

        public ObservableCollection<Goal> Goals
        {
            get { return _goals; }
            set { SetProperty(ref _goals , value); }
        }

        public Goal SelectedGoal
        {
            get { return _selectedGoal; }
            set
            {
                if ( value == Goals.Last() )
                    _lastedGoal = _selectedGoal;
                SetProperty(ref _selectedGoal , value);
            }
        }

        public bool IsPopupVisible
        {
            get { return _isPopupVisible; }
            set { SetProperty(ref _isPopupVisible , value); }
        }

        public override async void OnNavigatedTo( NavigationParameters parameters )
        {
            base.OnNavigatedTo(parameters);
            DataSource = (UserProfile)parameters["data"];

            await _staticDataServices.UpdateAsync();
            UpdateStaticResource();
        }

        private void UpdateStaticResource()
        {
            LevelsData = _staticDataServices.Levels;
            ActivitiesData = _staticDataServices.Activities;
            TrainingLocations = _staticDataServices.TrainingLocationData;
            Trainplaces = new ObservableCollection<Place>();
            ActivitiesLevel = new ObservableCollection<ActivityKey>();
            foreach ( var skillData in DataSource.Skill )
            {
                var skill = _staticDataServices.GetLevel(skillData.LevelId);
                foreach ( var activityData in skillData.Activities )
                {
                    var activity = _staticDataServices.GetActivity(activityData.ActivityId);
                    ActivitiesLevel.Add(new ActivityKey(activity , skill));
                }
            }
            foreach ( var place in DataSource.TrainPlace )
            {
                var p = _staticDataServices.GetTrainingLocation(place.LocationId);
                if ( p != null )
                    Trainplaces.Add(p);
            }
            Goals = new ObservableCollection<Goal>(_staticDataServices.Goals);
            if ( DataSource.GoalId != "6" )
            {
                SelectedGoal = Goals.First(t => t.Id == DataSource.GoalId);
            }
            else
            {
                var g = new Goal() { Id = "6" , Name = DataSource.Goal };
                Goals.Insert(Goals.Count - 1 , g);
                SelectedGoal = g;
            }
        }

    }
}
