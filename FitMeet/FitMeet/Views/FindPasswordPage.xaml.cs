using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindPasswordPage:ContentPage
    {
        public FindPasswordPage()
        {
            InitializeComponent();
        }

        private void Entry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateGrid.IsVisible = (!String.IsNullOrEmpty(e.NewTextValue));

        }
    }
}