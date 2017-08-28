using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileEditPage:ContentPage
    {
        public ProfileEditPage()
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
            await this.ScrollView.ScrollToAsync(AutoCompleteGrid,ScrollToPosition.Center,false);
        }
    }
}