using Prism.Navigation;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage : TabbedPage, INavigatingAware,INavigatedAware
    {
        public MainTabbedPage()
        {
            InitializeComponent();
            this.CurrentPageChanged += CurrentPageHasChanged;
        }

        private void CurrentPageHasChanged(object sender, EventArgs e)
        {
            var title = this.CurrentPage.Title;
            if (title == "Search")
                title = "Members";
            this.Title = title;

        }
        public void OnNavigatingTo(NavigationParameters parameters)
        {
            foreach (var child in Children)
            {
                (child as INavigatingAware)?.OnNavigatingTo(parameters);
                (child?.BindingContext as INavigatingAware)?.OnNavigatingTo(parameters);
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            foreach (var child in Children)
            {
                (child as INavigatedAware)?.OnNavigatedTo(parameters);
                (child?.BindingContext as INavigatedAware)?.OnNavigatedTo(parameters);
            }
        }

       
    }
}