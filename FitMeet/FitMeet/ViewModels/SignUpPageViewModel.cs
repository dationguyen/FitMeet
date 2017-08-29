using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace FitMeet.ViewModels
{
    public class SignUpPageViewModel:ViewModelBase
    {
        private IPageDialogService _dialogService;
        private ITokenService _tokenService;

        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsSubscrible { get; set; }

        public DelegateCommand SignUpCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    //Navigate("ThirdSignUpPage");
                    var result = await _fitMeetRestService.EmailSignUpAsync(Email,Password,IsSubscrible,true);
                    if(result != null)
                    {
                        if(result.Output.Status == 1 && result.Output.Response.token != null)
                        {
                            _tokenService.SetToken(result.Output.Response.token);
                            Navigate("SecondSignUpPage");
                        }
                        else
                        {
                            await _dialogService.DisplayAlertAsync("Error",result.Output.Validation,"Ok");
                        }
                    }
                    else
                    {
                        await _dialogService.DisplayAlertAsync("Error","There are some error, Please try again later","Ok");
                    }
                });
            }
        }

        public SignUpPageViewModel(INavigationService navigationService,ITokenService tokenService,
            IPageDialogService dialogService,IFitMeetRestService fitMeetRestServices) : base(navigationService,fitMeetRestServices)
        {
            _dialogService = dialogService;
            _tokenService = tokenService;
        }

    }
}
