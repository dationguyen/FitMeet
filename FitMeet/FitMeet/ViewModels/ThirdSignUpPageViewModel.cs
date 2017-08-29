using FitMeet.Models;
using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FitMeet.ViewModels
{
    public class ThirdSignUpPageViewModel:ViewModelBase
    {
        private IStaticDataService _staticDataService;
        private IPageDialogService _dialogService;

        private ObservableCollection<ActivityKey> _activitiesLevel;
        private ObservableCollection<PlaceKey> _trainplaces;
        private ObservableCollection<Goal> _goals;

        private List<Level> _levelsData;
        private List<Activity> _activitiesData;
        private List<Place> _trainingLocations;
        private Goal _selectedGoal;
        private Goal _lastedGoal;

        private bool _isPopupVisible;
        private bool _hasCustomGoal = false;

        public Goal SelectedGoal
        {
            get { return _selectedGoal; }
            set
            {
                if(value == Goals.Last())
                    _lastedGoal = _selectedGoal;
                SetProperty(ref _selectedGoal,value);
            }
        }
        public bool IsPopupVisible
        {
            get { return _isPopupVisible; }
            set { SetProperty(ref _isPopupVisible,value); }
        }

        public ObservableCollection<ActivityKey> ActivitiesLevel
        {
            get
            {
                return _activitiesLevel;
            }
            set
            {
                SetProperty(ref _activitiesLevel,value);
            }
        }
        public ObservableCollection<PlaceKey> Trainplaces
        {
            get { return _trainplaces; }
            set { SetProperty(ref _trainplaces,value); }
        }
        public ObservableCollection<Goal> Goals
        {
            get { return _goals; }
            set { SetProperty(ref _goals,value); }
        }

        public List<Level> LevelsData
        {
            get => _levelsData;
            set => SetProperty(ref _levelsData,value);
        }
        public List<Activity> ActivitiesData
        {
            get => _activitiesData;
            set => SetProperty(ref _activitiesData,value);
        }
        public List<Place> TrainingLocations
        {
            get => _trainingLocations;
            set => SetProperty(ref _trainingLocations,value);
        }

        public DelegateCommand AddActivityCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    ActivitiesLevel.Add(new ActivityKey(ActivitiesData.First(),LevelsData.First()));

                });
            }
        }
        public DelegateCommand<object> DeleteActivityCommand
        {
            get
            {
                return new DelegateCommand<object>((a) =>
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
                    var a = TrainingLocations?.First();
                    Trainplaces.Add(new PlaceKey(a));
                });
            }
        }
        public DelegateCommand<object> DeletePlaceCommand
        {
            get
            {
                return new DelegateCommand<object>((a) =>
                {
                    var item = (PlaceKey)a;
                    Trainplaces.Remove(item);
                });
            }
        }
        public DelegateCommand<object> GoalSelected
        {
            get
            {
                return new DelegateCommand<object>((a) =>
                {
                    if(_selectedGoal == Goals.Last())
                    {
                        IsPopupVisible = true;
                    }
                });
            }
        }
        public DelegateCommand HidePopupCommand
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
        public DelegateCommand<string> SetCustomGoalCommand
        {
            get
            {
                return new DelegateCommand<string>((s) =>
                {
                    var g = new Goal() { Id = "6",Name = s };

                    if(_hasCustomGoal)
                    {
                        Goals.RemoveAt(Goals.Count - 2);
                    }

                    Goals.Insert(Goals.Count - 1,g);
                    _hasCustomGoal = true;
                    IsPopupVisible = false;
                });
            }
        }

        public DelegateCommand SubmitCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    if (IsNotDuplicate())
                    {
                        var result = await _fitMeetRestService.SignUpStep3Async(_activitiesLevel.ToList(), _trainplaces.Select(x=>x.Place).ToList(), SelectedGoal);
                        if (result != null && result.Output?.Status == 1)
                        {
                            Navigate("app:///MainPage/NavigationPage/MainTabbedPage");
                        }
                    }
                });
            }
        }


        public ThirdSignUpPageViewModel(INavigationService navigationService,IStaticDataService staticDataService,IPageDialogService dialogService,
            IFitMeetRestService fitMeetRestService) : base(navigationService,fitMeetRestService)
        {
            _staticDataService = staticDataService;
            _dialogService = dialogService;
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);


            await _staticDataService.UpdateAsync();
            UpdateStaticResource();

        }

        private bool IsNotDuplicate()
        {
            var duplicateActivities = _activitiesLevel.GroupBy(x => x.Activity)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key);

            if(duplicateActivities.Count() > 0)
            {
                _dialogService.DisplayAlertAsync("Error","You can not have duplicate activities","Ok");
                return false;
            }

            var duplicateTrainningPlace = _trainplaces.GroupBy(x => x.Place)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key);
            if(duplicateTrainningPlace.Count() > 0)
            {
                _dialogService.DisplayAlertAsync("Error","You can not have duplicate trainning places","Ok");
                return false;
            }

            return true;
        }

        private void UpdateStaticResource()
        {
            LevelsData = _staticDataService.Levels;
            ActivitiesData = _staticDataService.Activities;
            TrainingLocations = _staticDataService.TrainingLocationData;
            Trainplaces = new ObservableCollection<PlaceKey>();
            ActivitiesLevel = new ObservableCollection<ActivityKey>();

            ActivitiesLevel.Add(new ActivityKey(ActivitiesData.First(),LevelsData.First()));
            var a = TrainingLocations?.First();
            Trainplaces.Add(new PlaceKey(a));

            Goals = new ObservableCollection<Goal>(_staticDataService.Goals);
            SelectedGoal = Goals.First();
        }
    }
}
