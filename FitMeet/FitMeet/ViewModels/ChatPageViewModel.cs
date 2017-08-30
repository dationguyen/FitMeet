using FitMeet.Models;
using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace FitMeet.ViewModels
{
    public class ChatPageViewModel:ViewModelBase
    {
        private IPageDialogService _dialogService;

        private MessageModel _selectedItem;
        private MessageModel _lastItem;

        private ObservableCollection<MessageModel> _messages;
        private int _currentIndex;
        private string _id;
        private bool _isTimerTick;
        private string _message;
        private bool _isAbleToSend = false;

        public ChatPageViewModel(IPageDialogService dialogService,INavigationService navigationService,IFitMeetRestService fitMeetRestService) : base(navigationService,fitMeetRestService)
        {
            _dialogService = dialogService;
        }

        public DelegateCommand SendMessageCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var result = await _fitMeetRestService.SendMessageAsync(Message,_id);
                    Message = "";
                });
            }
        }

        public ObservableCollection<MessageModel> Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages,value); }
        }

        public string Message
        {
            get => _message;
            set
            {
                IsAbleToSend = !String.IsNullOrEmpty(value);
                SetProperty(ref _message,value);
            }
        }

        public bool IsAbleToSend
        {
            get { return _isAbleToSend; }
            set { SetProperty(ref _isAbleToSend,value); }
        }

        public MessageModel SelectedItem
        {
            get => null;
            set => SetProperty(ref _selectedItem,value);
        }

        public MessageModel LastItem
        {
            get { return _lastItem; }
            set { SetProperty(ref _lastItem,value); }
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Messages = new ObservableCollection<MessageModel>();
            Title = parameters["name"]?.ToString();
            _id = parameters["id"].ToString();
            if(_id != null)
            {
                var api = await _fitMeetRestService.GetMessagesAsync(_id);
                var mesages = api.Output.Response;
                if (mesages != null)
                {
                    foreach(var message in mesages)
                    {
                        message.IsClient = !(message.SenderId == _id);
                    }
               
                    Messages = new ObservableCollection<MessageModel>(mesages);
                    var last = Messages.Last();
                    _currentIndex = last.MessageId;
                    LastItem = last;
                }
               
                _isTimerTick = true;
                Device.StartTimer(TimeSpan.FromSeconds(2),TimerOnTick);
            }
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            _isTimerTick = false;
        }

        private bool TimerOnTick()
        {
            if(_isTimerTick)
                updateMessage();
            return _isTimerTick;
        }

        private async void updateMessage()
        {
            var api = await _fitMeetRestService.GetMessageAsync(_id,_currentIndex.ToString());
            if(api?.Output?.Status == 1)
            {
                var mesages = api?.Output?.Response;
                if(mesages != null)
                    foreach(var message in mesages)
                    {
                        message.IsClient = !(message.SenderId == _id);
                        Messages.Add(message);
                        _currentIndex = message.MessageId;
                        LastItem = message;
                    }
            }

        }
    }
}
