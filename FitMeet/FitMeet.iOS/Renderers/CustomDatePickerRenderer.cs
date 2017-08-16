using FitMeet.Controls;
using FitMeet.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePicker) , typeof(CustomDatePickerRenderer))]
namespace FitMeet.iOS.Renderers
{
    public class CustomDatePickerRenderer : DatePickerRenderer
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

        protected override void OnElementChanged( ElementChangedEventArgs<DatePicker> e )
        {
            base.OnElementChanged(e);
            var element = this.Element;

            if ( element == null || this.Control == null )
            {
                return;
            }

            Control.Font = UIFont.FromName("MyriadPro-Regular" , 14f);
            Control.TextAlignment = UITextAlignment.Left;
            //Control.LeftView = new UIView(new CGRect(0 , 0 , (float)element.Padding.Left , 0));
            //Control.LeftViewMode = UITextFieldViewMode.Always;
        }
    }
}