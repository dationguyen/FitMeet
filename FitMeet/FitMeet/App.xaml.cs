using FitMeet.Services;
using FitMeet.Views;
using Microsoft.Practices.Unity;
using Prism.Navigation;
using Prism.Unity;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;


namespace FitMeet
{
    public partial class App:PrismApplication
    {
        private string _userId;
        private string _token;
        private string _userName;


        public static Action<string> PostSuccessFacebookAction { get; set; }

        public App(IPlatformInitializer initializer = null,string userId = null,string token = null,string userName = null) : base(initializer)
        {
            _userId = userId;
            _token = token;
            _userName = userName;
            OnUpdateInitialized();
        }

        public App()
        {

        }

        private void OnUpdateInitialized()
        {
            if(!String.IsNullOrEmpty(_userId) && !String.IsNullOrEmpty(_token) && !String.IsNullOrEmpty(_userName))
            {
                var param = new NavigationParameters()
                {
                    {"id",_userId},
                    { "name",_userName},
                    { "token",_token}
                };
                NavigationService.NavigateAsync("app:///MainPage/NavigationPage/MainTabbedPage/ChatPage",param,false,true);
            }
            else
            {
                NavigationService.NavigateAsync("StartupPage");
            }
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
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
            Container.RegisterTypeForNavigation<BlockedFriendsPage>();
            Container.RegisterTypeForNavigation<FindPasswordPage>();
            Container.RegisterTypeForNavigation<ChangePasswordPage>();

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
