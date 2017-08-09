using FitMeet.Controls;
using FitMeet.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker) , typeof(CustomPickerRenderer))]
namespace FitMeet.iOS.Renderers
{
    public class CustomPickerRenderer : PickerRenderer
    {

        protected override void OnElementChanged( ElementChangedEventArgs<Picker> e )
        {
            base.OnElementChanged(e);
            if ( this.Control == null )
                return;

            Control.Font = UIFont.FromName("MyriadPro-Regular" , 6f);
            Control.MinimumFontSize = 6f;
            Control.TextAlignment = UITextAlignment.Center;
        }
    }
}