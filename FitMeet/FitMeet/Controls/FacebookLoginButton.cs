using System.Windows.Input;
using Xamarin.Forms;

namespace FitMeet.Controls
{
    public class FacebookLoginButton:View
    {

        // BindableProperty implementation
        public static readonly BindableProperty CompletedCommandProperty =
            BindableProperty.Create(nameof(CompletedCommand),typeof(ICommand),typeof(FacebookLoginButton),null);

        public ICommand CompletedCommand
        {
            get { return (ICommand)GetValue(CompletedCommandProperty); }
            set { SetValue(CompletedCommandProperty,value); }
        }
    }
}
