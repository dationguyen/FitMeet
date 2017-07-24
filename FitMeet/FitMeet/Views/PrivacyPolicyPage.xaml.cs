
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivacyPolicyPage : ContentPage
    {
        public PrivacyPolicyPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Back");
        }
    }
}