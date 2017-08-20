using FitMeet.Controls;
using FitMeet.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderedEditor),typeof(BorderedEditorRenderer))]
namespace FitMeet.iOS.Renderers
{
    public class BorderedEditorRenderer:EditorRenderer
    {
        private string Placeholder { get; set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            var element = this.Element as BorderedEditor;

            if(Control != null && element != null)
            {
                Control.Layer.CornerRadius = 8;
                Control.Layer.BorderColor = Color.FromHex("c2c2c2").ToCGColor();
                Control.Layer.BorderWidth = 0.5f;

                Placeholder = element.Placeholder;
                Control.TextColor = UIColor.LightGray;
                Control.Text = Placeholder;

                Control.ShouldBeginEditing += (UITextView textView) =>
                {
                    if(textView.Text == Placeholder)
                    {
                        textView.Text = "";
                        textView.TextColor = UIColor.Black; // Text Color
                    }

                    return true;
                };

                Control.ShouldEndEditing += (UITextView textView) =>
                {
                    if(textView.Text == "")
                    {
                        textView.Text = Placeholder;
                        textView.TextColor = UIColor.LightGray; 
                    }

                    return true;
                };
            }
        }
    }
}