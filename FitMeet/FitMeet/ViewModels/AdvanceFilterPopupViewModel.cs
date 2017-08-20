using FitMeet.EventAggregator;
using FitMeet.Models;
using FitMeet.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FitMeet.ViewModels
{
    public class AdvanceFilterPopupViewModel:ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IStaticDataService _staticDataService;
        private readonly IPageDialogService _dialogService;

        private int _distanceIndex;
        private bool _isMale = true;
        private List<Activity> _activitiesSource = new List<Activity>();
        private ObservableCollection<Activity> _selectedActivities = new ObservableCollection<Activity>();

        public ObservableCollection<Activity> SelectedActivities
        {
            get { return _selectedActivities; }
            set
            {
                SetProperty(ref _selectedActivities,value);
            }
        }

        public int DistanceIndex
        {
            get { return _distanceIndex; }
            set { SetProperty(ref _distanceIndex,value); }
        }

        public bool IsMale
        {
            get { return _isMale; }
            set
            {
                SetProperty(ref _isMale,value);
            }
        }

        private int GetDistance()
        {
            switch(DistanceIndex)
            {
                case 0:
                    return 10;
                case 1:
                    return 20;
                case 2:
                    return 30;
                default:
                    return 10;
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

        public DelegateCommand SaveFilterCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    if(SelectedActivities.Count == 0)
                    {
                        await _dialogService.DisplayAlertAsync("Alert","Please select as least one activity","Ok");
                        return;
                    }
                    _eventAggregator.GetEvent<UpdateFilterEvent>().Publish(GetArgs());
#pragma warning disable CS0618 // Type or member is obsolete
                    await _navigationService.PopupGoBackAsync(null,false,false);
#pragma warning restore CS0618 // Type or member is obsolete
                });
            }
        }

        public DelegateCommand ClearFilterCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    _eventAggregator.GetEvent<UpdateFilterEvent>().Publish(null);
#pragma warning disable CS0618 // Type or member is obsolete
                    await _navigationService.PopupGoBackAsync(null,false,false);
#pragma warning restore CS0618 // Type or member is obsolete
                });
            }
        }

        public DelegateCommand CancelCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                   {
#pragma warning disable CS0618 // Type or member is obsolete
                       await _navigationService.PopupGoBackAsync(null,false,false);
#pragma warning restore CS0618 // Type or member is obsolete
                   });
            }
        }

        public List<Activity> ActivitiesSource
        {
            get { return _activitiesSource; }
            set
            {
                SetProperty(ref _activitiesSource,value);
            }
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            await _staticDataService.UpdateAsync();
            ActivitiesSource = _staticDataService.Activities;

            var filterAgr = (UpdateFilterEventArgs)parameters["agr"];
            if(filterAgr != null)
            {
                this.IsMale = filterAgr.IsMale;
                this.DistanceIndex = filterAgr.Distance / 10 - 1;
                var activitise = new List<Activity>();
                foreach(var item in ActivitiesSource)
                {
                    if(filterAgr.Activities.Contains(int.Parse(item.Id)))
                        activitise.Add(item);
                }
                SelectedActivities = new ObservableCollection<Activity>(activitise);
            }


        }


        public AdvanceFilterPopupViewModel(INavigationService navigationService,
            IFitMeetRestService fitMeetRestService,
            IPageDialogService dialogService,
            IEventAggregator eventAggregator,
            IStaticDataService staticDataService) : base(navigationService,fitMeetRestService)
        {
            _eventAggregator = eventAggregator;
            _staticDataService = staticDataService;
            _dialogService = dialogService;
        }

        private UpdateFilterEventArgs GetArgs()
        {
            var distance = (DistanceIndex + 1) * 10;
            var gender = IsMale;
            var activities = new List<int>();
            foreach(var item in SelectedActivities)
            {
                activities.Add(int.Parse(item.Id));
            }
            return new UpdateFilterEventArgs(distance,gender,activities);
        }


    }
}
