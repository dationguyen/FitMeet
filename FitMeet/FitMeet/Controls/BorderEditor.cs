using System;
using Xamarin.Forms;

namespace FitMeet.Controls
{
    public class BorderedEditor:Editor
    {
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create<BorderedEditor,string>(view => view.Placeholder,String.Empty);

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
    }
}
