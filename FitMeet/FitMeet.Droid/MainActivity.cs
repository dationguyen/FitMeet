using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Preferences;
using FFImageLoading.Forms.Droid;
using FitMeet.Droid.Renderers;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;

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

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

          
            callbackManager = CallbackManagerFactory.Create();
           

            
            CachedImageRenderer.Init();
            global::Xamarin.Forms.Forms.Init(this,bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }
    }

    public class AndroidInitializer:IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}

