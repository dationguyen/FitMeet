using Android.Util;
using Android.Views;
using FitMeet.Controls;
using FitMeet.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker),typeof(CustomPickerRenderer))]
namespace FitMeet.Droid.Renderers
{
    public class CustomPickerRenderer:PickerRenderer
    {
        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <value>The element.</value>
        public new CustomPicker Element
        {
            get
            {
                return base.Element as CustomPicker;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            var element = this.Element;

            if(element == null || this.Control == null)
            {
                return;
            }
            Control.Background = Resources.GetDrawable(Resource.Drawable.RoundedCornerEntry,null);
            Control.SetPadding(DpToPx((int)element.Padding.Left),0,0,0);
            Control.Gravity = GravityFlags.CenterVertical;

            Control.SetTextColor(element.TextColor.ToAndroid());
            Control.SetTextSize(ComplexUnitType.Dip,14);
        }
        private int DpToPx(int dp)
        {
            DisplayMetrics metrics = Context.Resources.DisplayMetrics;
            return (int)(dp * metrics.Density);
        }
    }
}