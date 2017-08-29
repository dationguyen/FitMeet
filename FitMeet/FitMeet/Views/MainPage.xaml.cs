
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage, INavigatedAware
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            var curPage = (Detail as NavigationPage)?.CurrentPage;
            (curPage as INavigatedAware)?.OnNavigatedFrom(parameters);
            (curPage?.BindingContext as INavigatedAware)?.OnNavigatedTo(parameters);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }
    }
}