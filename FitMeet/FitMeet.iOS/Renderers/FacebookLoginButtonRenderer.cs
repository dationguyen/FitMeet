using CoreGraphics;
using Facebook.LoginKit;
using FitMeet.Controls;
using FitMeet.iOS.Renderers;
using Foundation;
using System.Diagnostics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FacebookLoginButton),typeof(FacebookLoginButtonRenderer))]
namespace FitMeet.iOS.Renderers
{
    public class FacebookLoginButtonRenderer:ViewRenderer<FacebookLoginButton,LoginButton>
    {
        string[] readPermissions = { "public_profile","email" };

        protected override void OnElementChanged(ElementChangedEventArgs<FacebookLoginButton> e)
        {
            base.OnElementChanged(e);
            var element = (FacebookLoginButton)Element;
            if(element == null)
            {
                return;
            }
            
            var control = new LoginButton(new CGRect(0,0,element.WidthRequest,element.HeightRequest))
            {
                LoginBehavior = LoginBehavior.Native,
                ReadPermissions = readPermissions
            };

            var facebookLoginButtonText = new NSAttributedString("Sign up with Facebook",
                new UIStringAttributes()
                {
                    ForegroundColor = UIColor.White
                });
            control.SetAttributedTitle(facebookLoginButtonText,UIControlState.Normal);
            control.BackgroundColor = UIColor.Gray;
            control.Completed += ControlOnCompleted;
            
            SetNativeControl(control);
        }

        private void ControlOnCompleted(object sender,LoginButtonCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                Debug.WriteLine(e.Error);
                return;
            }

            if(e.Result.IsCancelled)
            {
                Debug.WriteLine("Canceled");
                return;
            }

            var token = e.Result.Token.TokenString;

            var element = Element;
            if(element.CompletedCommand != null && element.CompletedCommand.CanExecute(token))
            {
                element.CompletedCommand.Execute(token);
            }

        }
    }
}