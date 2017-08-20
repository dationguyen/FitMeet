using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class MessagePageViewModel:ViewModelBase
    {
        public MessagePageViewModel(INavigationService navigationService,IFitMeetRestService fitMeetRestServices) : base(navigationService,fitMeetRestServices)
        {
        }

    }
}
