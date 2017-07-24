﻿using Xamarin.Forms;

namespace FitMeet.Behaviors
{
    public class ConfirmPasswordBehavior : Behavior<Entry>
    {
        const string passwordRegex = @"^(?=.*?[a-z]).{6,}$";

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public static readonly BindableProperty CompareToEntryProperty = BindableProperty.Create("CompareToEntry",
            typeof(Entry), typeof(ConfirmPasswordBehavior), null);



        public Entry CompareToEntry
        {
            get { return (Entry)base.GetValue(CompareToEntryProperty); }
            set { base.SetValue(CompareToEntryProperty, value); }
        }

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            set { base.SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += HandleTextChanged;
        }



        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            var password = CompareToEntry.Text;
            var confirmPassword = e.NewTextValue;
            if (password != null)
                IsValid = password.Equals(confirmPassword);
            ((Entry)sender).TextColor = IsValid ? Color.FromHex("#5a5a5a") : Color.FromHex("#f27062");
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= HandleTextChanged;
        }

    }



}
