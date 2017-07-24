using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    class BaseNavigationPageViewModel : ViewModelBase
    {
        public BaseNavigationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
