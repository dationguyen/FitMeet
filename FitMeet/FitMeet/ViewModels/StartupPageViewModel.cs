using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using System;

namespace FitMeet.ViewModels
{
    public class StartupPageViewModel:ViewModelBase
    {
        private ITokenServices _tokenServices;

        public DelegateCommand OnAppearingCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                     NavigateCommand.Execute("app:///MainPage/NavigationPage/MainTabbedPage");
                    //var token = _tokenServices.GetToken();
                    //if(!String.IsNullOrEmpty(token) && await _tokenServices.HasValidToken())
                    //{
                    //    _fitMeetRestService.SetToken(token);
                    //    NavigateCommand.Execute("app:///MainPage/NavigationPage/MainTabbedPage");
                    //}
                    //else
                    //{
                    //    NavigateCommand.Execute("app:///NavigationPage/LoginPage");
                    //}
                });
            }
        }

        public StartupPageViewModel(INavigationService navigationService,IFitMeetRestService fitMeetRestServices,ITokenServices tokenServices) : base(navigationService,fitMeetRestServices)
        {
            _tokenServices = tokenServices;
        }

    }
}
