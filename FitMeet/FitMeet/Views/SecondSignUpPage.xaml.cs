
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondSignUpPage:ContentPage
    {
        public SecondSignUpPage()
        {
            InitializeComponent();
        }

        private void ListView_OnItemSelected(object sender,SelectedItemChangedEventArgs e)
        {
            AddressEntry.Unfocus();
        }

        private void AddressEntry_OnUnfocused(object sender,FocusEventArgs e)
        {
            AutoCompleteGrid.IsVisible = false;
        }


        private async void AddressEntry_OnFocused(object sender,FocusEventArgs e)
        {
            AutoCompleteGrid.IsVisible = true;
            await ScrollView.ScrollToAsync(AddressLabel,ScrollToPosition.Start,false);
        }

        private async void Button_OnClicked(object sender,EventArgs e)
        {
            if(String.IsNullOrEmpty(FullNameEntry.Text))
            {
                await DisplayAlert("Error","Please fill your Full Name","OK");
                FullNameEntry.Focus();
                return;
            }
            if(String.IsNullOrEmpty(AddressEntry.Text))
            {
                await DisplayAlert("Error","Please fill your Address","OK");
                AddressEntry.Focus();
                return;
            }
            if(String.IsNullOrEmpty(DescribleEditor.Text))
            {
                await DisplayAlert("Error","Please describle yourself","OK");
                DescribleEditor.Focus();
                return;
            }
        }


    }
}