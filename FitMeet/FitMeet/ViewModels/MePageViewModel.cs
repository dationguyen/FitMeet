using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class MePageViewModel : ViewModelBase
    {
        public MePageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestServices) : base(navigationService, fitMeetRestServices)
        {
            Title = "Me";
        }
    }
}
