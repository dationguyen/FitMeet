﻿using FitMeet.Models;
using FitMeet.Services;
using FitMeet.Services.DependencyServices;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FitMeet.ViewModels
{
    public class ProfileEditPageViewModel:ViewModelBase
    {
        private readonly IStaticDataService _staticDataServices;
        private readonly IPageDialogService _dialogService;
        private readonly IGoogleLocationService _googleLocation;
        private readonly IDependencyService _dependencyService;

        private ObservableCollection<ActivityKey> _activitiesLevel;
        private ObservableCollection<PlaceKey> _trainplaces;
        private ObservableCollection<Goal> _goals;

        private UserProfile _dataSource;
        private List<Level> _levelsData;
        private List<Activity> _activitiesData;
        private List<Place> _trainingLocations;
        private Goal _selectedGoal;
        private Goal _lastedGoal;
        private string _fullName;
        private bool _isLoading;
        private bool _isPopupVisible;
        private bool _hasCustomGoal = false;
        //private bool _hasChange = false;
        private bool _isMale = true;
        private DateTime _dob;
        private List<string> _autoCompleteCollection;
        private int _autoCompleteHeight;
        private string _autoCompleteResult;
        private string _address;
        private bool _isSearching;
        private string _profileImage;

        public ProfileEditPageViewModel(IDependencyService dependencyService,INavigationService navigationService,IPageDialogService dialogService,
            IFitMeetRestService fitMeetRestServices,IStaticDataService staticDataServices,IGoogleLocationService googleLocationService) : base(navigationService,fitMeetRestServices)
        {
            _dependencyService = dependencyService;
            _staticDataServices = staticDataServices;
            _dialogService = dialogService;
            _googleLocation = googleLocationService;
            EditImageCommand = new DelegateCommand(EditImageAsync);
        }

        public DelegateCommand EditImageCommand { get; set; }
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
                    IsLoading = true;
                    List<string> actIds = new List<string>();
                    List<string> skillIds = new List<string>();
                    List<string> ids = new List<string>();
                    List<string> placeIds = new List<string>();
                    List<string> locationIds = new List<string>();
                    foreach(var activityKey in ActivitiesLevel)
                    {
                        actIds.Add(activityKey.Activity.Id);
                        skillIds.Add(activityKey.Level.Id);
                    }
                    if(DataSource.Skill != null)
                        foreach(var skill in DataSource.Skill)
                        {
                            foreach(var act in skill.Activities)
                            {
                                ids.Add(act.ActivityUserId);
                            }
                        }
                    if(DataSource.TrainPlace != null)
                        foreach(var place in DataSource.TrainPlace)
                        {
                            placeIds.Add(place.TrainPlaceId);
                        }
                    foreach(var place in Trainplaces)
                    {
                        locationIds.Add(place.Place.LocationId);
                    }

                    string fName = "", lName = "";
                    string[] nameParts = FullName.ToString().Trim().Split(' ');
                    lName = nameParts.Last();
                    for(int i = 0 ; i < nameParts.Length - 1 ; i++)
                    {
                        fName += nameParts[i] + " ";
                    }

                    var a = await _fitMeetRestService.UpdateProfileAsync(fName.Trim(),
                        lName.Trim(),IsMale ? "Male" : "Female",
                        SelectedGoal.Id,SelectedGoal.Name,ProfileImage,
                        Address,DataSource.About,Dob.ToString("yyyy-MM-dd"),actIds,ids,skillIds,placeIds,locationIds);

                    if(a?.Output.Status == 1)
                    {
                        await _dialogService.DisplayAlertAsync("Success","Update Successfully","Ok");
                        await _navigationService.GoBackAsync();
                    }
                    else
                    {
                        await _dialogService.DisplayAlertAsync("Error","Update unsuccessful","Ok");
                    }
                    IsLoading = false;
                });
            }
        }
        public DelegateCommand<string> GenderSwitchCommand
        {
            get
            {
                return new DelegateCommand<string>((string isMale) =>
                {

                    IsMale = (isMale == "male");
                });
            }
        }

        public UserProfile DataSource
        {
            get => _dataSource;
            set => SetProperty(ref _dataSource,value);
        }
        public bool IsMale
        {
            get { return _isMale; }
            set
            {
                SetProperty(ref _isMale,value);
            }
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
        public DateTime Dob
        {
            get { return _dob; }
            set { SetProperty(ref _dob,value); }
        }
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName,value);
        }
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading,value); }
        public string Address
        {
            get { return _address; }
            set
            {
                if(_address != value)
                {
                    SetProperty(ref _address,value);

                    if(!String.IsNullOrEmpty(_address))
                    {
                        IsSearching = true;
                        GetAutoComplete(_address);

                    }
                    else
                    {
                        IsSearching = false;
                        AutoCompleteCollection.Clear();
                    }

                }
            }
        }
        public bool IsSearching
        {
            get => _isSearching;
            set
            {
                SetProperty(ref _isSearching,value);
            }
        }
        public List<string> AutoCompleteCollection
        {
            get
            {
                if(_autoCompleteCollection == null)
                    _autoCompleteCollection = new List<string>();
                return _autoCompleteCollection;
            }
            set => SetProperty(ref _autoCompleteCollection,value);
        }

        public string AutoCompleteResult
        {
            get { return null; }
            set
            {
                if(value != null)
                {
                    SetProperty(ref _autoCompleteResult,value);
                    _address = value;
                    RaisePropertyChanged("Address");
                    IsSearching = false;
                }
            }
        }

        public int AutoCompleteHeight
        {
            get { return _autoCompleteHeight; }
            set { SetProperty(ref _autoCompleteHeight,value); }
        }

        public string ProfileImage
        {
            get { return _profileImage; }
            set { SetProperty(ref _profileImage,value); }
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            DataSource = (UserProfile)parameters["data"];
            if(DataSource == null)
                return;
            IsMale = DataSource.IsMale;

            await _staticDataServices.UpdateAsync();
            UpdateStaticResource();

            _address = DataSource.Address;
            RaisePropertyChanged("Address");

            FullName = DataSource.FullName;
            ProfileImage = DataSource.UserPhoto;

            DateTime.TryParseExact(DataSource.Dob ?? "1990-01-01","yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture,DateTimeStyles.None,out var dob);
            Dob = dob;
        }

        private void UpdateStaticResource()
        {
            LevelsData = _staticDataServices.Levels;
            ActivitiesData = _staticDataServices.Activities;
            TrainingLocations = _staticDataServices.TrainingLocationData;
            Trainplaces = new ObservableCollection<PlaceKey>();
            ActivitiesLevel = new ObservableCollection<ActivityKey>();
            if(DataSource.Skill != null)
                foreach(var skillData in DataSource.Skill)
                {
                    var skill = _staticDataServices.GetLevel(skillData.LevelId);
                    foreach(var activityData in skillData.Activities)
                    {
                        var activity = _staticDataServices.GetActivity(activityData.ActivityId);
                        ActivitiesLevel.Add(new ActivityKey(activity,skill));
                    }
                }
            if(DataSource.TrainPlace != null)
                foreach(var place in DataSource.TrainPlace)
                {
                    var p = _staticDataServices.GetTrainingLocation(place.LocationId);
                    if(p != null)
                        Trainplaces.Add(new PlaceKey(p));
                }
            Goals = new ObservableCollection<Goal>(_staticDataServices.Goals);
            if(DataSource.GoalId != "6")
            {
                SelectedGoal = Goals.First(t => t.Id == DataSource.GoalId);
            }
            else
            {
                _hasCustomGoal = true;
                var g = new Goal() { Id = "6",Name = DataSource.Goal };
                Goals.Insert(Goals.Count - 1,g);
                SelectedGoal = g;
            }
        }
        private async void GetAutoComplete(string searchKeyWord)
        {
            var result = await _googleLocation.AutoComplete(searchKeyWord);
            if(result != null && result.Count > 0)
            {
                AutoCompleteHeight = 36 * result.Count;
                AutoCompleteCollection = result;
            }
        }

        private async void EditImageAsync()
        {
            IsLoading = true;
            Stream stream = await _dependencyService.Get<IPicturePicker>().GetImageStreamAsync();
            var mem = ReadFully(stream);

            if(mem != null)
            {
                var imageSource = await ImageUploadHelper.UploadImage(mem);
                if(imageSource != null)
                {
                    ProfileImage = imageSource;
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("error",
                         "There are some problems with our server, please try again later","ok");
                }
            }
            IsLoading = false;
        }
        private byte[] ReadFully(Stream input)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);

                return ms.ToArray();
            }
        }
        ~ProfileEditPageViewModel()
        {
        }
    }
}
