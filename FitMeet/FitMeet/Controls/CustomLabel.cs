using Xamarin.Forms;

namespace FitMeet.Controls
{
    public class CustomLabel:Label
    {
        /// <summary>
        /// Bindable property for button content padding.
        /// </summary>
        public static readonly BindableProperty MaxLineProperty =
            BindableProperty.Create(nameof(MaxLine),typeof(int),typeof(CustomLabel),-1,BindingMode.OneWay);

        //private static void MaxLinePropertyChanged(BindableObject bindable,object oldValue,object newValue)
        //{
        //    if(oldValue != newValue )
        //        ((CustomLabel)bindable).MaxLine = (int)newValue;
        //}


        /// <summary>
        /// Gets or sets the content Max Line.
        /// </summary>
        public int MaxLine
        {
            get { return (int)GetValue(MaxLineProperty); }
            set { this.SetValue(MaxLineProperty,value); }
        }

    }
}
