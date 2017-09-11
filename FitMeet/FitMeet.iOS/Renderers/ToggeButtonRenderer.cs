using FitMeet.Controls;
using FitMeet.iOS.Renderers;
using Foundation;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ToggleButton),typeof(ToggeButtonRenderer))]
namespace FitMeet.iOS.Renderers
{
    public class ToggeButtonRenderer:ButtonRenderer
    {
        /// <summary>
        /// Handles the initial drawing of the button.
        /// </summary>
        /// <param name="e">Information on the <see cref="ToggleButton"/>.</param> 
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            Control.ImageEdgeInsets = new UIEdgeInsets(12,12,12,12);
            if(Element?.Image != null)
            {
                var image = FromUrl(Element.Image);
                if(image != null)
                {
                    Control.SetImage(image,UIControlState.Normal);
                }
                Control.ImageEdgeInsets = new UIEdgeInsets(12,12,12,12);
                Control.TintColor = Control.CurrentTitleColor;
                
            }

        }

        /// <summary>
        /// Called when the underlying model's properties are changed.
        /// </summary>
        /// <param name="sender">Model sending the change event.</param>
        /// <param name="e">Event arguments.</param>
        protected override void OnElementPropertyChanged(object sender,PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender,e);
            if(e.PropertyName == "Image")
            {
                if(Element?.Image != null)
                {
                    var image = FromUrl(Element.Image);
                    if(image != null)
                    {
                        Control.SetImage(image,UIControlState.Normal);
                    }
                }
            }
            if(e.PropertyName == "TextColor")
                Control.TintColor = Control.CurrentTitleColor;
        }

        private UIImage FromUrl(string uri)
        {
            using(var url = new NSUrl(uri))
            using(var data = NSData.FromUrl(url))
            {
                if(data != null)
                    return UIImage.LoadFromData(data);
                else
                {
                    return new UIImage();
                }
            }
        }

    }
}