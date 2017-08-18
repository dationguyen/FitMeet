using FitMeet.EventAggregator;
using FitMeet.Services;
using Prism.Events;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class MainTabbedPageViewModel:ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;

        private int _selectedIndex;

        public MainTabbedPageViewModel( INavigationService navigationService,IFitMeetRestService fitMeetRestServices,IEventAggregator eventAggregator ) : base(navigationService,fitMeetRestServices)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<ChangeTabbedEvent>().Subscribe(ChangeTab);
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if(value != _selectedIndex)
                    SetProperty(ref _selectedIndex,value);
            }
        }

        private void ChangeTab( int s )
        {
            SelectedIndex = s;
        }
    }
}
