using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Text.RegularExpressions;

namespace FitMeet.ViewModels
{
    public class FindPasswordPageViewModel:ViewModelBase
    {
        private readonly IPageDialogService _dialogService;

        private string _email;
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";


        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                FindPasswordCommand?.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand FindPasswordCommand { get; set; }

        public FindPasswordPageViewModel(IPageDialogService dialogService,INavigationService navigationService,IFitMeetRestService fitMeetRestService) : base(navigationService,fitMeetRestService)
        {
            _dialogService = dialogService;
            FindPasswordCommand = new DelegateCommand(FindPassword,CanExecuteFindPassword);
        }


        private bool CanExecuteFindPassword()
        {
            if(!String.IsNullOrEmpty(Email))
            {
                var isValid = (Regex.IsMatch(Email,emailRegex,RegexOptions.IgnoreCase,
                    TimeSpan.FromMilliseconds(250)));
                return isValid;
            }
            return false;
        }

        private async void FindPassword()
        {
            var isSuccess = await _fitMeetRestService.ResetPasswordAsync(Email);
            if(isSuccess)
            {
                await _dialogService.DisplayAlertAsync("Success","Please check your email","Go back to login page");
                NavigateBackCommand?.Execute();
            }
            else
            {
                await _dialogService.DisplayAlertAsync("Error","There are some problem with our server, please try again later","Ok");
            }
        }
    }
}
