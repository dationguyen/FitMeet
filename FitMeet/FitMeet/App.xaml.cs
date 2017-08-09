using FitMeet.Services;
using FitMeet.Views;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;


namespace FitMeet
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

		public App()
		{
			
		}
        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync("NavigationPage/LoginPage");
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

            //Services registration
            Container.RegisterType<IFitMeetRestService, FitMeetRestService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IActivityService, ActivityService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IGeolocationServices, GeolocationServices>(new ContainerControlledLifetimeManager());

            Container.RegisterPopupNavigationService();
        }
    }
}
