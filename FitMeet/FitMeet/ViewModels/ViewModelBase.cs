using FitMeet.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class ViewModelBase:BindableBase, INavigationAware
    {
        //instances
        protected readonly INavigationService _navigationService;
        protected readonly IFitMeetRestService _fitMeetRestService;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title,value); }
        }

        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand NavigateBackCommand { get; set; }
        public DelegateCommand<string> NavigateModalCommand { get; set; }

        public ViewModelBase( INavigationService navigationService,IFitMeetRestService fitMeetRestService )
        {
            _fitMeetRestService = fitMeetRestService;
            _navigationService = navigationService;
            NavigateBackCommand = new DelegateCommand(async () => await _navigationService.GoBackAsync());
            NavigateCommand = new DelegateCommand<string>(Navigate);
            NavigateModalCommand = new DelegateCommand<string>(NavigateModal);
            Title = string.Empty;
        }


        private async void NavigateModal( string path )
        {
            await _navigationService.NavigateAsync(path,null,true,false);
        }

        protected async void Navigate( string path )
        {
            await _navigationService.NavigateAsync(path,null,false,false);
        }

        public virtual void OnNavigatedFrom( NavigationParameters parameters )
        {

        }

        public virtual void OnNavigatedTo( NavigationParameters parameters )
        {
        }

        public virtual void OnNavigatingTo( NavigationParameters parameters )
        {
        }
    }
}
