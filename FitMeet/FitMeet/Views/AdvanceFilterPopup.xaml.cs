using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdvanceFilterPopup : PopupPage
    {
        public AdvanceFilterPopup()
        {
            InitializeComponent();
            
        }

        private void VisualElement_OnFocused(object sender, FocusEventArgs e)
        {
            Padding = new Thickness(20, 50, 20, 0);
        }

        private void VisualElement_OnUnfocused(object sender, FocusEventArgs e)
        {
            Padding = new Thickness(20, 50);
        }
    }
}