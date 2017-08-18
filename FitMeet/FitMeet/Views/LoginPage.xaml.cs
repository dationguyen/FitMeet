using Xamarin.Forms;
using Xamarin.Forms.Xaml;
#if __IOS__
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;
using Facebook.LoginKit;
using Facebook.CoreKit;
#endif
namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage:ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}