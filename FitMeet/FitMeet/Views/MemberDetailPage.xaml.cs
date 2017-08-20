
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MemberDetailPage:ContentPage
    {
        private bool isOverlayVisible = true;
        public static BindableProperty IsFriendProperty = BindableProperty.Create(
            propertyName: "IsFriend",
            returnType: typeof(bool),
            declaringType: typeof(MemberDetailPage),
            defaultValue: true,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: IsFriendChanged);

        private static void IsFriendChanged(BindableObject bindable,object oldValue,object newValue)
        {
            var thisPage = (MemberDetailPage)bindable;
            thisPage.IsFriend = (bool)newValue;
        }

        public bool IsFriend
        {
            get { return (bool)GetValue(IsFriendProperty); }
            set
            {
                if(value)
                {
                    ToolbarItems.Add(MainToolbarItem);
                }
                else
                {
                    ToolbarItems.Clear();
                }
                SetValue(IsFriendProperty,value);
            }
        }


        public MemberDetailPage()
        {
            InitializeComponent();
            TriggerSecondToolbarMenu();
        }


        private void MenuItem_OnClicked(object sender,EventArgs e)
        {
            TriggerSecondToolbarMenu();
        }

        private void TriggerSecondToolbarMenu()
        {
            if(isOverlayVisible)
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

        private void TapGestureRecognizer_OnTapped(object sender,EventArgs e)
        {
            isOverlayVisible = true;
            TriggerSecondToolbarMenu();
        }


        private void ReportToolbarItem_OnClicked(object sender,EventArgs e)
        {
            TapGestureRecognizer_OnTapped(sender,e);
        }

        private void InputDialog_OnTapped(object sender,EventArgs e)
        {
            PopupLayout.IsVisible = false;
        }


    }
}