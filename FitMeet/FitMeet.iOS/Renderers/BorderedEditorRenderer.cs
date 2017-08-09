using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using FitMeet.iOS.Renderers;
using FitMeet.Controls;

[assembly: ExportRenderer(typeof(BorderedEditor) , typeof(BorderedEditorRenderer))]
namespace FitMeet.iOS.Renderers
{
    public class BorderedEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged( ElementChangedEventArgs<Editor> e )
        {
            base.OnElementChanged(e);

            if ( Control != null )
            {
                Control.Layer.CornerRadius = 8;
                Control.Layer.BorderColor = Color.FromHex("e8e8e8").ToCGColor();
                Control.Layer.BorderWidth = 1;
            }
        }
    }
}