using System;
using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class AdvanceFilterPopupViewModel : ViewModelBase
    {
        public DelegateCommand NavigateBackCommand { get; }
        public AdvanceFilterPopupViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestService) : base(navigationService, fitMeetRestService)
        {
            NavigateBackCommand = new DelegateCommand(OnNavigateBackCommandExecuted);
        }

        private void OnNavigateBackCommandExecuted()
        {
            throw new NotImplementedException();
        }
    }
}
