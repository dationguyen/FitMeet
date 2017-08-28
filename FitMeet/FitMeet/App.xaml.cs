using FitMeet.Services;
using FitMeet.Views;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;


namespace FitMeet
{
    public partial class App:PrismApplication
    {
        public static Action<string> PostSuccessFacebookAction { get; set; }

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        public App()
        {

        }
        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync("StartupPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterInstance(PopupNavigation.Instance);
            //navigate registration
            Container.RegisterTypeForNavigation<NavigationPage>();
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
            Container.RegisterTypeForNavigation<NewsPage>();
            Container.RegisterTypeForNavigation<NewsDetailPage>();
            Container.RegisterTypeForNavigation<ManualLoginPage>();
            Container.RegisterTypeForNavigation<AdvanceFilterPopup>();
            Container.RegisterTypeForNavigation<MemberDetailPage>();
            Container.RegisterTypeForNavigation<ProfileEditPage>();
            Container.RegisterTypeForNavigation<StartupPage>();
            Container.RegisterTypeForNavigation<MessagePage>();
            Container.RegisterTypeForNavigation<ChatPage>();
            Container.RegisterTypeForNavigation<SecondSignUpPage>();
            Container.RegisterTypeForNavigation<ThirdSignUpPage>();

            //Services registration
            Container.RegisterType<IFitMeetRestService,FitMeetRestService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IStaticDataService,StaticDataService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IGeoLocationService,GeolocationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ITokenService,TokenService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFacebookService,FacebookService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IGoogleLocationService,GoogleLocationService>(new ContainerControlledLifetimeManager());

            Container.RegisterPopupNavigationService();
        }
    }
}
