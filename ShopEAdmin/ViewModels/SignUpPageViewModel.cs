using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace ShopEAdmin.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        public SignUpPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        public void LoginPage()
        {
            NavigationService.NavigateAsync("LoginPage");
        }
    }
}
