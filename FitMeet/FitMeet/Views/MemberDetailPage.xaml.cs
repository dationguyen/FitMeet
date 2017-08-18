
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MemberDetailPage : ContentPage
    {
        private bool isOverlayVisible = true;
        public MemberDetailPage()
        {
            InitializeComponent();
            TriggerSecondToolbarMenu();
        }


        private void MenuItem_OnClicked( object sender , EventArgs e )
        {
            TriggerSecondToolbarMenu();
        }

        private void TriggerSecondToolbarMenu()
        {
            if ( isOverlayVisible )
            {
                //hide sub toolbar
                this.ToolbarItems.Remove(UnfriendToolbarItem);
                this.ToolbarItems.Remove(ReportToolbarItem);
                this.MainToolbarItem.Icon = "arrow_down_white.png";
            }
            else
            {
                //show subtoolbar
                this.ToolbarItems.Add(UnfriendToolbarItem);
                this.ToolbarItems.Add(ReportToolbarItem);
                this.MainToolbarItem.Icon = "arrow_up_white.png";
            }
            isOverlayVisible = !isOverlayVisible;
            OverlayGrid.IsVisible = isOverlayVisible;
        }

        private void TapGestureRecognizer_OnTapped( object sender , EventArgs e )
        {
            isOverlayVisible = true;
            TriggerSecondToolbarMenu();
        }


    }
}