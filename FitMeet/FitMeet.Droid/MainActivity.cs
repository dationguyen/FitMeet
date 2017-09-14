using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using FFImageLoading.Forms.Droid;
using FitMeet.Droid.IntentServices;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin.Facebook;

[assembly: MetaData("com.facebook.sdk.ApplicationId",Value = "@string/app_id")]
[assembly: MetaData("com.facebook.sdk.ApplicationName",Value = "@string/app_name")]

[assembly: Permission(Name = Android.Manifest.Permission.Internet)]
[assembly: Permission(Name = Android.Manifest.Permission.WriteExternalStorage)]
namespace FitMeet.Droid
{
    [Activity(Label = "FitMeet",Name = "au.com.fitmeet.MainActivity",Icon = "@drawable/icon",MainLauncher = true,ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,Theme = "@style/MyTheme")]
    public class MainActivity:global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static ICallbackManager callbackManager;
        public static bool IsInForegroundMode;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            CachedImageRenderer.Init();
            global::Xamarin.Forms.Forms.Init(this,bundle);

            if(callbackManager == null)
                callbackManager = CallbackManagerFactory.Create();

            if(IsPlayServicesAvailable())
            {
                var intent = new Intent(this,typeof(RegistrationIntentService));
                StartService(intent);
            }

            var userId = Intent.GetStringExtra("userId");
            var token = Intent.GetStringExtra("token");
            var userName = Intent.GetStringExtra("userName");

            LoadApplication(new App(new AndroidInitializer(),userId,token,userName));

        }

        protected override void OnActivityResult(int requestCode,Result resultCode,Intent data)
        {
            base.OnActivityResult(requestCode,resultCode,data);
            callbackManager.OnActivityResult(requestCode,(int)resultCode,data);
        }
        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if(resultCode != ConnectionResult.Success)
            {
                if(GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    System.Console.WriteLine(GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    System.Console.WriteLine("Sorry, this device is not supported");
                    Finish();
                }
                return false;
            }
            else
            {
                System.Console.WriteLine("Google Play Services is available.");
                return true;
            }
        }


        protected override void OnPause()
        {
            base.OnPause();
            IsInForegroundMode = false;
        }


        protected override void OnResume()
        {
            base.OnResume();
            IsInForegroundMode = true;
        }
    }

    public class AndroidInitializer:IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}

