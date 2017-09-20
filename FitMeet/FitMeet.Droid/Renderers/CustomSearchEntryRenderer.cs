using Android.Graphics;
using Android.Util;
using FitMeet.Controls;
using FitMeet.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSearchEntry),typeof(CustomSearchEntryRenderer))]
namespace FitMeet.Droid.Renderers
{
    public class CustomSearchEntryRenderer:ExtendedEntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                var searchLogo = Resources.GetDrawable(Resource.Drawable.search_logo, null);
                int h = searchLogo.IntrinsicHeight;
                int w = searchLogo.IntrinsicWidth;
                searchLogo.SetColorFilter(Resources.GetColor(Resource.Color.primary,null),PorterDuff.Mode.SrcIn);
                searchLogo.SetBounds(0,0,w,h);

                Control.SetCompoundDrawables(searchLogo,null,null,null);
                Control.SetPadding(20,0,120,0);
                Control.SetTextSize(ComplexUnitType.Dip,14);

            }
        }
    }
}