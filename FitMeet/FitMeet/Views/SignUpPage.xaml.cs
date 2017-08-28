
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage:ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            SignUpButton.IsEnabled = false;
        }

        private void EntPassword_OnTextChanged(object sender,TextChangedEventArgs e)
        {
            entRePassword.Text = String.Empty;
            confirmPasswordValidator.IsValid = false;
            UpdateButtonStatus();
        }

        private void UpdateButtonStatus()
        {
            if(emailValidator.IsValid && passwordValidator.IsValid && confirmPasswordValidator.IsValid &&
                ConfirmCheckBox.Checked)
            {
                SignUpButton.IsEnabled = true;
            }
            else
            {
                SignUpButton.IsEnabled = false;
            }
        }

        private void ConfirmCheckBox_OnCheckedChanged(object sender,EventArgs e)
        {
            UpdateButtonStatus();
        }

        private void Entry_OnCompleted(object sender, EventArgs e)
        {
            UpdateButtonStatus();
        }
    }
}