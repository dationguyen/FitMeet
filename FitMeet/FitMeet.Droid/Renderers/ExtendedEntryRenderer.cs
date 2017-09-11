using Android.Util;
using Android.Views;
using FitMeet.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TextAlignment = Android.Views.TextAlignment;

[assembly: ExportRenderer(typeof(Entry),typeof(ExtendedEntryRenderer))]
namespace FitMeet.Droid.Renderers
{
    public class ExtendedEntryRenderer:EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if(Control != null)
            {
                Control.Background = Resources.GetDrawable(Resource.Drawable.RoundedCornerEntry,null);
                Control.SetPadding(20,0,20,0);
                Control.Gravity = GravityFlags.CenterVertical;
            }
        }
    }
}