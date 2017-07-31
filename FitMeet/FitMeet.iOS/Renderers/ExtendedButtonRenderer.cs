using FitMeet.Controls;
using FitMeet.iOS.Extensions;
using FitMeet.iOS.Renderers;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedButton), typeof(ExtendedButtonRenderer))]
namespace FitMeet.iOS.Renderers
{
    /// <summary>
    /// Class ExtendedButtonRenderer.
    /// </summary>
    public class ExtendedButtonRenderer : ButtonRenderer
    {
        /// <summary>
        /// Called when [element changed].
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            var element = this.Element;

            if (element == null || this.Control == null)
            {
                return;
            }
            this.Control.ContentEdgeInsets = new UIEdgeInsets(
                (int)element.Padding.Top,
                (int)element.Padding.Left,
                (int)element.Padding.Bottom,
                (int)element.Padding.Right
            );
            this.Control.VerticalAlignment = this.Element.VerticalContentAlignment.ToContentVerticalAlignment();
            this.Control.HorizontalAlignment = this.Element.HorizontalContentAlignment.ToContentHorizontalAlignment();
        }

        /// <summary>
        /// Handles the <see cref="E:ElementPropertyChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "VerticalContentAlignment":
                    this.Control.VerticalAlignment = this.Element.VerticalContentAlignment.ToContentVerticalAlignment();
                    break;
                case "HorizontalContentAlignment":
                    this.Control.HorizontalAlignment = this.Element.HorizontalContentAlignment.ToContentHorizontalAlignment();
                    break;
                case "Padding":
                    this.Control.ContentEdgeInsets = new UIEdgeInsets(
                        (int)Element?.Padding.Top,
                        (int)Element?.Padding.Left,
                        (int)Element?.Padding.Bottom,
                        (int)Element?.Padding.Right
                    );
                    break;
                default:
                    base.OnElementPropertyChanged(sender, e);
                    break;
            }
        }

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <value>The element.</value>
        public new ExtendedButton Element
        {
            get
            {
                return base.Element as ExtendedButton;
            }
        }
    }
}