using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitMeet.ViewModels
{
    public class SecondSignUpPageViewModel:ViewModelBase
    {
        private IPageDialogService _dialogService;

        private bool _isMale = true;
        private DateTime _dob = DateTime.Today;
        private List<string> _autoCompleteCollection;
        private int _autoCompleteHeight;
        private string _autoCompleteResult;
        private string _address;
        private bool _isSearching;


        private readonly IGoogleLocationService _googleLocation;

        public DateTime Dob { get; set; }
        public string Describle { get; set; }
        public string FullName { get; set; }
        public string Address
        {
            get { return _address; }
            set
            {
                if(_address != value)
                {
                    SetProperty(ref _address,value);

                    if(!String.IsNullOrEmpty(_address))
                    {
                        IsSearching = true;
                        GetAutoComplete(_address);

                    }
                    else
                    {
                        IsSearching = false;
                        AutoCompleteCollection.Clear();
                    }

                }
            }
        }
        public bool IsSearching
        {
            get => _isSearching;
            set
            {
                SetProperty(ref _isSearching,value);
            }
        }
        public List<string> AutoCompleteCollection
        {
            get
            {
                if(_autoCompleteCollection == null)
                    _autoCompleteCollection = new List<string>();
                return _autoCompleteCollection;
            }
            set => SetProperty(ref _autoCompleteCollection,value);
        }
        public string AutoCompleteResult
        {
            get { return null; }
            set
            {
                if(value != null)
                {
                    SetProperty(ref _autoCompleteResult,value);
                    _address = value;
                    RaisePropertyChanged("Address");
                    IsSearching = false;
                }
            }
        }
        public int AutoCompleteHeight
        {
            get { return _autoCompleteHeight; }
            set { SetProperty(ref _autoCompleteHeight,value); }
        }
        public bool IsMale
        {
            get { return _isMale; }
            set
            {
                SetProperty(ref _isMale,value);
            }
        }


        public SecondSignUpPageViewModel(INavigationService navigationService,IGoogleLocationService googleLocation,
            IPageDialogService dialogService,IFitMeetRestService fitMeetRestServices)
            : base(navigationService,fitMeetRestServices)
        {
            _dialogService = dialogService;
            _googleLocation = googleLocation;
        }


        public DelegateCommand<string> GenderSwitchCommand
        {
            get
            {
                return new DelegateCommand<string>((string isMale) =>
                {

                    IsMale = (isMale == "male");
                });
            }
        }
        public DelegateCommand MoveNextCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    if(!String.IsNullOrEmpty(FullName) && !String.IsNullOrEmpty(Address) &&
                        !String.IsNullOrEmpty(Describle))
                    {

                        string fName = "", lName = "";
                        string[] nameParts = FullName.ToString().Trim().Split(' ');
                        lName = nameParts.Last();
                        for(int i = 0 ; i < nameParts.Length - 1 ; i++)
                        {
                            fName += nameParts[i] + " ";
                        }

                        var isSuccess = await _fitMeetRestService.SignUpStep2Async(Address,Describle,fName,lName,
                                            Dob.ToString("yyyy-MM-dd"),IsMale);
                        if(isSuccess)
                        {
                            Navigate("ThirdSignUpPage");
                        }
                        else
                        {
                            await  _dialogService.DisplayAlertAsync("Error", "There are some problem, Please try again later",
                                "Ok");
                        }
                    }
                });
            }
        }



        private async void GetAutoComplete(string searchKeyWord)
        {
            var result = await _googleLocation.AutoComplete(searchKeyWord);
            if(result != null && result.Count > 0)
            {
                AutoCompleteHeight = 36 * result.Count;
                AutoCompleteCollection = result;
            }
        }
    }
}
