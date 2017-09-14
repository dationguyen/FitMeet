using Android.App;
using Android.Content;
using Android.Gms.Gcm;
using Android.Gms.Iid;
using Android.Util;
using System;

namespace FitMeet.Droid.IntentServices
{
    [Service(Exported = false)]
    class RegistrationIntentService:IntentService
    {
        static object locker = new object();

        public RegistrationIntentService() : base("RegistrationIntentService") { }

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                Log.Info("RegistrationIntentService","Calling InstanceID.GetToken");
                lock(locker)
                {
                    var instanceID = InstanceID.GetInstance(this);
                    var token = instanceID.GetToken(
                        "574996532181",GoogleCloudMessaging.InstanceIdScope,null);

                    Log.Info("RegistrationIntentService","GCM Registration Token: " + token);
                    SendRegistrationToAppServer(token);
                    Subscribe(token);
                }
            }
            catch(Exception e)
            {
                Log.Debug("RegistrationIntentService","Failed to get a registration token");
                return;
            }
        }

        void SendRegistrationToAppServer(string token)
        {
            //--SAVE Data
            var prefs = Application.Context.GetSharedPreferences("Fitmeet",FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutString("token",token);
            prefEditor.Apply();
        }

        void Subscribe(string token)
        {
            var pubSub = GcmPubSub.GetInstance(this);
            pubSub.Subscribe(token,"/topics/global",null);
        }
    }
}