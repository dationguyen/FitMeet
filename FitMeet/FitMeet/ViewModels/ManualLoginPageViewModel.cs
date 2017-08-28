using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace FitMeet.ViewModels
{
    public class ManualLoginPageViewModel:ViewModelBase
    {
        private IPageDialogService _dialogService;
        private ITokenService _tokenService;

        private string _id;
        private string _password;

        private bool _isEmailValidated;

        public ManualLoginPageViewModel( ITokenService tokenService,INavigationService navigationService,IFitMeetRestService fitMeetRestServices,IPageDialogService dialogService ) : base(navigationService,fitMeetRestServices)
        {
            _dialogService = dialogService;
            _tokenService = tokenService;
        }

        public string Id
        {
            get => _id;
            set => _id = value;
        }
        public string Password
        {
            get => _password;
            set => _password = value;
        }
        public bool IsEmailValidated
        {
            get { return _isEmailValidated; }
            set { _isEmailValidated = value; }
        }
        public DelegateCommand LoginCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    if(IsEmailValidated)
                    {
                        var res = await _fitMeetRestService.ManualLoginAsync(Id.ToLower(),Password);
                        if(res != null)
                        {
                            if(res.Output?.Status == 1 && res.Output?.Response != null)
                            {
                                _tokenService.SetToken(res.Output.Response.Token);
                                NavigateCommand.Execute("app:///MainPage/NavigationPage/MainTabbedPage");
                                return;
                            }
                        }
                        await _dialogService.DisplayAlertAsync("Error",
                            "Usename/password invalid. Or you have not activated your account. Please check your email.",
                            "OK");
                    }
                    else
                    {
                        IsEmailValidated = true;
                        await _dialogService.DisplayAlertAsync("Warning","Please use a correct email address","Ok");
                    }
                });
            }
        }


    }
}
