using FitMeet.Controls;
using FitMeet.iOS.Renderers;
using System;
using System.ComponentModel;
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

                Control.TextContainerInset = new UIEdgeInsets(
                    (int)element.Padding.Top,
                    (int)element.Padding.Left,
                    (int)element.Padding.Bottom,
                    (int)element.Padding.Right
                    );

                Control.TextColor = element.TextColor.ToUIColor();

                Placeholder = element.Placeholder;
                if(String.IsNullOrEmpty(element.Text))
                {
                    Control.Text = Placeholder;
                    Control.TextColor = UIColor.LightGray;
                }
                Control.ShouldBeginEditing += (UITextView textView) =>
                {
                    if(textView.Text == Placeholder)
                    {
                        textView.Text = "";
                        //textView.TextColor = UIColor.Black; // Text Color
                    }

                    return true;
                };

                Control.ShouldEndEditing += (UITextView textView) =>
                {
                    if(textView.Text == "")
                    {
                        textView.Text = Placeholder;
                        //textView.TextColor = UIColor.LightGray;
                    }

                    return true;
                };
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var element = this.Element as BorderedEditor;

            if (Control != null && element != null)
            {
                if (e.PropertyName == Editor.TextProperty.PropertyName)
                {
                    if (element.Text == Placeholder)
                    {
                        Control.TextColor = Color.FromHex("7e7e7e").ToUIColor();
                    }
                    else
                    {
                        Control.TextColor = element.TextColor.ToUIColor();
                    }
                }
            }

        }
    }
}