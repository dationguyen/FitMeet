
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseNavigationPage : NavigationPage
    {
        public BaseNavigationPage()
        {
            InitializeComponent();
        }
    }
}