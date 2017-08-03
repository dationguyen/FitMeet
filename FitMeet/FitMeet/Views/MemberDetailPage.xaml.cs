
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MemberDetailPage : ContentPage
    {
        public MemberDetailPage()
        {
            InitializeComponent();
        }

        private void BindableObject_OnBindingContextChanged(object sender, EventArgs e)
        {
           
        }
    }
}