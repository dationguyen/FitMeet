using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace FitMeet.Behaviors
{
    public class EmailValidatorBehavior:Behavior<Entry>
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";


        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
        propertyName: "IsValid",
        returnType: typeof(bool),
        declaringType: typeof(EmailValidatorBehavior),
        defaultValue: false,
        defaultBindingMode: BindingMode.OneWay,
        propertyChanged: IsValidPropertyChanged);

        private static void IsValidPropertyChanged( BindableObject bindable,object oldValue,object newValue )
        {
            ((EmailValidatorBehavior)bindable).IsValid = (bool)newValue;
        }


        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set
            {
                SetValue(IsValidProperty,value);
                OnPropertyChanged("IsValid");
            }
        }

        protected override void OnAttachedTo( Entry bindable )
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += HandleTextChanged;
        }



        void HandleTextChanged( object sender,TextChangedEventArgs e )
        {
            if(!String.IsNullOrEmpty(e.NewTextValue))
            {
                IsValid = (Regex.IsMatch(e.NewTextValue,emailRegex,RegexOptions.IgnoreCase,
                    TimeSpan.FromMilliseconds(250)));
                ((Entry)sender).TextColor = IsValid ? Color.FromHex("#5a5a5a") : Color.FromHex("#f27062");
            }
        }

        protected override void OnDetachingFrom( Entry bindable )
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= HandleTextChanged;
        }

    }



}
