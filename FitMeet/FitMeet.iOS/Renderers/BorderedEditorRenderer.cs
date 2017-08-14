using FitMeet.Controls;
using FitMeet.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

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
                Control.Layer.BorderColor = Color.FromHex("c2c2c2").ToCGColor();
                Control.Layer.BorderWidth = 0.5f;
            }
        }
    }
}