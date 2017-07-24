
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void EntPassword_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            entRePassword.Text = String.Empty;
            confirmPasswordValidator.IsValid = false;
        }
    }
}