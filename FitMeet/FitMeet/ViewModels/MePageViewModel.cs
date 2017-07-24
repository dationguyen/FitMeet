using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class MePageViewModel : ViewModelBase
    {
        public MePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Me";
        }
    }
}
