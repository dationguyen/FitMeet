using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace FitMeet.ViewModels
{
    public class MainPageViewModel:ViewModelBase
    {
        private ITokenService _tokenService;
        private IPageDialogService _dialogService;

        public MainPageViewModel(INavigationService navigationService,ITokenService tokenService,
            IFitMeetRestService fitMeetRestServices,IPageDialogService dialogService) : base(navigationService,fitMeetRestServices)
        {
            _tokenService = tokenService;
            _dialogService = dialogService;
        }

        public DelegateCommand LogOutCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var confirm = await _dialogService.DisplayAlertAsync("Logout","Are you sure you want to log out","Ok",
                        "Cancel");
                    if(confirm)
                    {
                        _fitMeetRestService.LogOut();
                        _tokenService.SetToken(null);
                        NavigateCommand.Execute("app:///NavigationPage/LoginPage");
                    }
                });
            }
        }
    }
}
