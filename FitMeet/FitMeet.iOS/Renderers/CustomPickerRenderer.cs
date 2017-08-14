using CoreGraphics;
using FitMeet.Controls;
using FitMeet.iOS.Extensions;
using FitMeet.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker) , typeof(CustomPickerRenderer))]
namespace FitMeet.iOS.Renderers
{
    public class CustomPickerRenderer : PickerRenderer
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

        protected override void OnElementChanged( ElementChangedEventArgs<Picker> e )
        {
            base.OnElementChanged(e);
            var element = this.Element;

            if ( element == null || this.Control == null )
            {
                return;
            }

            Control.Font = UIFont.FromName("MyriadPro-Regular" , 14f);
            Control.TextAlignment = UITextAlignment.Left;
            Control.LeftView = new UIView(new CGRect(0 , 0 , (float)element.Padding.Left , 0));
            Control.LeftViewMode = UITextFieldViewMode.Always;
        }
    }
}