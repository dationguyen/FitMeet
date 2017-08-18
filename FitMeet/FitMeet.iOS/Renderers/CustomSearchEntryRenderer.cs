using CoreGraphics;
using FitMeet.Controls;
using FitMeet.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomSearchEntry), typeof(CustomSearchEntryRenderer))]
namespace FitMeet.iOS.Renderers
{
    public class CustomSearchEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var rightImageView = new UIView(new CGRect(0, 0, 40, 0));
                var leftImageView = new UIImageView(UIImage.FromFile("search.png").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate))
                {
                    Frame = new CGRect(0, 0, 40, 28),
                    ContentMode = UIViewContentMode.ScaleAspectFit,
                    TintColor = new UIColor(red: 0.30f, green: 0.75f, blue: 0.63f, alpha: 1.0f),
                };

                Control.RightView = rightImageView;
                Control.RightViewMode = UITextFieldViewMode.Always;


                Control.LeftView = leftImageView;
                Control.LeftViewMode = UITextFieldViewMode.Always;
            }
        }
    }
}