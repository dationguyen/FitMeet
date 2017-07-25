using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitMeet.Services;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class NewsDetailPageViewModel : ViewModelBase
    {
        public NewsDetailPageViewModel(INavigationService navigationService, IFitMeetRestService fitMeetRestServices) : base(navigationService, fitMeetRestServices)
        {
            
        }
    }
}
