using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading.Forms.Droid;
using Microsoft.Practices.Unity;
using Prism.Unity;
[assembly: MetaData("com.facebook.sdk.ApplicationId",Value = "@string/app_id")]
namespace FitMeet.Droid
{
    [Activity(Label = "FitMeet",Icon = "@drawable/icon",MainLauncher = true,ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,Theme = "@style/MyTheme")]
    public class MainActivity:global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            CachedImageRenderer.Init();
            global::Xamarin.Forms.Forms.Init(this,bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer:IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}

