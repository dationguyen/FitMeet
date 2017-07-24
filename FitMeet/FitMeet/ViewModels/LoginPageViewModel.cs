using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private IRestApiService _restApiService;

        public LoginPageViewModel(INavigationService navigationService, IRestApiService restApiServices) : base(navigationService)
        {
            _restApiService = restApiServices;
        }
    }
}
