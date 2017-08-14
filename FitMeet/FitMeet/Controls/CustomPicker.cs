using Xamarin.Forms;

namespace FitMeet.Controls
{
    public class CustomPicker : Picker
    {
        /// <summary>
        /// Bindable property for button content padding.
        /// </summary>
        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding) , typeof(Thickness) , typeof(ExtendedButton) , new Thickness(8));


        /// <summary>
        /// Gets or sets the content Padding.
        /// </summary>
        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { this.SetValue(PaddingProperty , value); }
        }

    }
}
