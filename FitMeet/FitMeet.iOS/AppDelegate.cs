
using Facebook.CoreKit;
using Foundation;
using Microsoft.Practices.Unity;
using Prism.Unity;
using UIKit;

namespace FitMeet.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate:global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        string appId = "306989955995811";
        string appName = "Roor";

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app,NSDictionary options)
        {
            //Profile.EnableUpdatesOnAccessTokenChange(true);
            //Settings.AppID = appId;
            //Settings.DisplayName = appName;

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App(new IOSInitializer()));
            UITabBar.Appearance.BarTintColor =
                new UIColor(red: 0.96f,green: 0.96f,blue: 0.96f,alpha: 1.0f);
            UITabBar.Appearance.SelectedImageTintColor =
                new UIColor(red: 0.30f,green: 0.75f,blue: 0.63f,alpha: 1.0f);

            return ApplicationDelegate.SharedInstance.FinishedLaunching(app,options);
        }
        public override bool OpenUrl(UIApplication application,NSUrl url,string sourceApplication,NSObject annotation)
        {
            // We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
            return ApplicationDelegate.SharedInstance.OpenUrl(application,url,sourceApplication,annotation);
        }
    }

    public class IOSInitializer:IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }

}
