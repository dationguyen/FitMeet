using Xamarin.Forms;

namespace FitMeet.Controls
{
    public class ExtendedButton : Button
    {
        /// <summary>
        /// Bindable property for button content vertical alignment.
        /// </summary>
        public static readonly BindableProperty VerticalContentAlignmentProperty =
            BindableProperty.Create(nameof(VerticalContentAlignment), typeof(TextAlignment), typeof(ExtendedButton), TextAlignment.Center);

        /// <summary>
        /// Bindable property for button content horizontal alignment.
        /// </summary>
        public static readonly BindableProperty HorizontalContentAlignmentProperty =
            BindableProperty.Create(nameof(HorizontalContentAlignment), typeof(TextAlignment), typeof(ExtendedButton), TextAlignment.Center);

        /// <summary>
        /// Bindable property for button content padding.
        /// </summary>
        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(ExtendedButton), new Thickness(8));


        /// <summary>
        /// Gets or sets the content Padding.
        /// </summary>
        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { this.SetValue(PaddingProperty, value); }
        }

        /// <summary>
        /// Gets or sets the content vertical alignment.
        /// </summary>
        public TextAlignment VerticalContentAlignment
        {
            get { return (TextAlignment)GetValue(VerticalContentAlignmentProperty); }
            set { this.SetValue(VerticalContentAlignmentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the content horizontal alignment.
        /// </summary>
        public TextAlignment HorizontalContentAlignment
        {
            get { return (TextAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { this.SetValue(HorizontalContentAlignmentProperty, value); }
        }
    }
}
