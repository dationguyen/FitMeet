using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage:ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }


        private void ListView_OnItemSelected(object sender,SelectedItemChangedEventArgs e)
        {
            SearchEntry.Unfocus();
        }

        private void SearchEntry_OnUnfocused(object sender,FocusEventArgs e)
        {
            AutoCompleteGrid.IsVisible = false;
        }
    }
}