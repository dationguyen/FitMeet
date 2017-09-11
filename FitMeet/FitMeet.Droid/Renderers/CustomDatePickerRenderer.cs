using Android.Util;
using Android.Views;
using FitMeet.Controls;
using FitMeet.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDatePicker),typeof(CustomDatePickerRenderer))]
namespace FitMeet.Droid.Renderers
{
    public class CustomDatePickerRenderer:DatePickerRenderer
    {
        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <value>The element.</value>
        public new CustomDatePicker Element
        {
            get
            {
                return base.Element as CustomDatePicker;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            var element = this.Element;

            if(element == null || this.Control == null)
            {
                return;
            }

            Control.Background = Resources.GetDrawable(Resource.Drawable.RoundedCornerEntry,null);
            Control.SetPadding(20,0,20,0);
            Control.Gravity = GravityFlags.CenterVertical;
            Control.SetTextSize(ComplexUnitType.Dip,14);
        }
    }
}