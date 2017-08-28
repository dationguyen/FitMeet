﻿using System;
using FitMeet.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace FitMeet.ViewModels
{
    public class LoginPageViewModel:ViewModelBase
    {
        private IFacebookService _facebookService;
        private ITokenService _tokenService;
        private IPageDialogService _dialogService;


        private bool _isLoading;

        public LoginPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService,
            ITokenService tokenService,IFitMeetRestService fitMeetRestServices,
            IFacebookService facebookService) : base(navigationService,fitMeetRestServices)
        {
            _facebookService = facebookService;
            _tokenService = tokenService;
            _dialogService = dialogService;
        }

        public DelegateCommand<string> FacebookLoginCommand
        {
            get
            {
                return new DelegateCommand<string>(async (o) =>
                {
                    IsLoading = true;
                    var userProfile = await _facebookService.GetFacebookProfileAsync(o);
                    if(userProfile != null)
                    {
                        var response = await _fitMeetRestService.FacebookLoginAsync(userProfile);
                        if(response != null && response?.Output?.Status == 1)
                        {
                            var token = response?.Output?.Response?.token;
                            if(token != null)
                            {
                                _tokenService.SetToken(token);
                                if((response?.Output?.Validation).Equals("User already exists",StringComparison.CurrentCultureIgnoreCase))
                                {
                                    NavigateCommand.Execute("app:///MainPage/NavigationPage/MainTabbedPage");
                                }
                                else
                                {
                                    NavigateCommand.Execute("SecondSignUpPage");
                                }

                            }
                        }
                        else
                        {
                            await _dialogService.DisplayAlertAsync("Error","Could not register. Please try again","Ok");
                        }
                    }


                    IsLoading = false;

                });
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading,value); }
        }
    }
}
