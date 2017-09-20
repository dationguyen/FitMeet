using Android.OS;
using Android.Text;
using Android.Widget;
using FitMeet.Controls;
using FitMeet.Droid.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HtmlLabel),typeof(HtmlLabelRenderer))]
namespace FitMeet.Droid.Renderers
{
    public class HtmlLabelRenderer:LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if(Control != null && Element != null && !string.IsNullOrWhiteSpace(Element.Text))
            {
                var r = Element.TextColor.R;
                var b = Element.TextColor.B;
                var g = Element.TextColor.G;
                var a = Element.TextColor.A;

                var textColor = string.Format("#{0:X2}{1:X2}{2:X2}",(int)(r * 255.0),(int)(g * 255.0),(int)(b * 255.0));


                var fontName = Element.FontFamily;
                var fontSize = Element.FontSize;
                var htmlContents = "<span style=\"font-family: '" + fontName + "'; color: " + textColor + "; font-size: " + fontSize + "\">" + Element.Text + "</span>";

                Control.SetSingleLine(false);
                Control.SetText(Html.FromHtml(htmlContents,FromHtmlOptions.ModeLegacy),TextView.BufferType.Spannable);
            }
        }

        protected override void OnElementPropertyChanged(object sender,PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender,e);

            if(e.PropertyName == Label.TextProperty.PropertyName)
            {
                if(Control != null && Element != null && !string.IsNullOrWhiteSpace(Element.Text))
                {
                    var r = Element.TextColor.R;
                    var b = Element.TextColor.B;
                    var g = Element.TextColor.G;
                    var a = Element.TextColor.A;

                    var textColor = string.Format("#{0:X2}{1:X2}{2:X2}",(int)(r * 255.0),(int)(g * 255.0),(int)(b * 255.0));


                    var fontName = Element.FontFamily;
                    var fontSize = Element.FontSize;
                    var htmlContents = "<span style=\"font-family: '" + fontName + "'; color: " + textColor + "; font-size: " + fontSize + "\">" + Element.Text + "</span>";

                    if(((int)Build.VERSION.SdkInt) >= 24)
                    {
                        Control.SetText(Html.FromHtml(htmlContents,FromHtmlOptions.ModeLegacy),TextView.BufferType.Spannable);
                    }
                    else
                    {
                        Control.SetText(Html.FromHtml(htmlContents),TextView.BufferType.Spannable);
                    }

                    Control.SetSingleLine(false);

                }
            }
        }


    }
}