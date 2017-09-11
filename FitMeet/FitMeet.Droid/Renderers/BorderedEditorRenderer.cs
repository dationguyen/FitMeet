using Android.Util;
using Android.Views;
using FitMeet.Controls;
using FitMeet.Droid.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(BorderedEditor),typeof(BorderedEditorRenderer))]
namespace FitMeet.Droid.Renderers
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
                Control.Background = Resources.GetDrawable(Resource.Drawable.RoundedCornerEntry,null);

                Control.SetPadding((int)element.Padding.Left * 5,(int)element.Padding.Top * 5,(int)element.Padding.Right * 5,(int)element.Padding.Bottom * 5);
                Control.Gravity = GravityFlags.Start;
                Control.SetTextSize(ComplexUnitType.Dip,14);


                Control.SetTextColor(element.TextColor.ToAndroid());

                Control.Hint = element.Placeholder;


            }
        }

        protected override void OnElementPropertyChanged(object sender,PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender,e);


            if(e.PropertyName == BorderedEditor.PlaceholderProperty.PropertyName)
            {
                var element = this.Element as BorderedEditor;
                this.Control.Hint = element?.Placeholder;
            }

        }
    }
}