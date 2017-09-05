using FitMeet.Controls;
using FitMeet.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(CustomLabel),typeof(CustomLabelRenderer))]
namespace FitMeet.iOS.Renderers
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

            Control.Lines = element.MaxLine;
        }
    }
}
