using System;
using Xamarin.Forms;

namespace FitMeet.Controls
{
    public class ToggleButton : Button
    {
        public Color UncheckedBackgroundColor { get; set; } = Color.White;
        public Color CheckedBackgroundColor { get; set; } = Color.FromHex("#4cbea0");
        public Color UncheckedTextColor { get; set; } = Color.FromHex("#5a5a5a");
        public Color CheckedTextColor { get; set; } = Color.White;


        public static BindableProperty CheckedProperty = BindableProperty.Create(
            propertyName: "Checked",
            returnType: typeof(Boolean?),
            declaringType: typeof(CheckBox),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: CheckedValueChanged);

        public Boolean? Checked
        {
            get
            {
                if (GetValue(CheckedProperty) == null)
                    return false;
                return (Boolean)GetValue(CheckedProperty);
            }
            set
            {
                SetValue(CheckedProperty, value);
                OnPropertyChanged();
                RaiseCheckedChanged();
            }
        }
        private static void CheckedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null && (Boolean)newValue == true)
            {
                ((ToggleButton)bindable).BackgroundColor = ((ToggleButton)bindable).CheckedBackgroundColor;
                ((ToggleButton)bindable).TextColor = ((ToggleButton)bindable).CheckedTextColor;
            }
            else
            {
                ((ToggleButton)bindable).BackgroundColor = ((ToggleButton)bindable).UncheckedBackgroundColor;
                ((ToggleButton)bindable).TextColor = ((ToggleButton)bindable).UncheckedTextColor;
            }
        }

        public event EventHandler CheckedChanged;
        private void RaiseCheckedChanged()
        {
            CheckedChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}
