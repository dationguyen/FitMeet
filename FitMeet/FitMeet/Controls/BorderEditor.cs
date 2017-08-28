using System;
using Xamarin.Forms;

namespace FitMeet.Controls
{
    public class BorderedEditor:Editor
    {
        #region BindableProperty

        //BindableProperties
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(
        propertyName: "Placeholder",
            returnType: typeof(string),
            declaringType: typeof(BorderedEditor),
            defaultValue: String.Empty,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: PlaceholderValueChanged);

        /// <summary>
        /// Bindable property for button content padding.
        /// </summary>
        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding),typeof(Thickness),typeof(ExtendedButton),new Thickness(8));


        //Methods
        private static void PlaceholderValueChanged(BindableObject bindable,object oldValue,object newValue)
        {
            ((BorderedEditor)bindable).Placeholder = (string)newValue;
        }


        //Properties
        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }

            set
            {
                SetValue(PlaceholderProperty,value);
            }
        }

        /// <summary>
        /// Gets or sets the content Padding.
        /// </summary>
        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { this.SetValue(PaddingProperty,value); }
        }
        #endregion
    }
}
