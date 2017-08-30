using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using System;

namespace FitMeet.ViewModels
{
    public class StartupPageViewModel:ViewModelBase
    {
        private ITokenService _tokenService;

        public DelegateCommand OnAppearingCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var token = _tokenService.GetToken();
                    if(!String.IsNullOrEmpty(token) && await _fitMeetRestService.CheckTokenAsync(token))
                    {
                        _fitMeetRestService.SetToken(token);
                        NavigateCommand.Execute("app:///MainPage/NavigationPage/MainTabbedPage");
                    }
                    else
                    {
                        NavigateCommand.Execute("app:///NavigationPage/LoginPage");
                    }
                });
            }
        }

        public StartupPageViewModel(INavigationService navigationService,IFitMeetRestService fitMeetRestServices,ITokenService tokenService) : base(navigationService,fitMeetRestServices)
        {
            _tokenService = tokenService;
        }

    }
}
