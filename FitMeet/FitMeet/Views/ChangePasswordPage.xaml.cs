using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage:ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        private void EntPassword_OnTextChanged(object sender,TextChangedEventArgs e)
        {
            ValidateGrid.IsVisible = (!String.IsNullOrEmpty(e.NewTextValue));
            entRePassword.Text = String.Empty;
            confirmPasswordValidator.IsValid = false;
        }

    }
}