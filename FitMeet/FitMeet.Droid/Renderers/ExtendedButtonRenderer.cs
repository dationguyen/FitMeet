
using Android.Util;
using FitMeet.Controls;
using FitMeet.Droid.Extensions;
using FitMeet.Droid.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedButton),typeof(ExtendedButtonRenderer))]
namespace FitMeet.Droid.Renderers
{
    public class ExtendedButtonRenderer:ButtonRenderer
    {
        /// <summary>
        /// Called when [element changed].
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            UpdateAlignment();
            UpdateFont();
            UpdatePadding();
        }

        /// <summary>
        /// Handles the <see cref="E:ElementPropertyChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnElementPropertyChanged(object sender,PropertyChangedEventArgs e)
        {
            if(e.PropertyName == ExtendedButton.VerticalContentAlignmentProperty.PropertyName ||
               e.PropertyName == ExtendedButton.HorizontalContentAlignmentProperty.PropertyName)
            {
                UpdateAlignment();
            }
            else if(e.PropertyName == Xamarin.Forms.Button.FontProperty.PropertyName)
            {
                UpdateFont();
            }
            else if(e.PropertyName == ExtendedButton.PaddingProperty.PropertyName)
            {
                UpdatePadding();
            }
            base.OnElementPropertyChanged(sender,e);
        }

        private int DpToPx(int dp)
        {
            DisplayMetrics metrics = Context.Resources.DisplayMetrics;
            return (int)(dp * metrics.Density);
        }
        private void UpdatePadding()
        {
            if(Element is ExtendedButton element)
            {
                Control.SetPadding(
                    DpToPx((int)element.Padding.Left),
                    DpToPx((int)element.Padding.Top),
                    DpToPx((int)element.Padding.Right),
                    DpToPx((int)element.Padding.Bottom));
            }
        }

        /// <summary>
        /// Updates the font
        /// </summary>
        private void UpdateFont()
        {
            Control.Typeface = Element.Font.ToExtendedTypeface(Context);
        }

        /// <summary>
        /// Sets the alignment.
        /// </summary>
        private void UpdateAlignment()
        {
            var element = this.Element as ExtendedButton;

            if(element == null || this.Control == null)
            {
                return;
            }

            this.Control.Gravity = element.VerticalContentAlignment.ToDroidVerticalGravity() |
                                   element.HorizontalContentAlignment.ToDroidHorizontalGravity();
        }
    }
}