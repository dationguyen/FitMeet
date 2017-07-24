using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage : TabbedPage
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


    }
}