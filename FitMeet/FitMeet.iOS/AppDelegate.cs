﻿using Facebook.CoreKit;
using FFImageLoading.Forms.Touch;
using Foundation;
using Plugin.Badge.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(TabbedPage),typeof(BadgedTabbedPageRenderer))]
namespace FitMeet.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate:global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app,NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            if(options != null && options.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey))
            {
                var token = string.Empty;

                if(options[UIApplication.LaunchOptionsRemoteNotificationKey] is NSDictionary remoteNotification)
                {
                   
                    NSDictionary aps = remoteNotification.ObjectForKey(new NSString("aps")) as NSDictionary;

                    token =  (aps?[new NSString("reciever_token")]).ToString();


                 
                    //if (message != null && message.ContainsKey(new NSString("reciever_token")))
                    //{
                    //    token = (message[new NSString("reciever_token")] as NSString).ToString();
                    //}
                }
                LoadApplication(new App(new IOSInitializer(),token));
            }
            else
            {
                LoadApplication(new App(new IOSInitializer()));
            }

            UITabBar.Appearance.BarTintColor =
                new UIColor(red: 0.96f,green: 0.96f,blue: 0.96f,alpha: 1.0f);
            UITabBar.Appearance.SelectedImageTintColor =
                new UIColor(red: 0.30f,green: 0.75f,blue: 0.63f,alpha: 1.0f);

            CachedImageRenderer.Init();

            if(UIDevice.CurrentDevice.CheckSystemVersion(8,0))
            {
                var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                    new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
            }

            return base.FinishedLaunching(app,options);
        }
        public override bool OpenUrl(UIApplication application,NSUrl url,string sourceApplication,NSObject annotation)
        {
            // We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
            return ApplicationDelegate.SharedInstance.OpenUrl(application,url,sourceApplication,annotation);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application,NSError error)
        {
            UIAlertController.Create("Error registering push notifications",error.LocalizedDescription,UIAlertControllerStyle.Alert);
        }
        public override void RegisteredForRemoteNotifications(UIApplication application,
            NSData deviceToken)
        {
            // Get current device token
            var DeviceToken = deviceToken.Description;
            if(!string.IsNullOrWhiteSpace(DeviceToken))
            {
                DeviceToken = DeviceToken.Trim('<').Trim('>');
            }

            // Get previous device token
            var oldDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");

            // Has the token changed?
            if(string.IsNullOrEmpty(oldDeviceToken) || !oldDeviceToken.Equals(DeviceToken))
            {
                //TODO: Put your own logic here to notify your server that the device token has changed/been created!
            }

            // Save new device token
            NSUserDefaults.StandardUserDefaults.SetString(DeviceToken,"PushDeviceToken");
        }

        public override void ReceivedRemoteNotification(UIApplication application,NSDictionary userInfo)
        {
            if(UIApplication.SharedApplication.ApplicationState == UIApplicationState.Active)
            {

            }
            else
            {

            }
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
        }
    }


}
