using System;
using FitMeet.Services;
using FitMeet.Services.DependencyServices;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace FitMeet.ViewModels
{
    public class SettingPageViewModel:ViewModelBase
    {
        private ISettings AppSettings =>
            CrossSettings.Current;

        private IDependencyService _dependencyService;

        private const string NotificationEnableKey = "IsNotification";
        private const string SoundEnableKey = "IsSoundEnable";
        private const string VibrateEnableKey = "IsVibrateEnable";

        private bool _isNotificationEnable;
        private bool _isSoundEnable;
        private bool _isVibrateEnable;
        private bool _hasFacebook;
        private readonly IPageDialogService _dialogService;

        public bool IsNotificationEnable
        {
            get => _isNotificationEnable;
            set
            {
                if(value != _isNotificationEnable)
                {
                    SetProperty(ref _isNotificationEnable,value);
                    AppSettings.AddOrUpdateValue(NotificationEnableKey,value);
                }

            }
        }

        public bool IsSoundEnable
        {
            get => _isSoundEnable;
            set
            {
                if(value != _isSoundEnable)
                {
                    SetProperty(ref _isSoundEnable,value);
                    AppSettings.AddOrUpdateValue(SoundEnableKey,value);
                }
            }
        }

        public bool IsVibrateEnable
        {
            get => _isVibrateEnable;
            set
            {
                if(value != _isVibrateEnable)
                {
                    SetProperty(ref _isVibrateEnable,value);
                    AppSettings.AddOrUpdateValue(VibrateEnableKey,value);
                }
            }
        }

        public bool HasFacebook
        {
            get => _hasFacebook;
            set => SetProperty(ref _hasFacebook,value);
        }

        public DelegateCommand ConnectWithFacebookCommand { get; set; }
        public DelegateCommand DeleteAccountCommand { get; set; }

        public SettingPageViewModel(IPageDialogService dialogService,IDependencyService dependencyService,INavigationService navigationService,IFitMeetRestService fitMeetRestServices) : base(navigationService,fitMeetRestServices)
        {
            _dialogService = dialogService;
            _dependencyService = dependencyService;
            ConnectWithFacebookCommand = new DelegateCommand(ConnectFacebook);
            DeleteAccountCommand = new DelegateCommand(DeleteAccount);
        }

        private async void DeleteAccount()
        {
            var accept = await _dialogService.DisplayAlertAsync("Delete", "Are you sure to delete your account?", "Delete", "Cancel");
            if (accept)
            {

            }

        }

        private async void ConnectFacebook()
        {
            var userId = await _dependencyService.Get<IFacebookLoginService>().LoginAsync();
            if(!string.IsNullOrEmpty(userId))
            {
                var isSuccess = await _fitMeetRestService.ConnectFacebookAsync(userId);
                if(isSuccess)
                {
                    await _dialogService.DisplayAlertAsync("Success","You have connected to facebook","Ok");
                    _fitMeetRestService.HasFacebook = false;
                    NavigateBackCommand?.Execute();
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("Error","There are some problem with our server, please try again later","Ok");
                }
            }


        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            IsNotificationEnable = AppSettings.GetValueOrDefault(NotificationEnableKey,true);
            IsSoundEnable = AppSettings.GetValueOrDefault(SoundEnableKey,true);
            IsVibrateEnable = AppSettings.GetValueOrDefault(VibrateEnableKey,true);

            HasFacebook = _fitMeetRestService.HasFacebook;
        }
    }
}
