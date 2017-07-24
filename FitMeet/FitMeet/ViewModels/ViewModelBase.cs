using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace FitMeet.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware
    {
        protected readonly INavigationService _navigationService;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand<string> NavigateModalCommand { get; set; }

        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            NavigateModalCommand = new DelegateCommand<string>(NavigateModal);
            Title = string.Empty;
        }


        private async void NavigateModal(string path)
        {
            await _navigationService.NavigateAsync(path, null, true, false);
        }

        private async void Navigate(string path)
        {
            await _navigationService.NavigateAsync(path);
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
