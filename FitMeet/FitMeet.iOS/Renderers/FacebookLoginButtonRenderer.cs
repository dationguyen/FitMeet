using CoreGraphics;
using Facebook.LoginKit;
using FitMeet.Controls;
using FitMeet.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FacebookLoginButton),typeof(FacebookLoginButtonRenderer))]
namespace FitMeet.iOS.Renderers
{
    public class FacebookLoginButtonRenderer:ViewRenderer<FacebookLoginButton,LoginButton>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<FacebookLoginButton> e)
        {
            base.OnElementChanged(e);
            var element = Element;
            var control = new LoginButton(new CGRect(0,0,element.WidthRequest,element.HeightRequest))
            {
                LoginBehavior = LoginBehavior.Native,
                ReadPermissions = element.ReadPermissions
            };

            var facebookLoginButtonText = new NSAttributedString("Sign up with Facebook",
                new UIStringAttributes()
                {
                    ForegroundColor = UIColor.White
                });
            control.SetAttributedTitle(facebookLoginButtonText,UIControlState.Normal);
            control.BackgroundColor = UIColor.Gray;

            SetNativeControl(control);
        }

    }


}