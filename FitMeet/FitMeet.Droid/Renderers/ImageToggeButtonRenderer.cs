using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using FitMeet.Controls;
using FitMeet.Droid.Renderers;
using System.ComponentModel;
using System.Net;
using Android.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(ImageToggleButton),typeof(ImageToggeButtonRenderer))]
namespace FitMeet.Droid.Renderers
{
    public class ImageToggeButtonRenderer:ViewRenderer<ImageToggleButton,Android.Widget.ImageButton>
    {
        /// <summary>
        /// Handles the initial drawing of the button.
        /// </summary>
        /// <param name="e">Information on the <see cref="ToggleButton"/>.</param> 
        protected override void OnElementChanged(ElementChangedEventArgs<ImageToggleButton> e)
        {
            base.OnElementChanged(e);

            if(e.OldElement == null)
            {
                if(base.Control == null)
                {
                    Android.Widget.ImageButton button = new Android.Widget.ImageButton(base.Context);
                    button.Touch += ButtonTouched;
                    button.SetBackgroundColor(Android.Graphics.Color.Transparent);
                    button.Tag = this;
                    base.SetNativeControl(button);
                }
            }

            UpdateBackground();
            UpdateImage();
            Control.SetColorFilter(Element.TextColor.ToAndroid());
            Control.SetPadding(12,12,12,12);
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
                UpdateImage();
            }
            if(e.PropertyName == "TextColor")
                Control.SetColorFilter(Element.TextColor.ToAndroid());
            if (e.PropertyName == ImageToggleButton.BackgroundColorProperty.PropertyName)
            {
                UpdateBackground();
            }
        }

        private void UpdateImage()
        {
            var btn = this.Element;
            if(btn?.Image != null)
            {
                var bitmap = GetImageBitmapFromUrl(btn.Image);
                Control.SetImageBitmap(bitmap);
            }
        }

        private void UpdateBackground()
        {
            var btn = this.Element;
            Color backgroundColor = btn?.BackgroundColor ?? Color.Transparent;
            if(backgroundColor != null && backgroundColor != Color.Transparent)
            {
                GradientDrawable drawable = new GradientDrawable();
                drawable.SetColor(backgroundColor.ToAndroid()); 
                drawable.SetCornerRadius(DpToPx(6));
                drawable.SetStroke(1,Color.FromHex("#c6c6c6").ToAndroid());

                Control.Background = drawable;
              
                btn.BackgroundColor = Color.Transparent;
            }
        }

        private int DpToPx(int dp)
        {
            DisplayMetrics metrics = Context.Resources.DisplayMetrics;
            return (int)(dp * metrics.Density);
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using(var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                imageBitmap = BitmapFactory.DecodeByteArray(imageBytes,0,imageBytes.Length);
            }

            return imageBitmap;
        }
        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if(disposing && this.Control != null)
            {
                this.Control.Touch -= ButtonTouched;
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Events

        void ButtonTouched(object sender,TouchEventArgs e)
        {
            if(e.Event.Action == MotionEventActions.Down)
                this.Control.Alpha = (float)this.Element.Opacity / 2;
            else if(e.Event.Action == MotionEventActions.Up)
            {
                this.Control.Alpha = (float)this.Element.Opacity;
                if(this.Element != null)
                {
                    Element.Command?.Execute(Element.CommandParameter);
                    Element.SendClicked();
                }


            }
        }

        #endregion
    }
}