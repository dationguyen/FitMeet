using Android.Text;
using FitMeet.Controls;
using FitMeet.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLabel),typeof(CustomLabelRenderer))]
namespace FitMeet.Droid.Renderers
{
    public class CustomLabelRenderer:LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            var element = this.Element as CustomLabel;

            if(element == null || this.Control == null)
            {
                return;
            }
        
            Control.SetSingleLine(false);
            Control.SetMaxLines(element.MaxLine);
        }
    }
}
