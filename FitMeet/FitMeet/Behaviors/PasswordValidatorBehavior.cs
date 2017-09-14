using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace FitMeet.Behaviors
{
    public class PasswordValidatorBehavior:Behavior<Entry>
    {
        const string passwordRegex = @"^(?=.*?[a-z]).{6,}$";

        //static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid",typeof(bool),typeof(EmailValidatorBehavior),false);

        //public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
            propertyName: "IsValid",
            returnType: typeof(bool),
            declaringType: typeof(PasswordValidatorBehavior),
            defaultValue: false,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: IsValidPropertyChanged);

        private static void IsValidPropertyChanged(BindableObject bindable,object oldValue,object newValue)
        {
            ((PasswordValidatorBehavior)bindable).IsValid = (bool)newValue;
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

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += HandleTextChanged;
        }



        void HandleTextChanged(object sender,TextChangedEventArgs e)
        {
            if(!String.IsNullOrEmpty(e.NewTextValue))
            {
                IsValid = (Regex.IsMatch(e.NewTextValue,passwordRegex,RegexOptions.IgnoreCase,
                    TimeSpan.FromMilliseconds(250)));
                ((Entry)sender).TextColor = IsValid ? Color.FromHex("#5a5a5a") : Color.FromHex("#f27062");
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= HandleTextChanged;
        }

    }



}
