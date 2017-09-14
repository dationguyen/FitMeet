using Android.App;
using Android.Content;
using Android.Gms.Iid;
using FitMeet.Droid.IntentServices;

namespace FitMeet.Droid
{
    [Service(Exported = false), IntentFilter(new[] { "com.google.android.gms.iid.InstanceID" })]
    class MyInstanceIDListenerService:InstanceIDListenerService
    {
        public override void OnTokenRefresh()
        {
            var intent = new Intent(this,typeof(RegistrationIntentService));
            StartService(intent);
        }
    }
}