using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace FitMeet.ViewModels
{
    public class ChangePasswordPageViewModel:ViewModelBase
    {
        private IPageDialogService _dialogService;
        private bool _isValidPassword;
        private bool _isConfirmPassword;

        public ChangePasswordPageViewModel(IPageDialogService dialogService,INavigationService navigationService,IFitMeetRestService fitMeetRestService) : base(navigationService,fitMeetRestService)
        {
            _dialogService = dialogService;
            ChangePasswordCommand = new DelegateCommand(ChangePassword,CanExecuteChangePassword);
        }

        private bool CanExecuteChangePassword()
        {
            return (IsValidPassword && IsConfirmPassword);
        }

        private async void ChangePassword()
        {
            var isSuccess = await _fitMeetRestService.ChangePasswordAsync(Password);
            if(isSuccess)
            {
                await _dialogService.DisplayAlertAsync("Success","Your password has changed","Ok");
                NavigateBackCommand?.Execute();
            }
            else
            {
                await _dialogService.DisplayAlertAsync("Error","There are some problem with our server, please try again later","Ok");
            }
        }

        public DelegateCommand ChangePasswordCommand { get; set; }

        public string Password { get; set; }

        public bool IsValidPassword
        {
            get => _isValidPassword;
            set
            {
                _isValidPassword = value;
                ChangePasswordCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsConfirmPassword
        {
            get => _isConfirmPassword;
            set
            {
                _isConfirmPassword = value;
                ChangePasswordCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
