﻿using FitMeet.Views;
using Prism.Unity;
using Xamarin.Forms;


namespace FitMeet
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        public App() : base(null)
        {

        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<BaseNavigationPage>();
            Container.RegisterTypeForNavigation<MainTabbedPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<SignUpPage>();
            Container.RegisterTypeForNavigation<SearchPage>();
            Container.RegisterTypeForNavigation<FriendsPage>();
            Container.RegisterTypeForNavigation<SettingPage>();
            Container.RegisterTypeForNavigation<PrivacyPolicyPage>();
            Container.RegisterTypeForNavigation<AboutPage>();
            Container.RegisterTypeForNavigation<MePage>();
            Container.RegisterTypeForNavigation<ManualLoginPage>();
        }
    }
}
