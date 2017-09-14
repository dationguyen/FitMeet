
using Android.App;
using Android.Content;
using Android.Gms.Gcm;
using Android.Media;
using Android.OS;
using Android.Preferences;
using Android.Util;
using Android.Widget;
using FitMeet.Views;
using System;
using Xamarin.Forms;
using Application = Android.App.Application;

namespace FitMeet.Droid
{
    [Service(Exported = false), IntentFilter(new[] { "com.google.android.c2dm.intent.RECEIVE" })]
    public class MyGcmListenerService:GcmListenerService
    {
        private ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);

        public override void OnMessageReceived(string from,Bundle data)
        {
            var userName = data.GetString("user");

            if(MainActivity.IsInForegroundMode)
            {
                Vibrator vibrator = (Vibrator)this.ApplicationContext.GetSystemService(Context.VibratorService);
                vibrator.Vibrate(400);

                string currentPage = String.Empty;
                var actionPage = (MainPage)App.Current.MainPage;
                var navPage = (NavigationPage)actionPage.Detail;
                if(navPage != null)
                {
                    var page = navPage.CurrentPage;
                    if(page is ChatPage)
                    {
                        if(page.Title == userName)
                        {
                            return;
                        }
                    }
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    Toast.MakeText(Android.App.Application.Context,"You have a new message from " + userName,
                        ToastLength.Long).Show();
                });

            }
            else
            {
                //retreive 
                var isEnable = prefs.GetBoolean("IsNotification",true);

                if(isEnable)
                {
                    var message = data.GetString("message");
                    var userId = data.GetString("user_id");
                    var token = data.GetString("reciever_token");

                    Log.Debug("MyGcmListenerService","From:    " + from);
                    Log.Debug("MyGcmListenerService","Message: " + message);
                    SendNotification(message,userId,token,userName);
                }
            }

        }

        void SendNotification(string message,string userId,string token,string userName)
        {
            var intent = new Intent(this,typeof(MainActivity));
            intent.PutExtra("userId",userId);
            intent.PutExtra("token",token);
            intent.PutExtra("userName",userName);
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this,0,intent,PendingIntentFlags.UpdateCurrent);

            var notificationBuilder = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.ic_stat_ic_notification)
                .SetContentText(message)
                .SetStyle(new Notification.BigTextStyle().BigText(message))
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);
            var isSound = prefs.GetBoolean("IsSoundEnable",true);
            var isVibrate = prefs.GetBoolean("IsVibrateEnable",true);
            if(isSound)
            {
                notificationBuilder.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));
            }
            if(isVibrate)
            {
                notificationBuilder.SetVibrate(new long[] { 100,400,100,500 });
            }
            var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Notify(1,notificationBuilder.Build());


        }
    }

}